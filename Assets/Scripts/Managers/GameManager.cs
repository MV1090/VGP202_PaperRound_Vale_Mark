using UnityEngine;
using UnityEngine.Events;
public class GameManager : Singleton<GameManager>
{

    public bool gameOver;
    public UnityEvent<int> OnScoreValueChanged;
    public UnityEvent<float> OnTimeScoreChanged;

    AudioSource audioSource;

    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip cowCatcherMusic;

    [SerializeField] public float carSpeed;
    float startSpeed = 4;
    float endSpeed = 8;
    float duration = 120;
    float timeElapsed = 0;

    public int timeModeStartValue;

    public enum ActiveBonus
    {
        Normal, DoubleScore, CowCatcher
    }

    public ActiveBonus activeBonus;

    private int _score = 0;
    public int score
    {
        get => _score;
        set
        {
            _score = value;

            OnScoreValueChanged?.Invoke(_score);
        }
    }

    private float _secondsScore = 0;
    public float secondsScore
    {
        get => _secondsScore;

        set
        {
            _secondsScore = value;
            OnTimeScoreChanged?.Invoke(_secondsScore);
        }
    }
    public float minutesScore;

    public float normalModeHighScore;
    public float hardModeHighScore;    
      
    private void Start()
    {
        gameOver = false;
        
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = gameMusic;
        audioSource.Play();

        minutesScore = 59;
        secondsScore = 59;

        ResetGame();
    }

    private void Update() 
    {             
        if (activeBonus == ActiveBonus.Normal)
        {
            if (audioSource.clip == gameMusic)
                return;
            audioSource.clip = gameMusic;
            audioSource.Play();
        }
        if (activeBonus == ActiveBonus.CowCatcher)
        {
            if (audioSource.clip == cowCatcherMusic)
                return;
            audioSource.clip = cowCatcherMusic;
            audioSource.Play();
        }               
            
    }

    private void FixedUpdate()
    {
        if (gameOver == true)
            return;

            SetCarSpeed();      
    } 

    private void SetCarSpeed()
    {
       if (timeElapsed > duration)
            return;
                      
       timeElapsed += Time.deltaTime;
       float t = timeElapsed / duration;

       carSpeed = Mathf.Lerp(startSpeed, endSpeed, t);    
    }
    public void ResetGame()
    {
        carSpeed = startSpeed;
        timeElapsed = 0;
        activeBonus = ActiveBonus.Normal;
        SetHighScores();

        Debug.Log(carSpeed.ToString() + " " + timeElapsed.ToString());
    }
    private void SetHighScores()
    {
        if(GameModeManager.Instance.mode == GameModeManager.GameMode.NormalMode)
        {
            if (score > normalModeHighScore)
            {
                normalModeHighScore = score;
            }
        }
        if(GameModeManager.Instance.wonTimedMode == true)
        {
            if (GameModeManager.Instance.mode == GameModeManager.GameMode.TimedMode)
            {

                if (minutesScore <= GameModeManager.Instance.minutes)
                {
                    if (secondsScore <= GameModeManager.Instance.seconds)
                    {
                        GameModeManager.Instance.wonTimedMode = false;
                        return;
                    }
                }

                minutesScore = GameModeManager.Instance.minutes;
                secondsScore = GameModeManager.Instance.seconds;
                GameModeManager.Instance.wonTimedMode = false;
            }
        }
        if(GameModeManager.Instance.mode == GameModeManager.GameMode.HardMode)
        {
            if (score > hardModeHighScore)
            {
                hardModeHighScore = score;
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

}
