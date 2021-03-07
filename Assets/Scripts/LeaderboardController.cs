using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using TMPro;
using PlayFab.DataModels;
using PlayFab.ProfilesModels;
using System.Collections.Generic;
using PlayFab.Json;
//using UnityEngine.SceneManagement;

public class LeaderboardController : MonoBehaviour
{
    
    //PlayFabLogin1 playfab;

    [SerializeField] GameObject leaderboard;
    //[SerializeField] GameObject score;
    public GameObject listingPrefab;
    public Transform listingContainer;
    private void Start()
    {
       
        //playfab = FindObjectOfType<PlayFabLogin1>();
    }

    private void Awake()
    {
        //leaderboard.SetActive(true);
        //score.SetActive(false);
        
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        //leaderboardCanvas.SetActive(true);

        var requestLeaderboard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "playerHighScore", MaxResultsCount = 20 };
        PlayFabClientAPI.GetLeaderboard(requestLeaderboard, OnGetLeaderboard, OnErrorLeaderboard);
    }

    void OnErrorLeaderboard(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    public void OnGetLeaderboard(GetLeaderboardResult result)
    {
        //leaderboardCanvas.SetActive(true);
        //playerMovement.scoreCanvas.SetActive(false);

        //Debug.LogError(result.Leaderboard[0].StatValue);
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
            //Transform leaderboard = transform.GetChild(1).transform;
            GameObject tempListing = Instantiate(listingPrefab, listingContainer) as GameObject;
            //tempListing.transform.SetParent(leaderboard);
            LeaderboardListing LL = tempListing.GetComponent<LeaderboardListing>();
            LL.playerNameText.text = player.DisplayName;
            LL.playerScoreText.text = ((player.StatValue) / 60 + ":" + (player.StatValue) % 60).ToString();
            Debug.Log(player.DisplayName + ": " + (player.StatValue) / 60 + ":" + (player.StatValue) % 60);
        }
    }
}
