
using UnityEngine;
using TMPro;
using UnityEngine.UI;

using UnityEngine.Audio;


public class CanvasManager : Singleton<CanvasManager>
{
    public AudioMixer audioMixer;   

    [Header("Text")]   
    public TMP_Text scoreText;
       

    // Start is called before the first frame update
    void Start()
    {       
        if(scoreText)
        {
            GameManager.Instance.OnScoreValueChanged.AddListener(UpdatePaperText);

            scoreText.text = GameManager.Instance.score.ToString();
        }                          
    }
    void UpdatePaperText(int value)
    {
        scoreText.text = "Papers: " + value.ToString();
    }          
     
    // Update is called once per frame
    void Update()
    {
       

    }       
}
