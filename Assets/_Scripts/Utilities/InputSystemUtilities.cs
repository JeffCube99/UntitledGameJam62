using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputSystemUtilities", menuName = "ScriptableObjects/Utilities/InputSystemUtilities")]
public class InputSystemUtilities : ScriptableObject
{
    public void ResetAllBindings(InputActionAsset inputActionAsset)
    {
        foreach (InputActionMap map in inputActionAsset.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }
    }
}
