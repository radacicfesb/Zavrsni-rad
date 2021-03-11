using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using TMPro;
using PlayFab.DataModels;
using PlayFab.ProfilesModels;
using System.Collections.Generic;
using PlayFab.Json;
using UnityEngine.SceneManagement;

public class PlayfabController : MonoBehaviour
{
    public static PlayfabController PFC;//pfc == playfab controller

    
    private string userPassword;
    private string username;

    public GameObject menuCanvas;
    public GameObject loginCanvas;

    public TMP_Text usernameTakenText;

    string currentSceneName;
    private void Awake()
    {
        if (PFC == null)
        {
            PFC = this;
        }
        else
        {
            if (PFC != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {

        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "E8F0E"; // Please change this value to your own titleId from PlayFab Game Manager
        }
        PlayerPrefs.DeleteAll();
       
        if (PlayerPrefs.HasKey("USERNAME"))
        {
           
            userPassword = PlayerPrefs.GetString("PASSWORD");
            username = PlayerPrefs.GetString("USERNAME");
           
            var request = new LoginWithPlayFabRequest { Username = username, Password = userPassword, };
            PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);

        }

    }

    private void Update()
    {
        currentSceneName = SceneManager.GetActiveScene().name;

        if(currentSceneName == "GameScene")
        {
            menuCanvas.SetActive(false);
        }
        else if (currentSceneName == "MainMenu")
        {
            menuCanvas.SetActive(true);
        }
    }

    #region Login
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("USERNAME", username);
       
        PlayerPrefs.SetString("PASSWORD", userPassword);
        loginCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        GetStats();
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("USERNAME", username);
        
        PlayerPrefs.SetString("PASSWORD", userPassword);

        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = username }, OnDisplayName, OnRegisterFailure);
        usernameTakenText.text = "";
        loginCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        GetStats();
    }

    void OnDisplayName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log(result.DisplayName + " is your new display name");
    }

    private void OnLoginFailure(PlayFabError error)
    {

        if (error.ToString().Contains("AccountNotFound"))
           usernameTakenText.text = "";
        else
        {
            usernameTakenText.text = "Username is officially taken";
            Invoke("RemoveTakenText", 2f);
            Debug.Log("taken");
        }
       
        var registerRequest = new RegisterPlayFabUserRequest { Username = username, Password = userPassword, RequireBothUsernameAndEmail = false };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterFailure(PlayFabError error)
    {

        Debug.LogError(error.GenerateErrorReport());
    }

    

    public void GetUserPassword(string passwordIn)
    {
        userPassword = passwordIn;
    }

    public void GetUsername(string usernameIn)
    {
        username = usernameIn;
    }

    public void OnClickLogin()
    {  
        var request = new LoginWithPlayFabRequest { Username = username, Password = userPassword };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
    }

    public void RemoveTakenText()
    {
        usernameTakenText.text = "";
    }

    #endregion Login

    public float playerHighScore;

    #region PlayerStats

    public void SetStats()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            // request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate { StatisticName = "playerHighScore", Value = playerHighScore},
    }
        },
        result => { Debug.Log("User statistics updated"); },
        error => { Debug.LogError(error.GenerateErrorReport()); });
    }

    void GetStats()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            OnGetStats,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    void OnGetStats(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics)
        {
            Debug.Log("Statistic (" + eachStat.StatisticName + "): " + eachStat.Value);
            switch (eachStat.StatisticName)
            {
                case "playerHighScore":
                    playerHighScore = eachStat.Value;
                    break;
            }
        }

    }

    // Build the request object and access the API
    public void StartCloudUpdatePlayerStats()
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "UpdatePlayerStats", // Arbitrary function name (must exist in your uploaded cloud.js file)
            FunctionParameter = new { highScore = playerHighScore }, // The parameter provided to your function
            GeneratePlayStreamEvent = true, // Optional - Shows this event in PlayStream
        }, OnCloudUpdateStats, OnErrorShared);
    }
    // OnCloudHelloWorld defined in the next code block

    private static void OnCloudUpdateStats(ExecuteCloudScriptResult result)
    {
        // CloudScript returns arbitrary results, so you have to evaluate them one step and one parameter at a time
        Debug.Log(PlayFab.PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer));
        JsonObject jsonResult = (JsonObject)result.FunctionResult;
        object messageValue;
        jsonResult.TryGetValue("messageValue", out messageValue); // note how "messageValue" directly corresponds to the JSON values set in CloudScript
        Debug.Log((string)messageValue);
    }

    private static void OnErrorShared(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    #endregion PlayerStats
}
