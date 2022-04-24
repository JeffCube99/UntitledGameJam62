using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseTravelSoundController : MonoBehaviour
{
    public List<SoundAsset> universeTravelSoundAssets;
    public List<SoundAsset> universeMusicSoundAssets;
    public GameState gameState;

    public void OnSuccessfulTravel(int universeIndex)
    {
        universeTravelSoundAssets[universeIndex].PlayAudioAtAudioListener();
        universeMusicSoundAssets[universeIndex].PlayAudioAsMusic();
    }

    public void OnRespawnTravel()
    {
        universeMusicSoundAssets[gameState.currentUniverseIndex].PlayAudioAsMusic();
    }
}
