using UnityEngine;
using UnityEngine.UI;


public class SettingsMenu : BaseMenu
{
    [SerializeField] GameObject player;    
    [SerializeField] Image playerImage;

    [SerializeField] Sprite[] playerSprites;
       
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
       player.GetComponent<SpriteRenderer>().sprite = playerSprites[0];
       playerImage.sprite = playerSprites[0];
    }

    public void PurpleCharacter() 
    {
        player.GetComponent<SpriteRenderer>().sprite = playerSprites[1];
        playerImage.sprite = playerSprites[1];
    }

    public void WhiteCharacter() 
    {
        player.GetComponent<SpriteRenderer>().sprite = playerSprites[2];
        playerImage.sprite = playerSprites[2];
    }


}
