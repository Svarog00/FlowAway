using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformActivator : MonoBehaviour
{
    public GameObject platform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player_Movement>())
            if (!platform.activeSelf)
                platform.SetActive(true);
    }
}
