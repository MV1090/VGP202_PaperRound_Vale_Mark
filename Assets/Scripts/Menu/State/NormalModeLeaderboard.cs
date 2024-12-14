using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalModeLeaderboard : BaseMenu
{
    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.NormalLeaderboard;
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

    public void JumpToLeaderboardMenu()
    {
        context.SetActiveState(MenuController.MenuStates.LeaderboardMenu);
    }

    public void JumpToNormalMode()
    {
        context.SetActiveState(MenuController.MenuStates.NormalMode);
    }
   
}
