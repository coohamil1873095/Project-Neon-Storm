using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float delay;
    public Transform target;
    private Vector3 vel = Vector3.zero;
    

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = target.position;
        pos.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, pos, ref vel, delay);
    }
}
