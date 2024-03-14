using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text errorMSG;
    [SerializeField] private GameObject[] maxLevelObjs;
    [SerializeField] private int healthCost, moveSpeedCost, weaponDamageCost, weaponSpreadCost, weaponRangeCost, coinGainCost;
    private bool isErrorMSGDisplaying = false;

    void Update()
    {
        coinsText.SetText("Coins: " + ShopManager.Instance.GetTotalCoins());
    }
    
    public void OnClick_Back() 
    {
        MenuManager.OpenMenu(Menu.MAIN_MENU, gameObject);
        errorMSG.gameObject.SetActive(false);
        isErrorMSGDisplaying = false;
    }

    IEnumerator DisplayErrorMSG()
    {
        if (!isErrorMSGDisplaying) 
        {
            errorMSG.gameObject.SetActive(true);
            isErrorMSGDisplaying = true;
            yield return new WaitForSeconds(3);
            errorMSG.gameObject.SetActive(false);
            isErrorMSGDisplaying = false;
        }
    }

    public void OnClick_HealthPurchase()
    {
        int result = ShopManager.Instance.PurchasedHealth(healthCost);
        switch (result)
        {
            case 1:
                StartCoroutine(DisplayErrorMSG());
                break;
            case 2:
                maxLevelObjs[0].SetActive(true);
                break;
        }
    }
    public void OnClick_MoveSpeedPurchase()
    {
        int result = ShopManager.Instance.PurchasedMoveSpeed(healthCost);
        switch (result)
        {
            case 1:
                StartCoroutine(DisplayErrorMSG());
                break;
            case 2:
                maxLevelObjs[1].SetActive(true);
                break;
        }
    }
    public void OnClick_WeaponDamagePurchase()
    {
        int result = ShopManager.Instance.PurchasedWeaponDamage(healthCost);
        switch (result)
        {
            case 1:
                StartCoroutine(DisplayErrorMSG());
                break;
            case 2:
                maxLevelObjs[2].SetActive(true);
                break;
        }
    }
    public void OnClick_WeaponSpreadPurchase()
    {
        int result = ShopManager.Instance.PurchasedWeaponSpread(healthCost);
        switch (result)
        {
            case 1:
                StartCoroutine(DisplayErrorMSG());
                break;
            case 2:
                maxLevelObjs[3].SetActive(true);
                break;
        }
    }
    public void OnClick_WeaponRangePurchase()
    {
        int result = ShopManager.Instance.PurchasedWeaponRange(healthCost);
        switch (result)
        {
            case 1:
                StartCoroutine(DisplayErrorMSG());
                break;
            case 2:
                maxLevelObjs[4].SetActive(true);
                break;
        }
    }
    public void OnClick_CoinGainPurchase()
    {
        int result = ShopManager.Instance.PurchasedCoinGain(healthCost);
        switch (result)
        {
            case 1:
                StartCoroutine(DisplayErrorMSG());
                break;
            case 2:
                maxLevelObjs[5].SetActive(true);
                break;
        }
    }
}
