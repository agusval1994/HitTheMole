using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButtons : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] molesList;
    ChestManager manager;

    [HideInInspector]
    public Dictionary<string, bool> choosedMoles = new Dictionary<string, bool>();

    private void Start()
    {
        manager = FindObjectOfType<ChestManager>();
        molesList = manager.moleListItems;
    }

    public void SelectAll()
    {
        foreach (GameObject go in molesList)
        {
            if (go.activeSelf)
            {
                Item item = go.GetComponent<Item>();

                if (!manager.selectedMoles.ContainsKey(item.id))
                {
                    Toggle btn = go.GetComponent<Toggle>();
                    btn.SetIsOnWithoutNotify(true);
                    manager.selectedMoles.Add(item.id, true);
                }
            }
        }
    }

    public void SelectNone()
    {
        foreach (GameObject go in molesList)
        {
            if (go.activeSelf)
            {
                Item item = go.GetComponent<Item>();

                if (manager.selectedMoles.ContainsKey(item.id))
                {
                    Toggle btn = go.GetComponent<Toggle>();
                    btn.SetIsOnWithoutNotify(false);
                    manager.selectedMoles.Remove(item.id);
                }

            }
        }
    }
}
