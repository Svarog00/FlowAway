using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalScript : MonoBehaviour
{
    public GUISkin GUISkin;
    public TextAsset tAsset;
    public Terminal terminal;

    private List<Note> notesList = new List<Note>();
    private string areaText = "";
    private bool isEnter;
    private bool showTerminal;
    private TextScript _text;
    private Vector2 scrollPosition;

    private void Start()
    {
        showTerminal = false;
        isEnter = false;
        scrollPosition = Vector2.zero;
        _text = FindObjectOfType<TextScript>();
        terminal = Terminal.Load(tAsset);
        InitializeNotes();   
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

    private void InitializeNotes()
    {
        for(int i = 0; i < terminal.notes.Length; i++)
        {
            notesList.Add(terminal.notes[i]);
        }
    }

    private void OnGUI() 
    {
        if(showTerminal)
        {   
            GUI.skin = GUISkin;
            GUI.Box(new Rect(Screen.width / 2 - 700, Screen.height - 800, 1400, 700), ""); //Создание бокса с ответами
            GUI.TextArea(new Rect(Screen.width / 2 - 150, Screen.height - 690, 780, 480), areaText);
            scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 - 600, Screen.height - 700, 400, 480), scrollPosition, new Rect(0, 0, 380, 800), false, false);
            for(int i = 0; i < notesList.Count; i++)
            {
                if (GUI.Button(new Rect(0, 110 * i, 350, 100), notesList[i].title))
                {
                    areaText = notesList[i].text;
                }
            }
            GUI.EndScrollView();
        }
    }
}
