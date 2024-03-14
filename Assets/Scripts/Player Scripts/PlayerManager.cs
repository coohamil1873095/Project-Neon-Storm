using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    [SerializeField] private GameObject player;

    private PlayerXP playerXP;
    private PlayerPowers playerPowers;
    private PlayerHealth playerHealth;
    private PlayerDetection playerDetection;
    private PlayerMove playerMove;
    
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
        playerXP = player.GetComponent<PlayerXP>();
        playerPowers = player.GetComponent<PlayerPowers>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerDetection = player.GetComponent<PlayerDetection>();
        playerMove = player.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyPlayerUpgrades(float healthGain, float moveSpeedGain, float weaponDamageGain, float weaponSpreadGain, float weaponRangeGain)
    {
        playerHealth.ApplyHealthUpgrade(healthGain);
        playerMove.ApplyMoveSpeedUpgrade(moveSpeedGain);
        playerDetection.ApplyWeaponUpgrade(weaponDamageGain, weaponSpreadGain, weaponRangeGain);
    }

    public void DamagePlayerHealth(float damageAmount)
    {
        playerHealth.DamagePlayer(damageAmount);
    }

    public void GivePlayerXP(int XPVal) 
    {
        playerXP.AddExperience(XPVal);
    }

    public void ResetPlayerXP(int newXPGoal)
    {
        playerXP.SetExperienceGoal(newXPGoal);
    }

    public void ResetPlayerXPBar() 
    {
        playerXP.UpdateExperienceBar(0, playerXP.GetExperienceGoal());
    }

    public void ResetPlayer()
    {
        ResetPlayerXP(playerXP.GetStartingXPGoal());
        ResetPlayerXPBar();
        playerPowers.ResetPlayerPowers();
        playerHealth.ResetPlayerHealth();
    }
}
