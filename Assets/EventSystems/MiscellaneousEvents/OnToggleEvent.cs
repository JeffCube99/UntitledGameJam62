using UnityEngine;
using UnityEngine.Events;

public class OnToggleEvent : MonoBehaviour
{
    public UnityEvent onToggleTrue;
    public UnityEvent onToggleFalse;


    public void RaiseToggleEvent(bool isTrueToggle)
    {
        if (isTrueToggle)
        {
            onToggleTrue.Invoke();
        }
        else
        {
            onToggleFalse.Invoke();
        }
    }
}
