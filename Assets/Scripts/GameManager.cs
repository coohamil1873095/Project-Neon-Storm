using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static bool isPlaying { get; private set; }
    public static bool playerWin { get; private set; }
    private bool isPaused;
    [SerializeField] private GameObject pauseMenu, gameOverMenu, levelUI;
    [SerializeField] public Button ultUnlock;
    private AudioSource levelSFX;
    [SerializeField] private AudioClip levelUpSFX;
    private Canvas gameHUD, levelHUD, abilityHUD;
    [SerializeField] private TMP_Text LevelText;
    [SerializeField] private TMP_Text CoinsText;
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
        levelHUD = levelUI.GetComponent<Canvas>();
        levelSFX = levelUI.GetComponent<AudioSource>();
        abilityHUD = GameObject.Find("Ability HUD").GetComponent<Canvas>();
        toggleLevelHUD(false);
        isPlaying = true;
        SetPauseStatus(false);
        GetComponent<TimerController>().StartTimer();

        ApplyUpgrades();
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

            pauseMenu.SetActive(true);
        }

        CoinsText.SetText("Coins: " + ShopManager.Instance.GetTotalCoins());
    }

    public void ApplyUpgrades()
    {
        PlayerManager.Instance.ApplyPlayerUpgrades(
            ShopManager.Instance.GetHealthGain(), 
            ShopManager.Instance.GetMoveSpeedGain(), 
            ShopManager.Instance.GetWeaponDamageGain(), 
            ShopManager.Instance.GetWeaponSpreadGain(), 
            ShopManager.Instance.GetWeaponRangeGain()
        );

    }

    public void GainCoin()
    {
        ShopManager.Instance.AddCoinToTotal();
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
        LevelText.SetText("LVL: " + level);
        toggleLevelHUD(true);
        levelSFX.PlayOneShot(levelUpSFX);
        SetPauseStatus(true);
        PlayerManager.Instance.ResetPlayerXP(level * 5); // level * 5
        if (level == 5)
        {
            ultUnlock.gameObject.SetActive(true);
        }
        EnemyManager.Instance.IncreaseEnemyHealth(level / 2);
        ShopManager.Instance.AddCoinToTotal();
    }

    public int GetCurrentLevel()
    {
        return level;
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
        playerWin = false;
        PlayerManager.Instance.ResetPlayer();
    }

    public void RestartGame()
    {
        Start();
    }

    public void EndCurrentGame(bool winStatus)
    {
        isPlaying = false;
        toggleGameHUD(false);
        toggleLevelHUD(false);
        toggleAbilityHUD(false);
        SetPauseStatus(true);

        playerWin = winStatus;

        gameOverMenu.SetActive(true);
    }
}
