using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [SerializeField] private int XPToLevelUp;
    private int currentXP = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentXP >= XPToLevelUp) 
        {
            GameManager.Instance.OnLevelUp();
        }
    }

    public void AddExperience(int XPVal) 
    {
        currentXP += XPVal;
    }

    public int GetCurrentExperience()
    {
        return currentXP;
    }

    public void SetExperienceGoal(int newXPGoal)
    {
        currentXP = 0;
        XPToLevelUp = newXPGoal;
    }
}
