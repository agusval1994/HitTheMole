using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsBehavior : MonoBehaviour
{
    public int lifeTime;

    void Start()
    {
        Invoke("DestroyEffect", lifeTime);
    }

    public void DestroyEffect()
    {
        Destroy(gameObject);
    }
}
