using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAudioManager : MonoBehaviour
{
    public static MusicAudioManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            // Don't destroy this game object when we go into a new scene
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
