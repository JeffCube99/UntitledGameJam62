using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "AudioMixerManager", menuName = "ScriptableObjects/Audio/AudioMixerManager")]
public class AudioMixerManager : ScriptableObject
{
    public AudioMixer mixer;
    
    public void SetSfxLevel(float dB)
    {
        mixer.SetFloat("sfxVol", dB);
    }

    public void SetMusicLevel(float dB)
    {
        mixer.SetFloat("musicVol", dB);
    }

    public void SetMasterLevel(float dB)
    {
        mixer.SetFloat("masterVol", dB);
    }

    public void SetSfxSliderValue(Slider slider)
    {
        SetSliderValueToMixerLevel(slider, "sfxVol");
    }

    public void SetMusicSliderValue(Slider slider)
    {
        SetSliderValueToMixerLevel(slider, "musicVol");
    }

    public void SetMasterSliderValue(Slider slider)
    {
        SetSliderValueToMixerLevel(slider, "masterVol");
    }

    private void SetSliderValueToMixerLevel(Slider slider, string mixerString)
    {
        float value;
        mixer.GetFloat(mixerString, out value);
        slider.value = value;
    }
}
