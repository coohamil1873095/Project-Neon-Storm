using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MenuManager
{
    public static bool IsInitialized { get; private set; }
    public static GameObject mainMenu, optionsMenu, creditsMenu /*pauseMenu, gameOverMenu*/;
    
    public static void Init()
    {
        GameObject canvas = GameObject.Find("Menus");
        mainMenu = canvas.transform.Find("MainMenu").gameObject;
        optionsMenu = canvas.transform.Find("Options").gameObject;
        creditsMenu = canvas.transform.Find("Credits").gameObject;
        //pauseMenu = canvas.transform.Find("Pause Menu").gameObject;
        //gameOverMenu = canvas.transform.Find("Game Over Menu").gameObject;

        IsInitialized = true;
    }

    public static void OpenMenu(Menu menu, GameObject prevMenu) 
    {
        if (!IsInitialized) {
            Init();
        }
        
        switch (menu) 
        {
            case Menu.MAIN_MENU:
                mainMenu.SetActive(true);
                break;
            case Menu.OPTIONS:
                optionsMenu.SetActive(true);
                break;
            case Menu.CREDITS:
                creditsMenu.SetActive(true);
                break;
            // case Menu.PAUSE_MENU:
            //     pauseMenu.SetActive(true);
            //     break;
            // case Menu.GAME_OVER:
            //     gameOverMenu.SetActive(true);
            //     break;
        }

        if (prevMenu != null) 
        {
            Debug.Log(prevMenu.name);
            prevMenu.SetActive(false);
        }
    }

    public static void CloseMenu(Menu menu)
    {
        if (!IsInitialized) {
            Init();
        }
        
        switch (menu) 
        {
            case Menu.MAIN_MENU:
                mainMenu.SetActive(false);
                break;
            case Menu.OPTIONS:
                optionsMenu.SetActive(false);
                break;
            case Menu.CREDITS:
                creditsMenu.SetActive(false);
                break;
            // case Menu.PAUSE_MENU:
            //     pauseMenu.SetActive(false);
            //     break;
            // case Menu.GAME_OVER:
            //     gameOverMenu.SetActive(false);
            //     break;
        }
    }
}
