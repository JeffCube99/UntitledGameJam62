using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;

    private void OnEnable()
    {
        particleSystem.Play();
    }
}