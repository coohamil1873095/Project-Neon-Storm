using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public void OnClick_Back() 
    {
        if (GameManager.isPlaying) 
            MenuManager.OpenMenu(Menu.PAUSE_MENU, gameObject);
        else
            MenuManager.OpenMenu(Menu.MAIN_MENU, gameObject);

    }
}
