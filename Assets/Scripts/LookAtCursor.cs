using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCursor : MonoBehaviour
{
    void Update()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookDirection = cursorPos - transform.position;
        lookDirection.z = 0;
        transform.up = lookDirection.normalized;
    }
}
