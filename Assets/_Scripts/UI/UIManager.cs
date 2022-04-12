using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public List<GameObject> uiElements;

    private void SetAllUiElementsActive(bool isActive)
    {
        for (int i = 0; i < uiElements.Count; i++)
        {
            uiElements[i].SetActive(isActive);
        }
    }

    public void HideAllUiElements()
    {
        SetAllUiElementsActive(false);
    }

    public void ShowAllUiElements()
    {
        SetAllUiElementsActive(true);
    }

    public void HideSingleUiElement(int elementIndex)
    {
        uiElements[elementIndex].SetActive(false);
    }

    public void ShowSingleUiElement(int elementIndex)
    {
        uiElements[elementIndex].SetActive(true);
    }

    public void ShowSingleUiElementOnly(int elementIndex)
    {
        HideAllUiElements();
        uiElements[elementIndex].SetActive(true);
    }

    /// <summary>
    /// This function takes in a string with ui element indices separated by commas.
    /// For example : "1,2,3,12"
    /// We do this so that this function can be exposed to UnityEvents since it only has one string parameter.
    /// </summary>
    /// <param name="elementList"></param>
    public void ShowMultipleUiElementsOnly(string elementList)
    {
        HideAllUiElements();
        foreach (var indexString in elementList.Split(','))
        {
            int index = int.Parse(indexString);
            if (index < uiElements.Count)
                uiElements[int.Parse(indexString)].SetActive(true);
        }
    }

}
