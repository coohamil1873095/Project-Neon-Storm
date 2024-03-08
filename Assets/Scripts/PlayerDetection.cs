using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerDetection : MonoBehaviour
{
    public float fov;   //how far you want the cone to extend from player
    [Range(0, 360)] public float fovAngle;      //size of cone (in degrees)
    private Collider2D target;

    private void OnDrawGizmos()
    {
        Handles.color = new Color(1, 0, 0, 0.3f);
        Handles.DrawSolidArc(transform.position , transform.forward , Quaternion.AngleAxis(-fovAngle/2f , transform.forward)*transform.up , fovAngle , fov);
        //Handles.DrawSolidDisc(transform.position , transform.forward , fov);
    }

    private void Update()
    {
        target = Physics2D.OverlapCircle(transform.position, fov);
        if (target && target.tag == "Enemy")
        {
            float signedAngle = Vector3.Angle(
            transform.up,
            target.transform.position - transform.position);
            if (Mathf.Abs(signedAngle) < fovAngle / 2)
            {
                //Debug.Log("found an enemy!");
                target.GetComponent<Enemy>().DamageEnemy(1);
                //Debug.Log("found an enemy!");
            }
        }
    }
}
