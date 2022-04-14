using UnityEngine;

// The CreateAssetMenu attribute allows us to create scriptable object assets in the editor
// In the Editor: Right Click > Create > ScriptableObjects > RuntimeSets > GameObjectRuntimeSet
[CreateAssetMenu(fileName = "NewGameObjectRuntimeSet", menuName = "ScriptableObjects/RuntimeSets/GameObjectRuntimeSet")]
public class GameObjectRuntimeSet : RuntimeSet<GameObject>
{

}
