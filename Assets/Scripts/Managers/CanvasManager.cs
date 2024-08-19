
using UnityEngine;
using TMPro;
using UnityEngine.UI;

using UnityEngine.Audio;


public class CanvasManager : Singleton<CanvasManager>
{
    public AudioMixer audioMixer;   

    [Header("Text")]   
    public TMP_Text scoreText;
    public TMP_Text HighScoreText;
       

    // Start is called before the first frame update
    void Start()
    {       
        if(scoreText)
        {
            GameManager.Instance.OnScoreValueChanged.AddListener(UpdatePaperText);
            scoreText.text = "Papers: " + GameManager.Instance.score.ToString();
        }
        if(HighScoreText)
        {
            GameManager.Instance.OnScoreValueChanged.AddListener(UpdateHighScore);
            //HighScoreText.text = "High Score " + GameManager.Instance.highScore.ToString();
        }

    }
    void UpdatePaperText(int value)
    {
        scoreText.text = "Papers: " + value.ToString();
    }

    void UpdateHighScore(int value)
    {
        if (GameManager.Instance.score < GameManager.Instance.highScore)
         return;
           
        HighScoreText.text = "High Score " + value.ToString();        
    }
     
    // Update is called once per frame
    void Update()
    {
       

    }       
}
