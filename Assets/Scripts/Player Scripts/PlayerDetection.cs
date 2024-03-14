using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] public float playerWeaponDamage;
    [SerializeField] private float playerWeaponCooldown;
    [SerializeField] public GameObject lightningEffect;
    //[SerializeField] public GameObject lightningEffectEnd;
    public float fov;   // How far you want the cone to extend from the player
    [Range(0, 360)] public float fovAngle;      // Size of cone (in degrees)
    private Collider2D[] targets;
    private bool isLeftMouseButtonDown = false;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Left mouse button is pressed
        {
            isLeftMouseButtonDown = true;
        }
        else if (Input.GetMouseButtonUp(0))  // Left mouse button is released
        {
            isLeftMouseButtonDown = false;
        }

        if (isLeftMouseButtonDown)
        {
            
            targets = Physics2D.OverlapCircleAll(transform.position, fov, LayerMask.GetMask("Enemy"));
            
            foreach (Collider2D collider in targets) 
            {
                float signedAngle = Vector3.Angle(
                transform.up,
                collider.transform.position - transform.position);

                if (Mathf.Abs(signedAngle) < fovAngle / 2 && !collider.GetComponent<Enemy>().EnemyBeingDamaged())
                {
                    StartCoroutine(ApplyDamage(collider));
                    
                    GameObject lightning = Instantiate(lightningEffect, GameObject.Find("Lightning").transform);
                    Destroy(lightning, 0.25f);
                    lightning.transform.Find("LightningStart").transform.position = transform.position;
                    lightning.transform.Find("LightningEnd").transform.position = collider.transform.position;
                }
            }

            
        }
    }

    public void ApplyWeaponUpgrade(float weaponDamageGain, float weaponSpreadGain, float weaponRangeGain)
    {
        playerWeaponDamage += playerWeaponDamage * weaponDamageGain / 10;
        fovAngle += fovAngle * weaponSpreadGain / 20;
        fov += fov * weaponRangeGain / 10;
    }

    IEnumerator ApplyDamage(Collider2D col)
    {
        col.GetComponent<Enemy>().DamageEnemy(playerWeaponDamage);
        yield return null;
    }
}