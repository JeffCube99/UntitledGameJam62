using UnityEngine;

public abstract class AddItemToRuntimeSet<T> : MonoBehaviour
{
    [SerializeField] private RuntimeSet<T> runtimeSet;

    // Child classes get to decide how to extract the item from the game
    // object the component is attached to.
    protected abstract T GetItemFromGameObject(GameObject gameObject);

    private void OnEnable()
    {
        T item = GetItemFromGameObject(gameObject);
        if (item == null)
        {
            ThrowExceptionForMissingItem(gameObject);
        }
        else
        {
            runtimeSet.Add(item);
        }
    }

    private void OnDestroy()
    {
        T item = GetItemFromGameObject(gameObject);
        if (item == null)
        {
            ThrowExceptionForMissingItem(gameObject);
        }
        else
        {
            runtimeSet.Remove(item);
        }
    }

    private void ThrowExceptionForMissingItem(GameObject gameObject)
    {
        Debug.LogError($"The game object '{gameObject}' does not contain elements required for the runtime set {runtimeSet.GetType().Name}");
    }
}
