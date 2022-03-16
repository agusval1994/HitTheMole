using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string newScreen;

    public void ScreenChange(float delay)
    {
        StartCoroutine("NewScene", delay);
    }

    IEnumerator NewScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(newScreen);
    }

    public void ExitGame(float delay)
    {
        StartCoroutine("CloseGame", delay);
    }

    IEnumerator CloseGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }
}
