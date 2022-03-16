using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public string id;
    public float timeInField;
    public int coinDrop;
    public Animator bombEffectAnim;
    
    Animator anim;
    SpawnMoles spawmnMoles;
    GameObject coinsToDrop = null;
    
    [Header("Effects")]
    public GameObject hitEfect;
    public GameObject dropCoin1;
    public GameObject dropCoin3;
    public GameObject dropCoin5;
    public GameObject dropCoin20;
    public GameObject bombEfect;
    public GameObject emptyGo;
    

    void Start()
    {
        anim = GetComponent<Animator>();

        spawmnMoles = FindObjectOfType<SpawnMoles>();

        MoleUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hammer")
        {
            if(id == "MoleBomb")
            {
                ExplodeBomb();
            }
            else
            {
                DeathEffect();
                CalculateCoins();
                FindObjectOfType<LevelManager>().KillMole(coinDrop);
                DestroyMole();
            }
        }
    }

    public void DeathEffect()
    {
        GameObject go = Instantiate(hitEfect, transform.position, transform.rotation) as GameObject;
    }

    IEnumerator MoleLife()
    {
        yield return new WaitForSeconds(timeInField);

        MoleDown();
    }

    public void MoleInField()
    {
        StartCoroutine("MoleLife");
    }

    public void MoleUp()
    {
        anim.SetInteger("Condition", 1);
    }

    public void MoleDown()
    {
        anim.SetInteger("Condition", 2);
    }

    public void DestroyMole()
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        spawmnMoles.AddPosition(parent);
        Destroy(gameObject);
    }

    public void ExplodeBomb()
    {
        GameObject go = Instantiate(bombEfect, transform.position, transform.rotation) as GameObject;
        spawmnMoles.ActivateBomb();
        DestroyMole();
    }

    public void CalculateCoins()
    {
        if (id == "MoleGold")
        {
            coinDrop = 20;
            coinsToDrop = dropCoin20;
        }
        else
        {
            int dropChance = Random.Range(0, 100);

            if (dropChance >= 0 && dropChance < 10)
            {
                coinDrop = 0;
                coinsToDrop = emptyGo;
            }
            else if (dropChance >= 10 && dropChance < 60)
            {
                coinDrop = 1;
                coinsToDrop = dropCoin1;
            }
            else if (dropChance >= 60 && dropChance < 90)
            {
                coinDrop = 3;
                coinsToDrop = dropCoin3;
            }
            else if (dropChance >= 90 && dropChance < 100)
            {
                coinDrop = 5;
                coinsToDrop = dropCoin5;
            }
        }

        DropCoins();
    }

    public void DropCoins()
    {
        GameObject go = Instantiate(coinsToDrop, transform.position, transform.rotation) as GameObject;
    }
}
