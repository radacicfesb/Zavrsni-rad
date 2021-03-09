using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

using PlayFab.DataModels;
using PlayFab.ProfilesModels;
using System.Collections.Generic;
using PlayFab.Json;


public class LeaderboardController : MonoBehaviour
{
    [SerializeField] GameObject leaderboard;
    public GameObject listingPrefab;
    public Transform listingContainer;
  

    private void Awake()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        

        var requestLeaderboard = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "playerHighScore", MaxResultsCount = 20 };
        PlayFabClientAPI.GetLeaderboard(requestLeaderboard, OnGetLeaderboard, OnErrorLeaderboard);
    }

    void OnErrorLeaderboard(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    public void OnGetLeaderboard(GetLeaderboardResult result)
    {
        
        foreach (PlayerLeaderboardEntry player in result.Leaderboard)
        {
         
            GameObject tempListing = Instantiate(listingPrefab, listingContainer) as GameObject;
            LeaderboardListing LL = tempListing.GetComponent<LeaderboardListing>();
            LL.playerNameText.text = player.DisplayName;
            LL.playerScoreText.text = ((player.StatValue) / 60 + ":" + (player.StatValue) % 60).ToString();
            Debug.Log(player.DisplayName + ": " + (player.StatValue) / 60 + ":" + (player.StatValue) % 60);
        }
    }
}
