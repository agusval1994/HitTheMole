using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0f;
        FindObjectOfType<HammerManager>().enabled = false;
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        FindObjectOfType<HammerManager>().enabled = true;
    }
    public void Exit(float delayTime)
    {
        Time.timeScale = 1f;
        StartCoroutine("Delay", delayTime);
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Main");
    }
}
