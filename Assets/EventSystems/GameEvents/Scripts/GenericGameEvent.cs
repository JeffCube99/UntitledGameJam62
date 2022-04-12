using System.Collections.Generic;
using UnityEngine;

// GenericGameEvent
// GenericGameEventListeners subscribe to the GenericGameEvent asset
// Other Scripts call the GameEvent's Raise() method with a parameter of type T
public class GenericGameEvent<T> : ScriptableObject
{
    private readonly List<GenericGameEventListener<T>> eventListeners = new List<GenericGameEventListener<T>>();

    public void Raise(T value)
    {
        // We go through the listeners in reverse in case some destroy themselves after the event is raised.
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised(value);
        }
    }

    public void RegisterListener(GenericGameEventListener<T> listener)
    {
        // Check to see that the eventListeners list does not already contain the target listener
        if (!eventListeners.Contains(listener))
        {
            eventListeners.Add(listener);
        }
    }

    public void UnregisterListener(GenericGameEventListener<T> listener)
    {
        // Check to see that the eventListeners list contains the target listener
        if (eventListeners.Contains(listener))
        {
            eventListeners.Remove(listener);
        }
    }
}
