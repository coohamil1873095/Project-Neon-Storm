using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text gameOverText;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.playerWin) 
            gameOverText.SetText("You Win!");
        else
            gameOverText.SetText("You Lose!");
    }

    public void OnClick_Restart() 
    {
        Time.timeScale = 1;
        //MenuManager.CloseMenu(Menu.GAME_OVER);

        gameObject.SetActive(false);

        GameManager.Instance.RestartGame();
    }
    public void OnClick_MainMenu() 
    {
        Time.timeScale = 1;
        //mainMenu.GetComponent<MainMenu>().ChangeHighScore(GameManager.highscore);
        //MenuManager.OpenMenu(Menu.MAIN_MENU, gameObject);
        
        MenuManager.OpenMenu(Menu.MAIN_MENU, null);
        gameObject.SetActive(false);

        GameManager.Instance.QuitGame();
    }
}