using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "NewInputActionEvent", menuName = "ScriptableObjects/Events/InputActionEvent")]
public class InputActionEvent : ScriptableObject
{
    public InputActionReference inputActionReference;
    public bool raiseOnStarted;
    public bool raiseOnPerformed;
    public bool raiseOnCancelled;

    private readonly List<InputActionEventListener> eventListeners = new List<InputActionEventListener>();

    public void Raise(InputAction.CallbackContext context)
    {
        // We go through the listeners in reverse in case some destroy themselves after the event is raised.
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised(context);
        }
    }

    public void RegisterListener(InputActionEventListener listener)
    {
        // Check to see that the eventListeners list does not already contain the target listener
        if (!eventListeners.Contains(listener))
        {
            eventListeners.Add(listener);
        }
    }

    public void UnregisterListener(InputActionEventListener listener)
    {
        // Check to see that the eventListeners list contains the target listener
        if (eventListeners.Contains(listener))
        {
            eventListeners.Remove(listener);
        }
    }

    private void OnEnable()
    {
        if (inputActionReference != null)
        {
            InputAction action = inputActionReference.action;
            // We need to make sure the action is enabled otherwise the input will not be processed.
            action.Enable();

            if (raiseOnStarted)
                action.started += Raise;

            if (raiseOnPerformed)
                action.performed += Raise;

            if (raiseOnCancelled)
                action.canceled += Raise;
        }
    }

    private void OnDisable()
    {
        if (inputActionReference != null)
        {
            InputAction action = inputActionReference.action;
            if (raiseOnStarted)
                action.started -= Raise;

            if (raiseOnPerformed)
                action.performed -= Raise;

            if (raiseOnCancelled)
                action.canceled -= Raise;
        }
    }
}
