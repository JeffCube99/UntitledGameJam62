using UnityEngine;

[CreateAssetMenu(fileName = "AudioUtilities", menuName = "ScriptableObjects/Utilities/AudioUtilities")]

public class AudioUtilities : ScriptableObject
{
    public void PlayAudioSource(AudioSource source)
    {
        source.Play();
    }

    public void StopAudioSource(AudioSource source)
    {
        source.Stop();
    }
}
