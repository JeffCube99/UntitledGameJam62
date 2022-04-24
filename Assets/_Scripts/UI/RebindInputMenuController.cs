using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEditor;

public class RebindInputMenuController : MonoBehaviour
{
    public TextMeshProUGUI rebindButtonText;
    public InputActionReference inputActionReference;
    public int bindingIndex;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    void Start()
    {
        RefreshRebindText();
    }

    public void RefreshRebindText()
    {
        rebindButtonText.text = GetInputActionKeyBindingName();
    }

    private string GetInputActionKeyBindingName()
    {
        if (inputActionReference != null)
            return InputControlPath.ToHumanReadableString(inputActionReference.action.bindings[bindingIndex].effectivePath);
        else
            return "No InputActionReference Found";
    }

    public void Rebind()
    {
        if (inputActionReference != null)
        {
            rebindButtonText.text = "Press a Button";
            InputAction action = inputActionReference.action;
            action.Disable();
            rebindingOperation = action.PerformInteractiveRebinding(bindingIndex)
                .OnMatchWaitForAnother(0.1f)
                .WithCancelingThrough("<Keyboard>/backspace")
                .OnComplete(operation => RebindComplete())
                .OnCancel(operation => RebindComplete())
                .Start();
        }
    }

    private void RebindComplete()
    {
        rebindingOperation.Dispose();
        RefreshRebindText();

        InputAction action = inputActionReference.action;
        action.Enable();
    }

}

//[CustomEditor(typeof(RebindInputMenuController))]
//public class customRebindInputMenuControllerInspector : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();
//        RebindInputMenuController rebindInputMenu = (RebindInputMenuController)target;
//        // Because we use this script in a prefab we need to make sure changes made by this custom 
//        // editor apply to the prefab instance.
//        PrefabUtility.RecordPrefabInstancePropertyModifications(rebindInputMenu);
//        if (rebindInputMenu.inputActionReference != null)
//        {
//            InputAction action = rebindInputMenu.inputActionReference.action;
//            List<string> bindingNames = new List<string>();
//            foreach (InputBinding binding in action.bindings)
//            {
//                bindingNames.Add(binding.path);
//            }

//            int index = rebindInputMenu.bindingIndex;
//            string[] bindingOptions = bindingNames.ToArray();
//            index = EditorGUILayout.Popup("Binding", index, bindingOptions);
//            rebindInputMenu.bindingIndex = index;
//        }
//    }
//}
