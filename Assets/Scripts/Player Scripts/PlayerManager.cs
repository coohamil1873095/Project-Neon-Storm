using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    [SerializeField] private GameObject player;

    private PlayerXP playerXP;
    
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
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
