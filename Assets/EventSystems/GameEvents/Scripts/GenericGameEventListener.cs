using UnityEngine;
using UnityEngine.Events;

public class GenericGameEventListener<T> : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GenericGameEvent<T> Event;

    [Tooltip("Response to invoke when event is raised.")]
    public UnityEvent<T> Response;

    private void OnEnable()
    {
        // Registers instance to the GameEvent so OnEventRaised() is called if the GameEvent is raised
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        // Unregisters instance to the GameEvent since OnEventRaised() does not need to be invoked when disabled.
        Event.UnregisterListener(this);
    }

    // We invoke the UnityEvent when we the GameEvent is raised
    public void OnEventRaised(T value)
    {
        Response.Invoke(value);
    }
}
