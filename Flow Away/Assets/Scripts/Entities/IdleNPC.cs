using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdleNPC : MonoBehaviour
{
    public DialogSystemNew dialog;

    private TextScript _note;
    private PlayerControl playerControl;

    private bool _isNearby;

    private void Start()
    {
        _isNearby = false;
        _note = FindObjectOfType<TextScript>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && _isNearby)
        {
            playerControl.CanAttack = false;
            dialog.showDialog = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _note.Appear("Press E to talk.", 2f);
            _isNearby = true;

            if (playerControl == null) playerControl = collision.GetComponent<PlayerControl>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dialog.showDialog = false;
            playerControl.CanAttack = true;
            _note.Disappear(2f);
            _isNearby = false;
        }
    }

    public void Hurt(int damage)
    {
        
    }
}
