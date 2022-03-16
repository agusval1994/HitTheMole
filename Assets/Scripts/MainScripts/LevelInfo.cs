using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public int levelId;

    public void Info()
    {
        PlayerPrefs.SetInt("Level", levelId);
    }
}
