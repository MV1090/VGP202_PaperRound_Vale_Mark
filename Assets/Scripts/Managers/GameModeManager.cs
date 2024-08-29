
using UnityEngine;
using UnityEngine.Events;

public class GameModeManager : Singleton<GameModeManager>
{

    public UnityEvent<float> OnTimerValueChanged;

    private int _seconds = 0;
    public int seconds
    {

        get => _seconds; 

        set
        { 
            _seconds = value;
            OnTimerValueChanged?.Invoke(_seconds);
        }
    }
    public int minutes;
    public float currentTime;

    public bool wonTimedMode;

    [SerializeField] GameState gameState;
    [SerializeField] PlayerMovement player;

    public enum GameMode
    {
        NormalMode, TimedMode, HardMode
    }

    public GameMode mode;   
  

    private void Update()
    {                
        if (mode == GameMode.TimedMode)
        {
            currentTime += Time.deltaTime;
            updateTimer(currentTime);

            if (GameManager.Instance.score <= 0)
            {
                if (GameManager.Instance.gameOver == true)
                    return;

                wonTimedMode = true;
                GameManager.Instance.gameOver = true;
                gameState.JumpToGameOver();
                player.gameObject.SetActive(false);
            }              
        }
    }

    private void updateTimer(float time)
    {                
        minutes = (int)Mathf.FloorToInt(time / 60);
        seconds = (int)Mathf.FloorToInt(time % 60);
    }

    public void SetNormalMode()
    {
        mode = GameMode.NormalMode;
        CanvasManager.Instance.timerText.gameObject.SetActive(false);
        GameManager.Instance.score = 0;
    }

    public void SetTimedMode()
    {
        mode = GameMode.TimedMode;
        GameManager.Instance.score = 25;
        CanvasManager.Instance.timerText.gameObject.SetActive(true);
    }

    public void SetHardMode()
    {
        mode = GameMode.HardMode;
        CanvasManager.Instance.timerText.gameObject.SetActive(false);
        GameManager.Instance.score = 0;
    }

    public void GameModeScoreSet()
    {
        if(mode == GameMode.TimedMode)
        {
            TimeModeScoreSet();
            return;
        }

        if (GameManager.Instance.activeBonus == GameManager.ActiveBonus.DoubleScore)
        {
            GameManager.Instance.score++;
        }

        GameManager.Instance.score++;
    }

    private void TimeModeScoreSet()
    {
        if (GameManager.Instance.activeBonus == GameManager.ActiveBonus.DoubleScore)
        {
            GameManager.Instance.score --;
        }

        GameManager.Instance.score--;
    }




}
