using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLevelManager : MonoBehaviour
{
    public static AbilityLevelManager Instance { get; private set; }
    [SerializeField] private int Ability1MaxLevel, Ability2MaxLevel, Ability3MaxLevel, Ability4MaxLevel;
    [SerializeField] private GameObject Ability1Blocker, Ability2Blocker, Ability3Blocker, Ability4Blocker;
    private int ability1Level, ability2Level, ability3Level, ability4Level;

    
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    
    public void Ability1LevelUp()
    {
        ability1Level += 1;
        if (ability1Level >= Ability1MaxLevel)
        {
            Ability1Blocker.SetActive(true);
        }
    }
    public void Ability2LevelUp()
    {
        ability2Level += 1;
        if (ability2Level >= Ability2MaxLevel)
        {
            Ability2Blocker.SetActive(true);
        }
    }
    public void Ability3LevelUp()
    {
        ability3Level += 1;
        if (ability3Level >= Ability3MaxLevel)
        {
            Ability3Blocker.SetActive(true);
        }
    }
    public void Ability4LevelUp()
    {
        ability4Level += 1;
        if (ability4Level >= Ability4MaxLevel)
        {
            Ability4Blocker.SetActive(true);
        }
    }
}
