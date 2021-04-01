using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalScript : MonoBehaviour
{
    public Texture2D normalButton;
    public GUISkin GUISkin;

    private bool isEnter;
    private bool showTerminal;
    private TextScript _text;
    private Vector2 scrollPosition;
    private GUIContent content;
    


    private void Start()
    {
        showTerminal = false;
        isEnter = false;
        scrollPosition = Vector2.zero;
        _text = FindObjectOfType<TextScript>();
        content = new GUIContent("test", normalButton);
    }


    private void Update()
    {
        if (isEnter && Input.GetButtonDown("Interact") && !showTerminal)
        {
            showTerminal = true;
        }
        else if (isEnter && Input.GetButtonDown("Interact") && showTerminal)
        {
            showTerminal = false;
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
            scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 - 600, Screen.height - 700, 400, 480), scrollPosition, new Rect(0, 0, 380, 800), false, false);
            GUI.Button(new Rect(0, 0, 350, 100), content);
            GUI.Button(new Rect(0, 110, 350, 100), "Test2");
            GUI.Button(new Rect(0, 220, 350, 100), "Test3");
            GUI.Button(new Rect(0, 330, 350, 100), "Test4");
            GUI.Button(new Rect(0, 440, 350, 100), "Test5");
            GUI.Button(new Rect(0, 550, 350, 100), "Test6");
            GUI.EndScrollView();
        }
    }
}
