using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundList : MonoBehaviour
{
    public void BombExplotion()
    {
        FindObjectOfType<AudioManager>().Play("BombExplotion", "Fx");
    }

    public void CancelButton()
    {
        FindObjectOfType<AudioManager>().Play("CancelButton", "Fx");
    }

    public void BigButton()
    {
        FindObjectOfType<AudioManager>().Play("BigButton", "Fx");
    }

    public void SmallButton()
    {
        FindObjectOfType<AudioManager>().Play("SmallButton", "Fx");
    }

    public void ClockTicTac()
    {
        FindObjectOfType<AudioManager>().Play("ClockTicTac", "Fx");
    }

    public void ClockEndGame()
    {
        FindObjectOfType<AudioManager>().Play("ClockEndGame", "Fx");
    }
}
