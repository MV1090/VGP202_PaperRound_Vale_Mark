using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine.EventSystems;
#if INPUT_SYSTEM_PRESENT
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.UI;
#endif
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Leaderboards.Models;

class LeaderboardsDeployment : MonoBehaviour
{
    [SerializeField]
    Button m_ButtonAdd;
    [SerializeField]
    Button m_ButtonLog;
    [SerializeField]
    TMP_InputField m_InputScore;
    [SerializeField]
    StandaloneInputModule m_DefaultInputModule;
    [SerializeField]
    TMP_Text _score;

#if INPUT_SYSTEM_PRESENT
    void Awake()
    {
        m_DefaultInputModule.enabled = false;
        m_DefaultInputModule.gameObject.AddComponent<InputSystemUIInputModule>();
        TouchSimulation.Enable();
    }
#endif
    
    async void Start()
    {
        ToggleButtons(false);

        m_ButtonAdd.onClick.AddListener(async() => await AddScore_Async());
        m_ButtonLog.onClick.AddListener(async() => await LogScore_Async());

        await InitializeServices();
        await SignInAnonymously();

        ToggleButtons(true);
    }

    static async Task InitializeServices()
    {
        if (UnityServices.State == ServicesInitializationState.Uninitialized)
        {
            await UnityServices.InitializeAsync();
        }
    }

    static async Task SignInAnonymously()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    async Task AddScore_Async()
    {
        ToggleButtons(false);

        try
        {
            double score = double.Parse(m_InputScore.text);
            LeaderboardEntry result = await LeaderboardsService.Instance.AddPlayerScoreAsync("High_Scores", score);

            if (Math.Abs(score - result.Score) > double.Epsilon)
            {
                Debug.Log($"Attempted to add score {score}, but the current score of {result.Score} is better.");
            }
            else
            {
                Debug.Log($"Added score {result.Score} to the leaderboard.");
                Debug.Log($"Player: {result.PlayerName}");
                Debug.Log($"Rank: {result.Rank}");

                _score.text = (result.Rank+1) + " | " + result.PlayerName + " | " + result.Score;
            }
        }
        finally
        {
            ToggleButtons(true);
        }
    }

    async Task LogScore_Async()
    {
        ToggleButtons(false);

        try
        {
            //LeaderboardEntry
            LeaderboardEntry result = await LeaderboardsService.Instance.GetPlayerScoreAsync("High_Scores");
            Debug.Log($"Score: {result.Score}");           
        }
        finally
        {
            ToggleButtons(true);
        }
    }

    void ToggleButtons(bool toggle)
    {
        m_ButtonAdd.interactable = toggle;
        m_ButtonLog.interactable = toggle;
    }
}
