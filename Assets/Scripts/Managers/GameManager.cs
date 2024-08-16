using UnityEngine.Events;



public class GameManager : Singleton<GameManager>
{

    public bool gameOver;
    public UnityEvent<int> OnScoreValueChanged;

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

    private void Start()
    {
        gameOver = false;
        activeBonus = ActiveBonus.Normal;
    }

}
