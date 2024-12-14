using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardMenu : BaseMenu
{
    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.LeaderboardMenu;
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

    public void JumpToNormalModeLeaderboard()
    {
        context.SetActiveState(MenuController.MenuStates.NormalLeaderboard);
    }

    public void JumpToTimedModeLeaderboard()
    {
        context.SetActiveState(MenuController.MenuStates.TimedLeaderboard);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
