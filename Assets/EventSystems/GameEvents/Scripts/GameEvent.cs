using System.Collections.Generic;
using UnityEngine;

// GameEvent
// GameEventListeners subscribe to the GameEvent asset
// Other Scripts call the GameEvent's Raise() method

// The CreateAssetMenu attribute allows us to create scriptable object assets in the editor
// In the Editor: Right Click > Create > ScriptableObjects > GameEvent
[CreateAssetMenu(fileName = "New GameEvent", menuName = "ScriptableObjects/Events/GameEvent")]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> eventListeners = new List<GameEventListener>();

    public void Raise()
    {
        // We go through the listeners in reverse in case some destroy themselves after the event is raised.
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        // Check to see that the eventListeners list does not already contain the target listener
        if (!eventListeners.Contains(listener))
        {
            eventListeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListener listener)
    {
        // Check to see that the eventListeners list contains the target listener
        if (eventListeners.Contains(listener))
        {
            eventListeners.Remove(listener);
        }
    }
}
