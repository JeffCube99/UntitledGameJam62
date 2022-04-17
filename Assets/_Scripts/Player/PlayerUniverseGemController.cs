using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerUniverseGemController : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    [SerializeField] private GameState gameState;
    public float travelDelay;
    [SerializeField] private ObjectPool travelChargeParticlesObjectPool;
    [SerializeField] private ObjectPool travelSuccessParticlesObjectPool;


    public UnityEvent OnUniverseTravelBegin;
    public UnityEvent<int> OnUniverseTravelSuccess;

    private bool CanTravelToUniverse(int universeIndex)
    {
        return !playerState.isDead
            && !playerState.isTraveling
            && (gameState.currentUniverseIndex != universeIndex)
            && playerState.universeCrystals.Contains(universeIndex);
    }

    public void TravelToUniverse(int universeIndex)
    {
        if (CanTravelToUniverse(universeIndex))
        {
            playerState.isTraveling = true;
            StartCoroutine(Travel(universeIndex));
        }
    }

    IEnumerator Travel(int universeIndex)
    {
        OnUniverseTravelBegin.Invoke();
        if (travelChargeParticlesObjectPool != null)
            travelChargeParticlesObjectPool.Instantiate(transform.position, transform.rotation);
        yield return new WaitForSeconds(travelDelay);
        if (travelSuccessParticlesObjectPool != null)
            travelSuccessParticlesObjectPool.Instantiate(transform.position, transform.rotation);
        gameState.currentUniverseIndex = universeIndex;
        OnUniverseTravelSuccess.Invoke(universeIndex);
        playerState.isTraveling = false;
    }

    public void CancelUniverseTravel()
    {
        StopAllCoroutines();
    }


}
