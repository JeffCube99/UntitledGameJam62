using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSourceManager", menuName = "ScriptableObjects/Audio/AudioSourceManager")]
public class AudioSourceManager : ScriptableObject
{
    public ObjectPool audioSourceObjectPool;
    public int maxAudioSources2D;
    public GameObject musicAudioSourcePrefab;

    private Queue<AudioSource> audioSources2D;
    private AudioSource musicAudioSource;

    private AudioSource InitializeMusicAudioSource()
    {
        GameObject musicGameObject = Instantiate(musicAudioSourcePrefab);
        return musicGameObject.GetComponent<AudioSource>();
    }

    public AudioSource GetMusicAudioSource()
    {
        if (musicAudioSource == null)
        {
            musicAudioSource = InitializeMusicAudioSource();
        }
        return musicAudioSource;
    }


    private void Initialize2DAudioSources()
    {
        AudioListener listener = FindObjectOfType<AudioListener>();

        if (listener == null)
        {
            Debug.LogError("Cannot find a audio listener in the scene!");
            return;
        }

        GameObject audioListenerGameObject = listener.gameObject;

        if (audioSources2D == null)
        {
            audioSources2D = new Queue<AudioSource>();
        }    
        else
        {
            foreach (AudioSource queueSource in audioSources2D)
            {
                if (queueSource != null)
                {
                    Destroy(queueSource);
                }
            }
            audioSources2D.Clear();
        }

        for (int i = 0; i < maxAudioSources2D; i++)
        {
            AudioSource source = audioListenerGameObject.AddComponent(typeof(AudioSource)) as AudioSource;
            audioSources2D.Enqueue(source);
        }
    }

    public AudioSource Get2DAudioSource()
    {
        // If no 2d audiosources exist we create them
        if (audioSources2D == null || audioSources2D.Count == 0)
        {
            Initialize2DAudioSources();
        }

        AudioSource source = audioSources2D.Dequeue();

        // If a source in the queue is null it is likely the previous
        // listener we attach our sources to has been destroyed so we re initialize the 
        // 2d audio sources
        if (source == null)
        {
            Initialize2DAudioSources();
            source = audioSources2D.Dequeue();
        }
        audioSources2D.Enqueue(source);
        return source;
    }

    public AudioSource Get3DAudioSourceAtLocation(Transform location)
    {
        GameObject audioSourceObject = audioSourceObjectPool.Instantiate(location.position, location.rotation);
        AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();
        return audioSource;
    }

}
