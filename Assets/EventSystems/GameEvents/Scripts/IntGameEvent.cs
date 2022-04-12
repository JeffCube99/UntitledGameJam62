using UnityEngine;

// GameEvent
// IntGameEventListeners subscribe to the IntGameEvent asset
// Other Scripts call the IntGameEvent's Raise(int) method

// The CreateAssetMenu attribute allows us to create scriptable object assets in the editor
// In the Editor: Right Click > Create > ScriptableObjects > IntGameEvent
[CreateAssetMenu(fileName = "New IntGameEvent", menuName = "ScriptableObjects/Events/IntGameEvent")]
public class IntGameEvent : GenericGameEvent<int>
{

}
