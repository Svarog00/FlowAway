using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdleNPC : MonoBehaviour
{
    public DialogSystemNew dialog;
    public Text note;

    private bool _isNearby;

    private void Start()
    {
        _isNearby = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && _isNearby)
        {
            dialog.showDialog = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            note.CrossFadeAlpha(1, 2f, false);
            note.text = "Press E to talk.";
            _isNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            note.CrossFadeAlpha(0, 2f, false);
            _isNearby = false;
        }
    }
}
