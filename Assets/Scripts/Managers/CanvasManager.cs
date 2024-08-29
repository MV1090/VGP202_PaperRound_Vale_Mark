using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CanvasManager : Singleton<CanvasManager>
{
    public AudioMixer audioMixer;   

    [Header("Text")]   
    [SerializeField] TMP_Text scoreText;
    [SerializeField] public TMP_Text timerText;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text fastestTimeText;
    [SerializeField] TMP_Text volumeText;
    [SerializeField] TMP_Text musicText;
    [SerializeField] TMP_Text SFXText;

    [Header("Sliders")]
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (scoreText)
        {
            GameManager.Instance.OnScoreValueChanged.AddListener(UpdatePaperText);
            scoreText.text = "Papers: " + GameManager.Instance.score.ToString();
        }
        if (highScoreText)
        {
            GameManager.Instance.OnScoreValueChanged.AddListener(UpdateHighScore);            
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
        if (timerText)
        {
            timerText.text = string.Format("{00:00} : {1:00}", GameModeManager.Instance.minutes, GameModeManager.Instance.seconds);
        }
    }

    void UpdatePaperText(int value)
    {
        scoreText.text = "Papers: " + value.ToString();
    }

    void UpdateHighScore(int value)
    {
        if (GameModeManager.Instance.mode == GameModeManager.GameMode.NormalMode)
        {
            if (GameManager.Instance.score < GameManager.Instance.normalModeHighScore)
                return;

            highScoreText.text = "High Score " + value.ToString();
        }
    }

    void UpDateFastestTime()
    {
        if (GameModeManager.Instance.mode == GameModeManager.GameMode.TimedMode)
        {         
             fastestTimeText.text = "Fastest time " + string.Format("{00:00} : {1:00}", GameManager.Instance.timerModeMinutes, GameManager.Instance.timerModeSeconds);           
        }
    }
    void UpDateTimerText()
    {
        if (timerText)
        {
            timerText.text = string.Format("{00:00} : {1:00}", GameModeManager.Instance.minutes, GameModeManager.Instance.seconds);
        }
    }

    void OnSliderValueChanged(float value, TMP_Text sliderText, string sliderName)
    {
        sliderText.text = value.ToString();
        audioMixer.SetFloat(sliderName, value - 80);
    }

    // Update is called once per frame
    void Update()
    {
        UpDateTimerText();
        UpDateFastestTime();
    }       
}
