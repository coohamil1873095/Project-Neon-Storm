using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static bool isPlaying { get; private set; }
    private bool isPaused;
    private Canvas gameHUD;
    private Canvas levelHUD;
    private Canvas abilityHUD;
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
        abilityHUD = GameObject.Find("Ability HUD").GetComponent<Canvas>();
        toggleLevelHUD(false);
        isPlaying = true;
        GetComponent<TimerController>().StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            isPaused = true;
            SetPauseStatus(true);
            toggleGameHUD(false);
            toggleAbilityHUD(false);
            MenuManager.OpenMenu(Menu.PAUSE_MENU, null);
        }
    }

    public void toggleGameHUD(bool activeStatus)
    {
        gameHUD.enabled = activeStatus;
    }

    public void toggleLevelHUD(bool activeStatus)
    {
        levelHUD.enabled = activeStatus;
    }

    public void toggleAbilityHUD(bool activeStatus)
    {
        abilityHUD.enabled = activeStatus;
    }

    public void OnLevelUp()
    {
        level++;
        Debug.Log("Player Leveled Up");
        LevelText.SetText("Level: " + level);
        toggleLevelHUD(true);
        SetPauseStatus(true);
        PlayerManager.Instance.ResetPlayerXP(level * 5);
    }

    public void SetPauseStatus(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else 
        {
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    public bool GameIsPaused()
    {
        return isPaused;
    }

    public void QuitGame()
    {
        isPlaying = false;
    }
}
