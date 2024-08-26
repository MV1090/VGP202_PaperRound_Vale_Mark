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
    float startSpeed = 2;
    float endSpeed = 7;
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

    public int highScore;
    
    private void Start()
    {
        gameOver = false;
        activeBonus = ActiveBonus.Normal;
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = gameMusic;
        audioSource.Play();

        ResetCarSpeed();
    }

    private void Update() 
    {
        SetCarSpeed();

        if (gameOver == true)
            ResetCarSpeed();

        if (score > highScore)
        {
            highScore = score;
        }

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

    private void SetCarSpeed()
    {
       if (timeElapsed > duration)
            return;
                      
       timeElapsed += Time.deltaTime;
       float t = timeElapsed / duration;

       carSpeed = Mathf.Lerp(startSpeed, endSpeed, t);

       Debug.Log(carSpeed.ToString());      
    }


    private void ResetCarSpeed()
    {
        carSpeed = startSpeed;
        timeElapsed = 0;
    }
}
