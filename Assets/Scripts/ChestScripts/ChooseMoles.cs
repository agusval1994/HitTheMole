using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMoles : MonoBehaviour
{
    ChestManager manager;
    Item item;
    Toggle toggleButton;

    void Start()
    {
        item = GetComponent<Item>();
        manager = FindObjectOfType<ChestManager>();
        toggleButton = GetComponent<Toggle>();
    }

    public void SelectDeselectMole()
    {
        if (toggleButton.isOn)
        {
            manager.selectedMoles.Add(item.id, true);
            return;
        }
        else
        {
            manager.selectedMoles.Remove(item.id);
            return;
        }
    }
}
