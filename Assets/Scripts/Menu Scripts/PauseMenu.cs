using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.isPlaying)
        {
            if (isPaused) {
                UnPause();
            }
            else {
                isPaused = true;
            }
        }
    }

    public void OnClick_Resume() 
    {
        GameManager.Instance.toggleGameHUD(true);
        UnPause();
    }
    public void OnClick_Options() 
    {
        MenuManager.OpenMenu(Menu.OPTIONS, gameObject);
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
        isPaused = false;
        MenuManager.CloseMenu(Menu.PAUSE_MENU);
    }
}
