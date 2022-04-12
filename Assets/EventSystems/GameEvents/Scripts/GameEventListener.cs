using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameEvent Event;

    [Tooltip("Response to invoke when event is raised.")]
    public UnityEvent Response;

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
    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
