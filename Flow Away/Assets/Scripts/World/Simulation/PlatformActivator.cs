using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformActivator : MonoBehaviour
{
    public GameObject platform;
    public EventManager eventManager = null;

    public void Start()
    {
        if(eventManager != null)
            eventManager.OnEventStarted += EventManager_OnEventStarted;
    }

    private void EventManager_OnEventStarted(object sender, System.EventArgs e)
    {
        platform.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>())
            if (!platform.activeSelf)
                platform.SetActive(true);
    }
}
