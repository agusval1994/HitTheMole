using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAudio : MonoBehaviour
{
    public void AudioToPlay(string audioName)
    {
        FindObjectOfType<AudioManager>().Play(audioName, "Fx");
    }
}
