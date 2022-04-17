using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    public UnityEvent OnParticlesPlay; 

    private void OnEnable()
    {
        OnParticlesPlay.Invoke();
        particleSystem.Play();
    }
}
