
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
    public TMP_Text volumeText;
    public TMP_Text musicText;
    public TMP_Text SFXText;

    [Header("Sliders")]
    public Slider volumeSlider;
    public Slider musicSlider;
    public Slider SFXSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (scoreText)
        {
            GameManager.Instance.OnScoreValueChanged.AddListener(UpdatePaperText);
            scoreText.text = "Papers: " + GameManager.Instance.score.ToString();
        }
        if (HighScoreText)
        {
            GameManager.Instance.OnScoreValueChanged.AddListener(UpdateHighScore);
            //HighScoreText.text = "High Score " + GameManager.Instance.highScore.ToString();
        }

        if (volumeSlider)
        {
            volumeSlider.onValueChanged.AddListener((sliderValue) => OnSliderValueChanged(sliderValue, volumeText, "Master"));
            float newValue;
            audioMixer.GetFloat("Master", out newValue);
            volumeSlider.value = newValue + 80;
            if (volumeText)
                volumeText.text = volumeSlider.value.ToString();
        }

        if (musicSlider)
        {
            musicSlider.onValueChanged.AddListener((sliderValue) => OnSliderValueChanged(sliderValue, musicText, "Music"));
            float newValue;
            audioMixer.GetFloat("Music", out newValue);
            musicSlider.value = newValue + 80;
            if (musicText)
                musicText.text = musicSlider.value.ToString();
        }

        if (SFXSlider)
        {
            SFXSlider.onValueChanged.AddListener((sliderValue) => OnSliderValueChanged(sliderValue, SFXText, "SFX"));
            float newValue;
            audioMixer.GetFloat("SFX", out newValue);
            SFXSlider.value = newValue + 80;
            if (SFXText)
                SFXText.text = SFXSlider.value.ToString();

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

    void OnSliderValueChanged(float value, TMP_Text sliderText, string sliderName)
    {
        sliderText.text = value.ToString();
        audioMixer.SetFloat(sliderName, value - 80);

    }

    // Update is called once per frame
    void Update()
    {
       

    }       
}
