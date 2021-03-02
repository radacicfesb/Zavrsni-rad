using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using TMPro;
using PlayFab.DataModels;
using PlayFab.ProfilesModels;
using System.Collections.Generic;

public class PlayFabLogin1 : MonoBehaviour
{
    public static PlayFabLogin1 PFC;//pfc == playfab controller

    private string userEmail;//ovo posli izbaci
    private string userPassword;//ovo posli izbaci
    private string username;
    public GameObject loginCanvas;
    public TMP_Text usernameTakenText;
    public GameObject mainMenuCanvas;
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
        loginCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
        GetStats();
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

    #endregion PlayerStats
}

