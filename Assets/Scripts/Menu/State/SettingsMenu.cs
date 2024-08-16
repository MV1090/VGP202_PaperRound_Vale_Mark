using UnityEngine;
using UnityEngine.UI;


public class SettingsMenu : BaseMenu
{
    [SerializeField] GameObject player;    
    [SerializeField] Image playerImage;
       
    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.Settings;       
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

    public void YellowCharacter()
    {
       player.GetComponent<SpriteRenderer>().color = new Color(238 / 255f, 213 / 255f, 24 / 255f, 255 / 255f);
       playerImage.color = new Color(238 / 255f, 213 / 255f, 24 / 255f, 255 / 255f);
    }

    public void PurpleCharacter() 
    {
        player.GetComponent<SpriteRenderer>().color = new Color(153/255f,28/255f,233/255f,255/255f);
        playerImage.color = new Color(153 / 255f, 28 / 255f, 233 / 255f, 255 / 255f);
    }

    public void WhiteCharacter() 
    {
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1,1, 1);
        playerImage.color = new Color(1, 1, 1, 1);
    }


}
