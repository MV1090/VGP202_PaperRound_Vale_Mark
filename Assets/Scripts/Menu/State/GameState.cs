using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

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
