using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSelector : MonoBehaviour
{
    public string value;

    public bool isClicked;

    public Color selectedColor;
    public Color unselectedColor;

    public Image image;

    public StringReference mouseState;
    public StringReference selectedHotbarItem;


    public void Start()
    {
        image = gameObject.GetComponent<Image>();
        image.color = unselectedColor;
        isClicked = false;
    }

    public void OnThisHotbarItemClicked()
    {
        // On Deselect
        if (isClicked)
        {
            image.color = unselectedColor;
            selectedHotbarItem.SetDefault();
            mouseState.SetDefault();
        }
        // On Select
        else
        {
            image.color = selectedColor;
            selectedHotbarItem.Value = value;
            mouseState.Value = "Edit"; 
        }

        isClicked = !isClicked;
    }

    public void OnHotbarItemClicked()
    {
        if (selectedHotbarItem.Value != value)
        {
            image.color = unselectedColor;
            isClicked = false;
        }
            
    }
}
