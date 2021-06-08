using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdleNPC : MonoBehaviour
{
    public DialogSystemNew dialog;
    private TextScript _note;

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
            dialog.showDialog = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _note.Appear("Press E to talk.", 2f);
            _isNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _note.Disappear(2f);
            _isNearby = false;
        }
    }

    public void Hurt(int damage)
    {
        gameObject.AddComponent<Ghoul>();
    }
}
