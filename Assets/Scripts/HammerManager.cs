using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HammerManager : MonoBehaviour
{
    [HideInInspector]
    public GameObject hammerVisual;
    [HideInInspector]
    public Animator anim;

    public CameraShake cameraShake;

    [Header("HamerList")]
    public GameObject[] hammerList;

    [Header("Audios")]
    public List<string> audiosName = new List<string>();
    
    AudioManager audioManager;
    Scene currentScene;
    
    string sceneName;
    string selectedHammerID;
    bool canHit = true;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        CreateHammer();
        GetHammer();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canHit)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000))
                {
                    if (hit.transform.tag == "Hole" || hit.transform.tag == "Mole" || hit.transform.tag == "Market" 
                        || hit.transform.tag == "Settings" || hit.transform.tag == "Chest" || hit.transform.tag == "Exit")
                    {
                        transform.position = hit.transform.position;
                        canHit = false;
                        StartCoroutine(Hit());
                    }
                }
            }
        }
    }

    public IEnumerator Hit()
    {
        anim.SetBool("Hit", true);

        yield return new WaitForSeconds(.1f);

        HitSound();

        if(sceneName == "Game")
        {
            cameraShake.ShakeCamera();
        }

        yield return new WaitForSeconds(.1f);

        anim.SetBool("Hit", false);

        canHit = true;
    }

    public void CreateHammer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        selectedHammerID = data.selectedHammerID;

        foreach (GameObject hammer in hammerList)
        {
            Item item = hammer.GetComponent<Item>();

            if (selectedHammerID == item.id)
            {
                GameObject go = Instantiate(hammer, hammer.transform.position, hammer.transform.rotation);
                go.transform.parent = transform;
            }
        }
    }

    public void GetHammer()
    {
        hammerVisual = gameObject.transform.GetChild(0).gameObject;
        anim = hammerVisual.GetComponent<Animator>();
    }

    public void HitSound()
    {
        int randAudio = Random.Range(0, audiosName.Count);

        string soundToPlay = audiosName[randAudio];

        audioManager.Play(soundToPlay, "Fx");
    }
}
