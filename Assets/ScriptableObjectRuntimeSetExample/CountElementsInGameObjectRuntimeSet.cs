using UnityEngine;

public class CountElementsInGameObjectRuntimeSet : MonoBehaviour
{
    public GameObjectRuntimeSet runtimeSet;

    void Start()
    {
        Debug.Log($"RuntimeSet '{runtimeSet.name}' contains {runtimeSet.items.Count} elements");
    }
}
