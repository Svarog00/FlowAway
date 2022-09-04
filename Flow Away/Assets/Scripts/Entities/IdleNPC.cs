using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdleNPC : AgentBehaviour
{
    [SerializeField] private DialogView _dialog;
    [SerializeField] private UINoteTextScript _note;

    private PlayerControl _playerControl;

    private bool _canStartDialogue;

    private void Start()
    {
        _canStartDialogue = false;
        _note = GetComponentInChildren<UINoteTextScript>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && _canStartDialogue)
        {
            ShowDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Contains("Player"))
        {
            _note.Appear("Press E to talk.", 2f);
            _canStartDialogue = true;

            if (_playerControl == null) 
                _playerControl = collision.GetComponent<PlayerControl>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            _note.Disappear(2f);
            CloseDialogue();
        }
    }

    private void ShowDialogue()
    {
        _playerControl.CanAttack = false;
        _dialog.showDialog = true;
        _note.Disappear(2f);
    }

    private void CloseDialogue()
    {
        _dialog.showDialog = false;
        _playerControl.CanAttack = true;
        _canStartDialogue = false;
    }
}
