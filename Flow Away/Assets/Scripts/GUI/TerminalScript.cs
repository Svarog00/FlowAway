using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalScript : MonoBehaviour
{
    public GUISkin GUISkin;

    private bool isEnter;
    private bool showTerminal;
    private TextScript _text;


    private void Start()
    {
        showTerminal = false;
        isEnter = false;
        _text = FindObjectOfType<TextScript>();
    }


    private void Update()
    {
        if (isEnter && Input.GetButtonDown("Interact"))
        {
            showTerminal = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Movement>())
        {
            isEnter = true;
            _text.Appear("Press E to open terminal", 1.2f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Movement>())
        {
            isEnter = false;
            showTerminal = false;
            _text.Disappear(1.2f);
        }
    }

    private void OnGUI() 
    {
        if(showTerminal)
        {
            GUI.skin = GUISkin;
            GUI.Box(new Rect(Screen.width / 2 - 700, Screen.height - 800, 1400, 700), ""); //Создание бокса с ответами
        }
    }
}
