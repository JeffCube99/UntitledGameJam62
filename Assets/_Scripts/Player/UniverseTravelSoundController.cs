using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseTravelSoundController : MonoBehaviour
{
    public List<SoundAsset> universeTravelSoundAssets;
    public void OnSuccessfulTravel(int universeIndex)
    {
        universeTravelSoundAssets[universeIndex].PlayAudioAtAudioListener();
    }
}
