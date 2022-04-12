using UnityEngine;
using UnityEditor.Presets;

[CreateAssetMenu(fileName = "NewSoundAsset", menuName = "ScriptableObjects/Audio/SoundAsset")]

public class SoundAsset : ScriptableObject
{
    public Preset audioSourcePreset;
    public AudioSourceManager audioSourceManager;

    // Plays audio from audio sources at the specified position
    public void PlayAudioAtLocation(Transform location)
    {
        AudioSource source = audioSourceManager.Get3DAudioSourceAtLocation(location);
        audioSourcePreset.ApplyTo(source);
        source.Play();
    }

    // Plays audio from audio sources at the position of the audio listener
    public void PlayAudioAtAudioListener()
    {
        AudioSource source = audioSourceManager.Get2DAudioSource();
        audioSourcePreset.ApplyTo(source);
        source.Play();
    }

    // Plays audio from special music audio source that persists between
    // scenes
    public void PlayAudioAsMusic()
    {
        AudioSource source = audioSourceManager.GetMusicAudioSource();
        audioSourcePreset.ApplyTo(source);
        source.Play();
    }

    // Plays audio from a specified audio source
    // Note this method is inefficient since we apply the audio source preset
    // to the target source each time this is called.
    // Just apply the audioSourcePreset to the audio source and use
    // AudioUtilities.PlayAudioSource instead if you want to use scriptable objects.
    public void PlayAudioFromAudioSource(AudioSource source)
    {
        audioSourcePreset.ApplyTo(source);
        source.Play();
    }
}
