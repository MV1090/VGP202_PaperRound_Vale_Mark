using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardModeMenu : BaseMenu
{
    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.HardMode;
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

    public void JumpToMainMenu()
    {
        context.SetActiveState(MenuController.MenuStates.MainMenu);
    }
    public void JumpToGameState()
    {
        context.SetActiveState(MenuController.MenuStates.GameState);
    }

}
