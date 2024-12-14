using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedModeLeaderboard : BaseMenu
{
    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.TimedLeaderboard;
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

    public void JumpToTimedMode()
    {
        context.SetActiveState(MenuController.MenuStates.TimedMode);
    }
}
