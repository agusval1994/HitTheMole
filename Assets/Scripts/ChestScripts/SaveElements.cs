using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveElements : MonoBehaviour
{
    public Animator saveAnim;
    ChestManager cManager;
    GameManager gManager;

    void Start()
    {
        gManager = FindObjectOfType<GameManager>();
        cManager = FindObjectOfType<ChestManager>();
    }

    public void Save()
    {
        gManager.selectedMoles = cManager.selectedMoles;
        gManager.selectedHammerID = cManager.selectedHammerID;
        gManager.SaveData();
        saveAnim.SetBool("Save", false);
        saveAnim.SetBool("Save", true);
    }
}
