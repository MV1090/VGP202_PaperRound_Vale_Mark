using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{

    public bool gameOver;
    public UnityEvent<int> OnScoreValueChanged;
        
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
    }

}
