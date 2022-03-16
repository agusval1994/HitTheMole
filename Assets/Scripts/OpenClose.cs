using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClose : MonoBehaviour
{
    public GameObject openPopUp;
    public GameObject closePopUp;

    public void Open()
    {
        openPopUp.gameObject.SetActive(true);
    }

    public void Close()
    {
        closePopUp.gameObject.SetActive(false);
    }

    public void UnblockHammer()
    {
        FindObjectOfType<HammerManager>().enabled = true;
    }
}
