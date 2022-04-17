using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectOnContactComponent : MonoBehaviour
{
    public bool detectContactWithPlayer;
    public bool detectContactWithEnemy;
    public bool detectContactWithEnvironment;

    public UnityEvent OnContact;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (detectContactWithPlayer && collision.CompareTag("Player"))
        {
            OnContact.Invoke();
        }
        else if (detectContactWithEnemy && collision.CompareTag("Enemy"))
        {
            OnContact.Invoke();
        }

        else if (detectContactWithEnvironment && collision.CompareTag("Environment"))
        {
            OnContact.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (detectContactWithPlayer && collision.gameObject.CompareTag("Player"))
        {
            OnContact.Invoke();
        }

        else if (detectContactWithEnemy && collision.gameObject.CompareTag("Enemy"))
        {
            OnContact.Invoke();
        }

        else if (detectContactWithEnvironment && collision.gameObject.CompareTag("Environment"))
        {
            OnContact.Invoke();
        }
    }
}
