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
    [SerializeField] TMP_Text[] playerRankNormal;
    [SerializeField] TMP_Text _playerRankNormal;
    [SerializeField] TMP_Text[] playerNameNormal;
    [SerializeField] TMP_Text _playerNameNormal;
    [SerializeField] TMP_Text[] playerScoreNormal;
    [SerializeField] TMP_Text _playerScoreNormal;

    [SerializeField] TMP_Text[] playerRankTimed;
    [SerializeField] TMP_Text _playerRankTimed;
    [SerializeField] TMP_Text[] playerNameTimed;
    [SerializeField] TMP_Text _playerNameTimed;
    [SerializeField] TMP_Text[] playerScoreTimed;
    [SerializeField] TMP_Text _playerScoreTimed;
    // Start is called before the first frame update
    async void Start()    {
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

            //string name = await AuthenticationService.Instance.GetPlayerNameAsync();

            while(AuthenticationService.Instance.PlayerName.Length > 15)
            {
                
                await AuthenticationService.Instance.UpdatePlayerNameAsync(RandomNameGenerator.Instance.RandomName());                 

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

        UpdateTimedText(_playerRankTimed ,_playerNameTimed ,_playerScoreTimed, result);
        GetTimedTopTenScores();
    }

    public void UpdateTimedText(TMP_Text rankToUpdate, TMP_Text nameToUpdate, TMP_Text scoreToUpdate, LeaderboardEntry result)
    {
        float minutes = Mathf.Floor(result.Score.ConvertTo<float>() / 60);
        float seconds = Mathf.Floor(result.Score.ConvertTo<float>() % 60);

        rankToUpdate.text = result.Rank + 1.ToString();
        nameToUpdate.text = result.PlayerName;
        scoreToUpdate.text = string.Format("{00:00} : {1:00}", minutes, seconds);
    }

    public async void GetTimedTopTenScores()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync("Timed_Leaderboard", new GetScoresOptions { Offset = 0, Limit = 10});
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));

        for (int i = 0; i < scoresResponse.Results.Count; i++)
        {
            if(scoresResponse.Results[i] == null)
                continue;

            UpdateTimedText(playerRankTimed[i], playerNameTimed[i], playerScoreTimed[i], scoresResponse.Results[i]);
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

        UpdateNormalText(_playerRankNormal, _playerNameNormal, _playerScoreNormal, result);
        GetNormalTopTenScores();
    }

    public void UpdateNormalText(TMP_Text rankToUpdate, TMP_Text nameToUpdate, TMP_Text scoreToUpdate, LeaderboardEntry result)
    {
        rankToUpdate.text = result.Rank + 1.ToString();
        nameToUpdate.text = result.PlayerName;
        scoreToUpdate.text = result.Score.ToString();
    }

    public async void GetNormalTopTenScores()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync("High_Scores", new GetScoresOptions { Offset = 0, Limit = 10 });
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));

        for (int i = 0; i < scoresResponse.Results.Count; i++)
        {
            if (scoresResponse.Results[i] == null)
                continue;

            UpdateNormalText(playerRankNormal[i], playerNameNormal[i] ,playerScoreNormal[i], scoresResponse.Results[i]);
        };
    }


}
