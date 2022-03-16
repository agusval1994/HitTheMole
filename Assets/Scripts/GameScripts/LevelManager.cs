using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    UIManager uimanager;
    SpawnMoles moleManager;
    GameManager gameManager;

    public int score;
    public int coinsEarned;

    public List<GameObject> groundList = new List<GameObject>();

    [HideInInspector]
    public List<GameObject> spawnMoles = new List<GameObject>();
    public GameObject[] listPrefabsMoles;

    public Dictionary<string, bool> selectedMoles = new Dictionary<string, bool>();

    void Start()
    {
        CreatePlayground();
        
        moleManager = FindObjectOfType<SpawnMoles>();
        uimanager = GetComponent<UIManager>();
        gameManager = FindObjectOfType<GameManager>();

        CheckAvaliablesMoles();

        StartUI();
    }

    public void CreatePlayground()
    {
        int levelId = PlayerPrefs.GetInt("Level", 0);

        GameObject go = groundList[levelId];

        Instantiate(go, go.transform.position, go.transform.rotation);
    }

    public void StartUI()
    {
        uimanager.start = true;
    }

    public void StartGame()
    {
        moleManager.WabeSystem();
    }

    public void KillMole(int coin)
    {
        score++;
        coinsEarned += coin;
        uimanager.UpdateUI();
    }

    public void FinishGame()
    {
        moleManager.EndGame();
    }

    public void CheckAvaliablesMoles()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        selectedMoles = data.selectedMoles;

        foreach (GameObject go in listPrefabsMoles)
        {
            Mole mole = go.GetComponent<Mole>();

            if (selectedMoles.ContainsKey(mole.id))
            {
                spawnMoles.Add(go);
            }
        }
    }
}
