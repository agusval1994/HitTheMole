using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Dictionary<string, bool> unlockedMoles = new Dictionary<string, bool>();
    public Dictionary<string, bool> selectedMoles = new Dictionary<string, bool>();
    public Dictionary<string, bool> unlockedHammers = new Dictionary<string, bool>();
    public string selectedHammerID;
    public int money;
    public int score;

    public PlayerData(GameManager manager)
    {
        money = manager.money;
        score = manager.score;
        unlockedMoles = manager.unlockedMoles;
        selectedMoles = manager.selectedMoles;
        unlockedHammers = manager.unlockedHammers;
        selectedHammerID = manager.selectedHammerID;
    }
}
