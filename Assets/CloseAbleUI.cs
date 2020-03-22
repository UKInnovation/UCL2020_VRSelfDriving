using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAbleUI : MonoBehaviour
{
    public void CloseOrOpen()
    {
        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
