using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] public float playerWeaponDamage;
    [SerializeField] private float playerWeaponCooldown;
    [SerializeField] public GameObject lightningEffect;
    [SerializeField] public GameObject lightningEffectEnd;
    public float fov;   // How far you want the cone to extend from the player
    [Range(0, 360)] public float fovAngle;      // Size of cone (in degrees)
    private Collider2D target;
    private bool isLeftMouseButtonDown = false;
    private bool dmgActive = false;

    private void OnDrawGizmos()
    {
        if (isLeftMouseButtonDown)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            mousePosition.y = 0.5f;
            Handles.color = new Color(1, 0, 0, 0.3f);
            Handles.DrawSolidArc(transform.position, transform.forward, Quaternion.AngleAxis(-fovAngle / 2f, transform.forward) * transform.up, fovAngle, fov);
            lightningEffect.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Left mouse button is pressed
        {
            isLeftMouseButtonDown = true;
        }
        else if (Input.GetMouseButtonUp(0))  // Left mouse button is released
        {
            lightningEffect.SetActive(false);
            isLeftMouseButtonDown = false;
        }

        if (isLeftMouseButtonDown)
        {
            target = Physics2D.OverlapCircle(transform.position, fov);
            if (target && target.tag == "Enemy")
            {
                float signedAngle = Vector3.Angle(
                    transform.up,
                    target.transform.position - transform.position);


                if (Mathf.Abs(signedAngle) < fovAngle / 2 && !dmgActive)
                {
                    StartCoroutine(ApplyDamage());
                    lightningEffectEnd.transform.position = target.transform.position;
                }
            }
        }
    }

    IEnumerator ApplyDamage()
    {
        target.GetComponent<Enemy>().DamageEnemy(playerWeaponDamage);
        dmgActive = true;
        yield return new WaitForSeconds(playerWeaponCooldown);
        dmgActive = false;
    }
}