using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }
    [SerializeField] private float healthBonus, moveSpeedBonus, weaponDamageBonus, weaponSpreadBonus, weaponRangeBonus, coinGainBonus;
    [SerializeField] private int maxLevel;
    private float totalHealthGain, totalMoveSpeedGain, totalWeaponDamageGain, totalWeaponSpreadGain, totalWeaponRangeGain, totalCoinGain;
    private float healthGainLvl, moveSpeedGainLvl, weaponDamageGainLvl, weaponSpreadGainLvl, weaponRangeGainLvl, coinGainLvl;
    private int totalCoins = 0;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetTotalCoins()
    {
        return totalCoins;
    }
    public void AddCoinToTotal()
    {
        totalCoins += 1;
    }

    public void RemoveCoins(int coins)
    {
        totalCoins -= coins;
    }

    public float GetHealthGain()
    {
        return totalHealthGain;
    }
    public float GetMoveSpeedGain()
    {
        return totalMoveSpeedGain;
    }
    public float GetWeaponDamageGain()
    {
        return totalWeaponDamageGain;
    }
    public float GetWeaponSpreadGain()
    {
        return totalWeaponSpreadGain;
    }
    public float GetWeaponRangeGain()
    {
        return totalWeaponRangeGain;
    }
    public float GetCoinGain()
    {
        return totalCoinGain;
    }

    public int PurchasedHealth(int cost)
    {
        if (totalCoins >= cost) 
        {
            healthGainLvl += 1;
            totalHealthGain += healthBonus;
            RemoveCoins(cost);
            if (healthGainLvl >= maxLevel)
            {
                return 2;
            }
            else {
                return 0;
            }
        }
        else {
            return 1;
        }
    }
    public int PurchasedMoveSpeed(int cost)
    {
        if (totalCoins >= cost) 
        {
            moveSpeedGainLvl += 1;
            totalMoveSpeedGain += moveSpeedBonus;
            RemoveCoins(cost);
            if (moveSpeedGainLvl >= maxLevel)
            {
                return 2;
            }
            else {
                return 0;
            }
        }
        else {
            return 1;
        }
    }
    public int PurchasedWeaponDamage(int cost)
    {
        
        if (totalCoins >= cost) 
        {
            weaponDamageGainLvl += 1;
            totalWeaponDamageGain += weaponDamageBonus;
            RemoveCoins(cost);
            if (weaponDamageGainLvl >= maxLevel)
            {
                return 2;
            }
            else {
                return 0;
            }
        }
        else {
            return 1;
        }
    }
    public int PurchasedWeaponSpread(int cost)
    {
        if (totalCoins >= cost) 
        {
            weaponSpreadGainLvl += 1;
            totalWeaponSpreadGain += weaponSpreadBonus;
            RemoveCoins(cost);
            if (weaponSpreadGainLvl >= maxLevel)
            {
                return 2;
            }
            else {
                return 0;
            }
        }
        else {
            return 1;
        }
    }
    public int PurchasedWeaponRange(int cost)
    {
        if (totalCoins >= cost) 
        {
            weaponRangeGainLvl += 1;
            totalWeaponRangeGain += weaponRangeBonus;
            RemoveCoins(cost);
            if (weaponRangeGainLvl >= maxLevel)
            {
                return 2;
            }
            else {
                return 0;
            }
        }
        else {
            return 1;
        }
    }
    public int PurchasedCoinGain(int cost)
    {
        if (totalCoins >= cost) 
        {
            coinGainLvl += 1;
            totalCoinGain += coinGainBonus;
            RemoveCoins(cost);
            if (coinGainLvl >= maxLevel)
            {
                return 2;
            }
            else {
                return 0;
            }
        }
        else {
            return 1;
        }
    }
}
