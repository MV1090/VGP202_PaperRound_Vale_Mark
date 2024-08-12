
using UnityEngine;
using TMPro;
using UnityEngine.UI;

using UnityEngine.Audio;


public class CanvasManager : Singleton<CanvasManager>
{
    public AudioMixer audioMixer;   

    [Header("Text")]   
    public TextMeshPro scoreText;
       

    // Start is called before the first frame update
    void Start()
    {       
        if(scoreText)
        {
            GameManager.Instance.OnScoreValueChanged.AddListener(UpdateMoneyText);
            scoreText.text = GameManager.Instance.score.ToString();
        }                          
    }
    void UpdateMoneyText(int value)
    {
        scoreText.text = value.ToString();
    }          
     
    // Update is called once per frame
    void Update()
    {
       

    }       
}
