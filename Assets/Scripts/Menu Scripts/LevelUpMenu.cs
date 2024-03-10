using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpMenu : MonoBehaviour
{
    public void OnClick_Upgrade() 
    {
        GameManager.Instance.SetPauseStatus(false);
        GameManager.Instance.toggleLevelHUD(false);
        PlayerManager.Instance.ResetPlayerXPBar();
    }
    
}
