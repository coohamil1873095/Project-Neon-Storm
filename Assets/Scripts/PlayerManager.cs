using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    [SerializeField] private GameObject player;
    
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

    public void GivePlayerXP(int XPVal) 
    {
        player.GetComponent<PlayerXP>().AddExperience(XPVal);
    }

    public void ResetPlayerXP(int newXPGoal)
    {
        player.GetComponent<PlayerXP>().SetExperienceGoal(newXPGoal);
    }
}
