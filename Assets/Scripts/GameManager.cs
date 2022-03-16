using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public int money, score;
    [HideInInspector]
    public Dictionary<string, bool> unlockedMoles = new Dictionary<string, bool>();
    [HideInInspector]
    public Dictionary<string, bool> unlockedHammers = new Dictionary<string, bool>();
    [HideInInspector]
    public Dictionary<string, bool> selectedMoles = new Dictionary<string, bool>();
    [HideInInspector]
    public string selectedHammerID;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        CheckFiles();
    }

    public void CheckFiles()
    {
        string path = Application.persistentDataPath + "/player.fun";

        if (System.IO.File.Exists(path))
        {
            LoadData();
        }
        else
        {
            InitialData();
        }
    }

    public void InitialData()
    {
        money = 500;
        score = 0;
        unlockedMoles.Add("Mole01", true);
        selectedMoles.Add("Mole01", true);
        unlockedHammers.Add("Hammer01", true);
        selectedHammerID = "Hammer01";

        SaveData();
        LoadData();
    }

    public void LoadData()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        unlockedMoles = data.unlockedMoles;
        unlockedHammers = data.unlockedHammers;
        selectedMoles = data.selectedMoles;
        money = data.money;
        score = data.score;
        selectedHammerID = data.selectedHammerID;

        FindObjectOfType<AudioManager>().Play("MainTheme", "Music");
    }

    public void DeleteData()
    {
        unlockedMoles.Clear();
        selectedMoles.Clear();
        unlockedHammers.Clear();

        SaveData();
    }

    public void SaveData()
    {
        SaveSystem.SavePlayer(this);
    }
}
