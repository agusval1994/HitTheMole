using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMoles : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> spawnMoles = new List<GameObject>();

    int intPosition;
    int difficulty = 0;
    int bombMoleProb = 5;
    int goldMoleProb = 5;
    int hordeProb = 15;
    bool horde = true;
    float bombDuration = 3f;
    float spawnFrequency = 1;
   
    GameObject goPosition;
    UIManager uiManager;
    LevelManager manager;
    HammerManager hammer;
    
    public List<GameObject> freePositions = new List<GameObject>();
    public List<GameObject> positions = new List<GameObject>();
    public GameObject bombMole, moleGold;
    
    GameObject moleToSpawn;

    void Start()
    {
        manager = FindObjectOfType<LevelManager>();
        uiManager = FindObjectOfType<UIManager>();
        hammer = FindObjectOfType<HammerManager>();
        spawnMoles = manager.spawnMoles;
    }

    public void WabeSystem()
    {
        if (uiManager.gameTime >= 51)
        {
            spawnFrequency = 1f;
            difficulty = 0;
        }
        else if(uiManager.gameTime >= 31)
        {
            spawnFrequency = .75f;
            difficulty = 1;
        }
        else if(uiManager.gameTime >= 11)
        {
            spawnFrequency = .5f;
            difficulty = 2;
        }
        else if(uiManager.gameTime >= 1)
        {
            spawnFrequency = .25f;
            difficulty = 3;
        }
        else if(uiManager.gameTime <= 0)
        {
            EndGame();
        }

        StartCoroutine("WaveMole");
    }

    public IEnumerator WaveMole()
    {
        if (uiManager.gameTime >= 11 && horde)
        {
            int hordeRand = Random.Range(1, 100);
            
            if (hordeRand <= hordeProb)
            {
                horde = false;
                StartCoroutine(Horde());
            }
        }
        
        CreateMole();

        yield return new WaitForSeconds(spawnFrequency);
        
        WabeSystem();
    }
    
    public void CreateMole()
    {
        SearchEmptyPosition();

        int bombRand = Random.Range(1, 100);
        int goldRand = Random.Range(1, 100);

        if(bombRand <= bombMoleProb)
        {
            moleToSpawn = bombMole;
        }
        else if(goldRand <= goldMoleProb)
        {
            moleToSpawn = moleGold;
        }
        else
        {
            int int_rand = Random.Range(0, spawnMoles.Count);
            moleToSpawn = spawnMoles[int_rand];
        }

        Mole mole = moleToSpawn.GetComponent<Mole>();

        Animator anim = moleToSpawn.GetComponent<Animator>();

        if (difficulty == 0)
        {
            mole.timeInField = 1f;
        }
        else if (difficulty == 1)
        {
            mole.timeInField = 0.75f;
        }
        else if (difficulty == 2)
        {
            mole.timeInField = .5f;
        }
        else if (difficulty == 3)
        {
            mole.timeInField = .25f;
        }

        GameObject go = Instantiate(moleToSpawn, moleToSpawn.transform.position, moleToSpawn.transform.rotation) as GameObject;
        go.transform.parent = goPosition.transform;

        DeletePosition(goPosition);
    }

    public void SearchEmptyPosition()
    {
        intPosition = Random.Range(0, freePositions.Count);
        goPosition = freePositions[intPosition];
    }

    public void DeletePosition(GameObject go)
    {
        freePositions.Remove(go);
    }

    public void AddPosition(GameObject go)
    {
        freePositions.Add(go);
    }

    public void ActivateBomb()
    {
        StopAllCoroutines();
        FindObjectOfType<AudioManager>().Play("BombExplotion", "Fx");
        StartCoroutine("BombEffect");
    }

    public IEnumerator BombEffect()
    {
        AllMolesDown();
        hammer.enabled = false;
        uiManager.FlashEffect();

        yield return new WaitForSeconds(bombDuration);

        hammer.enabled = true;
        StartCoroutine("WaveMole");
    }

    public void AllMolesDown()
    {
        foreach(GameObject go in positions)
        {
            if (go.transform.childCount != 0)
            {
                GameObject goMole = go.transform.GetChild(0).gameObject;
                Mole mole = goMole.GetComponent<Mole>();
                mole.MoleDown();
            }
        }
    }

    public IEnumerator Horde()
    {
        yield return new WaitForSeconds(.5f);
        CreateMole();
        
        yield return new WaitForSeconds(.5f);
        CreateMole();
        
        yield return new WaitForSeconds(.5f);
        CreateMole();
        
        yield return new WaitForSeconds(.5f);
        CreateMole();
        
        yield return new WaitForSeconds(3f);

        horde = true;
    }
    
    public void EndGame()
    {
        StopAllCoroutines(); 
        AllMolesDown();
        hammer.enabled = false;
    }
}
