using UnityEngine;


public class GameOver : BaseMenu
{
    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.GameOver;
    }

    public override void EnterState()
    {
        base.EnterState();
        Time.timeScale = 0.0f;
        GameManager.Instance.ResetCarSpeed();
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
}
