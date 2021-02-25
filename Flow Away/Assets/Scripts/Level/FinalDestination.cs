using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FinalDestination : MonoBehaviour
{
    private Text text;
    private int faliuresCount = 0;

    private void Start()
    {
        SurveillanceScript surveillanceScript = FindObjectOfType<SurveillanceScript>();
        surveillanceScript.OnPlayerDetected += SurveillanceScript_OnPlayerDetected;
        text = FindObjectOfType<Text>();
    }

    private void SurveillanceScript_OnPlayerDetected(object sender, System.EventArgs e)
    {
        faliuresCount+=2;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            text.text = "Press R to try again";
            if(Input.GetKeyDown(KeyCode.R))
            {
                collision.transform.position = new Vector2(0, 0);
            }
            collision.GetComponent<Invisibility>().MaxTime -= faliuresCount;
        }
    }
}
