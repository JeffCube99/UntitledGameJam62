using UnityEngine;

public class AddGameObjectToRuntimeSet : AddItemToRuntimeSet<GameObject>
{
    protected override GameObject GetItemFromGameObject(GameObject gameObject)
    {
        return gameObject;
    }
}
