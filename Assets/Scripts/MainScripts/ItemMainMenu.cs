using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMainMenu : MonoBehaviour
{
    Animator anim;
    OpenClose openPopUp;
    ChangeScene changeScene;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hammer")
        {
            Check();
        }
    }

    public void Check()
    {
        if (gameObject.tag == "Mole")
        {
            anim.SetBool("HitMenu", true);
            StartCoroutine(DelayPopUp(.25f));
            FindObjectOfType<HammerManager>().enabled = false;
        }
        else if (gameObject.tag == "Settings")
        {
            anim.SetBool("HitMenu", true);
            StartCoroutine(DelayPopUp(.25f));
            FindObjectOfType<HammerManager>().enabled = false;
        }
        else if (gameObject.tag == "Market")
        {
            anim.SetBool("HitMenu", true);
            changeScene = GetComponent<ChangeScene>();
            changeScene.ScreenChange(.25f);
            FindObjectOfType<HammerManager>().enabled = false;
        }
        else if (gameObject.tag == "Chest")
        {
            anim.SetBool("HitMenu", true);
            changeScene = GetComponent<ChangeScene>();
            changeScene.ScreenChange(.25f);
            FindObjectOfType<HammerManager>().enabled = false;
        }
        else if(gameObject.tag == "Exit")
        {
            StartCoroutine(ExitGame());
        }
    }

    public IEnumerator DelayPopUp(float timeDelay)
    {
        openPopUp = GetComponent<OpenClose>();

        yield return new WaitForSeconds(timeDelay);

        openPopUp.Open();
    }

    public void StopAnimation()
    {
        anim.SetBool("HitMenu", false);
    }

    public IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(.5f);

        Application.Quit();
    }
}