using UnityEngine;
using UnityEngine.Events;



public class GameManager : Singleton<GameManager>
{

    public bool gameOver;
    public UnityEvent<int> OnScoreValueChanged;

    AudioSource audioSource;

    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip cowCatcherMusic;
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
    }

    private void Update() 
    {
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
       
}
