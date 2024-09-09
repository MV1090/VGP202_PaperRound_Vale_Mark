using System.Collections;
using UnityEngine;


public class GameState : BaseMenu
{
    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.GameState;
    }

    public override void EnterState()
    {
        base.EnterState();
        Time.timeScale = 1.0f;

        if (GameModeManager.Instance.mode == GameModeManager.GameMode.TimedMode)
        {
            Debug.Log("Timed mode set");
            GameManager.Instance.score = GameManager.Instance.timeModeStartValue;
            GameModeManager.Instance.currentTime = 0;
        }
        else
            GameManager.Instance.score = 0;
    }

    public override void ExitState()
    {
        base.ExitState();       
    }
    public void JumpToGameOver()
    {
        context.SetActiveState(MenuController.MenuStates.GameOver);
    }


}
