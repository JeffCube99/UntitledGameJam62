using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "NewSoundAsset", menuName = "ScriptableObjects/Audio/SoundAsset")]

public class SoundAsset : ScriptableObject
{
    public AudioClip audioClip;
    public float volume;
    public AudioMixerGroup audioMixerGroup;
    public AudioSourceManager audioSourceManager;

    // Plays audio from audio sources at the specified position
    public void PlayAudioAtLocation(Transform location)
    {
        AudioSource source = audioSourceManager.Get3DAudioSourceAtLocation(location);
        PlayAudioFromAudioSource(source);
    }

    // Plays audio from audio sources at the position of the audio listener
    public void PlayAudioAtAudioListener()
    {
        AudioSource source = audioSourceManager.Get2DAudioSource();
        PlayAudioFromAudioSource(source);
    }

    // Plays audio from special music audio source that persists between
    // scenes
    public void PlayAudioAsMusic()
    {
        AudioSource source = audioSourceManager.GetMusicAudioSource();
        PlayAudioFromAudioSource(source);
    }

    // Plays audio from a specified audio source using the settings from the SoundAsset
    public void PlayAudioFromAudioSource(AudioSource source)
    {
        source.Stop();
        source.outputAudioMixerGroup = audioMixerGroup;
        source.volume = volume;
        source.clip = audioClip;
        source.Play();
    }
}
