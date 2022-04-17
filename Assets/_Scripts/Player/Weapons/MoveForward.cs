using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;

    private void FixedUpdate()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
