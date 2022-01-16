using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FinalDestination : MonoBehaviour
{
    public EventManager eventManager;

    private Text text;
    private int faliuresCount = 0;
    private SurveillanceScript surveillanceScript;

    private void Start()
    {
        surveillanceScript = FindObjectOfType<SurveillanceScript>();
        surveillanceScript.OnPlayerDetected += SurveillanceScript_OnPlayerDetected;
        eventManager.OnEventFinished += EventManager_OnEventFinished;
        text = FindObjectOfType<Text>();
    }

    private void EventManager_OnEventFinished(object sender, System.EventArgs e)
    {
        faliuresCount++;
    }

    private void SurveillanceScript_OnPlayerDetected(object sender, System.EventArgs e)
    {
        faliuresCount++;
        surveillanceScript.OnPlayerDetected -= SurveillanceScript_OnPlayerDetected;
    }
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            text.text = $"You failed {faliuresCount} times. Press R to try again";
            if(Input.GetKeyDown(KeyCode.R))
            {
                collision.transform.position = new Vector2(0, 0);
                faliuresCount = 0;
                surveillanceScript.OnPlayerDetected += SurveillanceScript_OnPlayerDetected;
            }
            collision.GetComponent<Invisibility>().MaxTime -= faliuresCount;
        }
    }
}
