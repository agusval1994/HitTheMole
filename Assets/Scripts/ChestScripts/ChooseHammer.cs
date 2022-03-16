using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseHammer : MonoBehaviour
{
    ChestManager manager;
    Item item;

    private void Start()
    {
        manager = FindObjectOfType<ChestManager>();
        item = GetComponent<Item>();
    }

    public void Choose()
    {
        manager.selectedHammerID = item.id;
    }
}
