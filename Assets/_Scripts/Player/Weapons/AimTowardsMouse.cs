using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTowardsMouse : MonoBehaviour
{
    private void FixedUpdate()
    {
        Vector3 mousePosition = MouseUtilities.GetMousePosition();
        transform.right = mousePosition - transform.position;
    }
}
