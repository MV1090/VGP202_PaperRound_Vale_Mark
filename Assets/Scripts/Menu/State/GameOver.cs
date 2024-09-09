using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameOver : BaseMenu
{
    [SerializeField] Image image;
    [SerializeField] Sprite[] endGameImage;
    [SerializeField] TMP_Text endGameText;
    [SerializeField] TMP_Text finalScoreText;

    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.GameOver;
    }

    public override void EnterState()
    {
        base.EnterState();
        Time.timeScale = 0.0f;

        SetEndGameImage();
        SetEndGameText();
        SetFinalScoreText();

        GameManager.Instance.ResetGame();
        ParticleManager.Instance.StopParticle();
        CoroutineManager.Instance.StopAllCoroutines();       
    }

    public override void ExitState()
    {
        base.ExitState();
        Time.timeScale = 1.0f;
    }

    public void JumpToMainMenu()
    {
        context.SetActiveState(MenuController.MenuStates.MainMenu);
        Debug.Log("jump to main menu");
    }

    public void JumpToGameState()
    {
        context.SetActiveState(MenuController.MenuStates.GameState);
    }

    public void SetEndGameImage()
    {
        if (GameModeManager.Instance.wonTimedMode == true)
            image.sprite = endGameImage[0];
        else
            image.sprite = endGameImage[1];
    }

    public void SetEndGameText()
    {
        if (GameModeManager.Instance.wonTimedMode == true)
            endGameText.text = "WINNER";
        else
            endGameText.text = "GAME OVER";
    }

    public void SetFinalScoreText()
    {
        if (GameModeManager.Instance.mode == GameModeManager.GameMode.TimedMode)
        {
            if (GameModeManager.Instance.wonTimedMode == true)
                finalScoreText.text = "Time: " + string.Format("{00:00} : {1:00}", GameModeManager.Instance.minutes.ToString("00"), GameModeManager.Instance.seconds.ToString("00"));
            else
                finalScoreText.text = GameManager.Instance.score.ToString() + " Undelivered Papers";
        }           

        else
            finalScoreText.text = "Score: " + GameManager.Instance.score.ToString();

    }
}
