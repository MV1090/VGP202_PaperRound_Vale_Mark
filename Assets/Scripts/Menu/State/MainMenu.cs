using UnityEngine;


public class MainMenu : BaseMenu
{
    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.MainMenu;
    }

    public override void EnterState()
    {
        base.EnterState();
        Time.timeScale = 0.0f;           
    }

    public override void ExitState()
    {
        base.ExitState();
        Time.timeScale = 1.0f;
    }

    public void JumpToSettings()
    {
        context.SetActiveState(MenuController.MenuStates.Settings);
    }

    public void JumpToGameState()
    {
        context.SetActiveState(MenuController.MenuStates.GameState);
    }
    public void JumpToNormalMode()
    {
        context.SetActiveState(MenuController.MenuStates.NormalMode);
    }
    public void JumpToTimedMode()
    {
        context.SetActiveState(MenuController.MenuStates.TimedMode);
    }
    public void JumpToHardMode()
    {
        context.SetActiveState(MenuController.MenuStates.HardMode);
    }
}
