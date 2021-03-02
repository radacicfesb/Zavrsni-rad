using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using TMPro;
using PlayFab.DataModels;
using PlayFab.ProfilesModels;
using System.Collections.Generic;
using PlayFab.Json;

public class PlayFabLogin1 : MonoBehaviour
{
    public static PlayFabLogin1 PFC;//pfc == playfab controller

    private string userEmail;//ovo posli izbaci
    private string userPassword;//ovo posli izbaci
    private string username;
    
    public TMP_Text usernameTakenText;
    public GameObject mainMenuCanvas;
    PlayerMovement playerMovement;
    
    //public List<string, float> StatsUpdate  = new List
    private void OnEnable()
    {
        if(PlayFabLogin1.PFC == null)
        {
            PlayFabLogin1.PFC = this;
        }
        else
        {
            if(PlayFabLogin1.PFC != this)
            {
                Destroy(this.gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "E8F0E"; // Please change this value to your own titleId from PlayFab Game Manager
        }
        //PlayerPrefs.DeleteAll();//mskni posli
        //var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        // PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        if (PlayerPrefs.HasKey("USERNAME"))
        {
            //userEmail = PlayerPrefs.GetString("EMAIL");
            userPassword = PlayerPrefs.GetString("PASSWORD");
            username = PlayerPrefs.GetString("USERNAME");
            //var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
            var request = new LoginWithPlayFabRequest { Username = username, Password = userPassword, };
            PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);

        }
        /* else
         {
 #if UNITY_ANDROID
             var requestAndroid = new LoginWithAndroidDeviceIDRequest {AndroidDeviceId = ReturnMpboleID(), CreateAccount = true};
             PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, OnLoginMobileSuccess, OnLoginMobileFailure);
 #endif
 #if UNITY_IOS
             var requestIOS = new LoginWithIOSDeviceIDRequest {DeviceId = ReturnMobileID, CreateAccount - true};
             PlayFabClientAPI.LoginWithIOSDeviceID(requestIOS, OnLoginMobileSuccess, OnLoginMobileFailure);
 #endif
         }*/
    }

    /* private void OnLoginMobileSuccess(LoginResult result)//login mi ne triba
     {
         Debug.Log("Congratulations, you made your first successful API call!");
         loginCanvas.SetActive(false);
     }

     private void OnLoginMobileFailure(PlayFabError error)//ne triba
     {
         Debug.Log(error.GenerateErrorReport());
     }

     public static string ReturnMobileID()
     {
         string deviceID = SystemInfo.deviceUniqueIdentifier;
         return deviceID;
     }*/

    #region Login
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("USERNAME", username);
        //PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        loginCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
        GetStats();//mozda bude tribalo micat
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("USERNAME", username);
        //PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);

        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest { DisplayName = username }, OnDisplayName, OnRegisterFailure);
        loginCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
        GetStats();
    }

    void OnDisplayName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log(result.DisplayName + " is your new display name");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        
        if (error.ToString().Contains("AccountNotFound"))
            //Debug.Log("available");
            usernameTakenText.text = "";
        else 
        {
            usernameTakenText.text = "Username is taken";
            Invoke("RemoveTakenText", 2f);
        }
        //Debug.Log("taken");
        var registerRequest = new RegisterPlayFabUserRequest { Username = username, Password = userPassword, RequireBothUsernameAndEmail = false };//{ Email = userEmail, Password = userPassword, Username = username };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterFailure(PlayFabError error)
    {
       
        Debug.LogError(error.GenerateErrorReport());
    }

    public void GetUserEmail(string emailIn)//ne triba
    {
        userEmail = emailIn;
    }

    public void GetUserPassword(string passwordIn)
    {
        userPassword = passwordIn;
    }

    public void GetUsername(string usernameIn)
    {
        username = usernameIn;
    }

    public void OnClickLogin()//ne triba
    {
        //var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
        //PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        var request = new LoginWithPlayFabRequest { Username = username, Password = userPassword };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
    }

   public void RemoveTakenText()
    {
        usernameTakenText.text = "";
    }
    #endregion

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

    public GameObject leaderboardCanvas;
    public GameObject listingPrefab;
    public Transform listingContainer;
    public GameObject loginCanvas;
    #region Leaderboard

    public void GetLeaderboard()
    {
        //leaderboardCanvas.SetActive(true);

        var requestLeaderboard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "playerHighScore", MaxResultsCount = 20 };
        PlayFabClientAPI.GetLeaderboard(requestLeaderboard, OnGetLeaderboard, OnErrorLeaderboard);
    }

    void OnGetLeaderboard(GetLeaderboardResult result)
    {
        leaderboardCanvas.SetActive(true);
        playerMovement.scoreCanvas.SetActive(false);
        
        //Debug.LogError(result.Leaderboard[0].StatValue);
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            GameObject tempListing = Instantiate(listingPrefab, listingContainer);
            LeaderboardListing LL = tempListing.GetComponent<LeaderboardListing>();
            LL.playerNameText.text = player.DisplayName;
            LL.playerScoreText.text = ((player.StatValue) / 60 + ":" + (player.StatValue) % 60).ToString();
            Debug.Log(player.DisplayName + ": " + (player.StatValue) / 60 + ":" + (player.StatValue) % 60);
        }
    }

    public void CloseLeaderboardCanvas()
    {
        leaderboardCanvas.SetActive(false);
        playerMovement.scoreCanvas.SetActive(true);
        for(int i = listingContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(listingContainer.GetChild(i).gameObject);
        }
    }

    void OnErrorLeaderboard(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    #endregion Leaderboard
}

