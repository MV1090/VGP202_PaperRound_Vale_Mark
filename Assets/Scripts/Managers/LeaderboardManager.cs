using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Leaderboards.Models;
using Newtonsoft.Json;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;

public class LeaderboardManager : Singleton<LeaderboardManager>
{
    [SerializeField] TMP_Text[] playerScoreNormal;
    [SerializeField] TMP_Text _playerScoreNormal;

    [SerializeField] TMP_Text[] playerScoreTimed;
    [SerializeField] TMP_Text _playerScoreTimed;
    // Start is called before the first frame update
    async void Start()
    {
        
        await InitializeServices();
        await SignInAnonymously();
        UpdateScoresNormal();
        UpdateScoresTimed();
    }

    static async Task InitializeServices()
    {
        if (UnityServices.State == ServicesInitializationState.Uninitialized)
        {
            await UnityServices.InitializeAsync();
        }
    }

    async Task SignInAnonymously()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
        {            
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            string name = await AuthenticationService.Instance.GetPlayerNameAsync();

            if (name.Length > 5)
            {
                Debug.Log("New name needed");
            }
        }
    }

    public async Task AddScoreTimed(float minutes, float seconds)
    {
        float score = (minutes * 60) + seconds;

        LeaderboardEntry result = await LeaderboardsService.Instance.AddPlayerScoreAsync("Timed_Leaderboard", score);

        UpdateScoresTimed();
    }

    public async void UpdateScoresTimed()
    {    
        LeaderboardEntry result = await LeaderboardsService.Instance.GetPlayerScoreAsync("Timed_Leaderboard");
        Debug.Log(JsonConvert.SerializeObject(result));

        UpdateTimedText(_playerScoreTimed, result);
        GetTimedTopTenScores();
    }

    public void UpdateTimedText(TMP_Text textToUpdate, LeaderboardEntry result)
    {
        float minutes = Mathf.Floor(result.Score.ConvertTo<float>() / 60);
        float seconds = Mathf.Floor(result.Score.ConvertTo<float>() % 60);

        textToUpdate.text = (result.Rank + 1) + "     " + result.PlayerName + "   " + string.Format("{00:00} : {1:00}", minutes, seconds);
    }

    public async void GetTimedTopTenScores()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync("Timed_Leaderboard", new GetScoresOptions { Offset = 0, Limit = 10});
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));

        for (int i = 0; i < scoresResponse.Results.Count; i++)
        {
            if(scoresResponse.Results[i] == null)
                continue;

            UpdateTimedText(playerScoreTimed[i], scoresResponse.Results[i]);
        };
    }

    public async Task AddScoreNormal(float score)
    {
        LeaderboardEntry result = await LeaderboardsService.Instance.AddPlayerScoreAsync("High_Scores", score);

        UpdateScoresNormal();
    }

   
    public async void UpdateScoresNormal()
    {
        LeaderboardEntry result = await LeaderboardsService.Instance.GetPlayerScoreAsync("High_Scores");
        Debug.Log(JsonConvert.SerializeObject(result));

        UpdateNormalText(_playerScoreNormal, result);
        GetNormalTopTenScores();
    }

    public void UpdateNormalText(TMP_Text textToUpdate, LeaderboardEntry result)
    {
        textToUpdate.text = (result.Rank + 1) + "     " + result.PlayerName + "   " + result.Score;
    }

    public async void GetNormalTopTenScores()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync("High_Scores", new GetScoresOptions { Offset = 0, Limit = 10 });
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));

        for (int i = 0; i < scoresResponse.Results.Count; i++)
        {
            if (scoresResponse.Results[i] == null)
                continue;

            UpdateNormalText(playerScoreNormal[i], scoresResponse.Results[i]);

        };
    }


}
