using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerXP : MonoBehaviour
{
    [SerializeField] private int startingXPGoal;
    [SerializeField] private Slider slider;
    private int XPToLevelUp;
    private int currentXP = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        XPToLevelUp = startingXPGoal;
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
        UpdateExperienceBar(currentXP, XPToLevelUp);
    }

    public void UpdateExperienceBar(float currentVal, float maxVal)
    {
        slider.value = currentVal / maxVal;
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

    public int GetExperienceGoal()
    {
        return XPToLevelUp;
    }

    public int GetStartingXPGoal()
    {
        return startingXPGoal;
    }
}
