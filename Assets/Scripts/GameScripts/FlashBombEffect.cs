using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBombEffect : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FlashEffect()
    {
        anim.SetBool("Flash", true);
    }

    public void ReStartAnim()
    {
        anim.SetBool("Flash", false);
    }
}
