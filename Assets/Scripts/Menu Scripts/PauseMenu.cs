using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    void Update() 
    {
        
    }

    public void OnClick_Resume() 
    {
        GameManager.Instance.toggleGameHUD(true);
        GameManager.Instance.toggleAbilityHUD(true);
        UnPause();
    }
    public void OnClick_Options() 
    {
        //MenuManager.OpenMenu(Menu.OPTIONS, gameObject);

        MenuManager.OpenMenu(Menu.OPTIONS, null);
    }
    public void OnClick_MainMenu() 
    {
        MenuManager.OpenMenu(Menu.MAIN_MENU, null);
        GameManager.Instance.QuitGame();
        UnPause();
    }

    private void UnPause() 
    {
        Time.timeScale = 1;
        GameManager.Instance.SetPauseStatus(false);
        //MenuManager.CloseMenu(Menu.PAUSE_MENU);

        gameObject.SetActive(false);
    }
}
