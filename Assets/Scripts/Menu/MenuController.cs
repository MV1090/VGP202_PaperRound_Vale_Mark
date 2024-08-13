using System.Collections.Generic;
using UnityEngine;

public class MenuController : Singleton<MenuController>
{
    public BaseMenu[] allMenus;
    public enum MenuStates
    {
        MainMenu, Settings, Controls, WinScreen, GameOver, GameState 
    }

    private BaseMenu currentState;
    public Dictionary<MenuStates, BaseMenu> menuDictionary = new Dictionary<MenuStates, BaseMenu>();
    private Stack<MenuStates> menuStack = new Stack<MenuStates>();

    void Start()
    {
        if (allMenus == null)
            return;

        foreach (BaseMenu menu in allMenus)
        {
            if (menu == null) continue;

            menu.InitState(this);

            if (menuDictionary.ContainsKey(menu.state))
                continue;

            menuDictionary.Add(menu.state, menu);
        }

        foreach(MenuStates state in menuDictionary.Keys)
        {
            menuDictionary[state].gameObject.SetActive(false);
        }
        SetActiveState(MenuStates.MainMenu);
    }

    public void SetActiveState(MenuStates newState, bool isJumpingBack = false)
    {
        if (!menuDictionary.ContainsKey(newState))
            return;

        if(currentState != null)
        {
            currentState.ExitState();
            currentState.gameObject.SetActive(false);
        }

        currentState = menuDictionary[newState];    
        currentState.gameObject.SetActive(true);
        currentState.EnterState();

        if(!isJumpingBack)
        {
            menuStack.Push(newState);   
        }
    }

    public void JumpBack()
    {
        if (menuStack.Count >= 1)
        {
            SetActiveState(MenuStates.MainMenu);
        }
        else
        {
            menuStack.Pop();
            SetActiveState(menuStack.Peek(), true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
