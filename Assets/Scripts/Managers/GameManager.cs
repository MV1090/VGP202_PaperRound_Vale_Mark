using UnityEngine;
using UnityEngine.Events;



public class GameManager : Singleton<GameManager>
{

    public bool gameOver;
    public UnityEvent<int> OnScoreValueChanged;

    AudioSource audioSource;

    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip cowCatcherMusic;
   
    [SerializeField] public float carSpeed;
    float startSpeed = 3;
    float endSpeed = 8;
    float duration = 120;
    float timeElapsed = 0;  

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

    public int normalModeHighScore;
    public float timerModeMinutes;
    public float timerModeSeconds;
    public int hardModeHighScore;
    
    private void Start()
    {
        gameOver = false;
        activeBonus = ActiveBonus.Normal;
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = gameMusic;
        audioSource.Play();

        timerModeMinutes = 59;
        timerModeSeconds = 59;

    ResetCarSpeed();
    }

    private void Update() 
    {     
        SetHighScores();
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
        SetCarSpeed();
        if (gameOver == true)
            ResetCarSpeed();
    }

    private void SetCarSpeed()
    {
       if (timeElapsed > duration)
            return;
                      
       timeElapsed += Time.deltaTime;
       float t = timeElapsed / duration;

       carSpeed = Mathf.Lerp(startSpeed, endSpeed, t);    
    }
    private void ResetCarSpeed()
    {
        carSpeed = startSpeed;
        timeElapsed = 0;
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

                if (timerModeMinutes <= GameModeManager.Instance.minutes)
                {
                    if (timerModeSeconds <= GameModeManager.Instance.seconds)
                    {
                        GameModeManager.Instance.wonTimedMode = false;
                        return;
                    }
                }

                timerModeMinutes = GameModeManager.Instance.minutes;
                timerModeSeconds = GameModeManager.Instance.seconds;
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



}
