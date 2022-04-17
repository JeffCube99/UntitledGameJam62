using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class MouseUtilities
{
    // Projects a ray from the camera through the mouse cursor.
    // Gets the position of the raycast that intersects along the x and z plane.
    public static Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        // create a plane at 0,0,0 whose normal points to +Z:
        Plane xyPlane = new Plane(Vector3.forward, Vector3.zero);
        // Plane.Raycast stores the distance from ray.origin to the hit point in this variable:
        float distance = 0;
        // if the ray hits the plane...
        if (xyPlane.Raycast(ray, out distance))
        {
            // get the hit point:
            return ray.GetPoint(distance);
        }
        else
        {
            // Return Zero if no intersection occurs
            return Vector3.zero;
        }
    }
}
