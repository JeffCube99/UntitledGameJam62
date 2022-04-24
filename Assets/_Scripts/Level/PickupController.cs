using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupController : MonoBehaviour
{
    public UnityEvent OnPickup;
    private bool pickedUp;

    public void ResetPickup()
    {
        pickedUp = false;
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        if (pickedUp)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pickedUp = true;
            OnPickup.Invoke();
            gameObject.SetActive(false);
        }
    }
}
