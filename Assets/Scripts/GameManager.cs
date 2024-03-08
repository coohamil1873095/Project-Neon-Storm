using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static bool isPlaying { get; private set; }
    private Canvas gameHUD;
    private Canvas levelHUD;
    [SerializeField] private TMP_Text XPText;
    [SerializeField] private TMP_Text LevelText;
    private int level = 1;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameHUD = GameObject.Find("Game HUD").GetComponent<Canvas>();
        levelHUD = GameObject.Find("Level HUD").GetComponent<Canvas>();
        toggleLevelHUD(false);
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PauseMenu.isPaused)
        {
            PauseMenu.isPaused = true;
            Time.timeScale = 0;
            toggleGameHUD(false);
            MenuManager.OpenMenu(Menu.PAUSE_MENU, null);
        }
    }

    public void UpdateXPCounter(int newXPVal)
    {
        XPText.SetText("XP: " + newXPVal);
    }

    public void toggleGameHUD(bool activeStatus)
    {
        gameHUD.enabled = activeStatus;
    }

    public void toggleLevelHUD(bool activeStatus)
    {
        levelHUD.enabled = activeStatus;
    }

    public void OnLevelUp()
    {
        level++;
        Debug.Log("Player Leveled Up");
        LevelText.SetText("Level: " + level);
        toggleLevelHUD(true);
        PauseMenu.isPaused = true;
        PlayerManager.Instance.ResetPlayerXP(level * 5);
        UpdateXPCounter(0);
    }

    public void QuitGame()
    {
        isPlaying = false;
    }
}
