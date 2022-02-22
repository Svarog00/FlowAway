using Assets.Scripts.Infrastructure.Services;
using System.Collections.Generic;
using UnityEngine;

public class TerminalScript : MonoBehaviour
{
    private const string _uiInformerText = "Press E to open terminal";

    public GUISkin GUISkin;
    public TextAsset tAsset;
    public Terminal terminal;
    
    [SerializeField] private UINoteTextScript _uiInformer;

    private List<Note> _notesList = new List<Note>();
    private string _areaText = "";
    private bool _isEnter;
    private bool _showTerminal;
    private Vector2 _scrollPosition;

    private IInputService _inputService;

    private void Start()
    {
        InitializeTerminal();
    }

    private void InitializeTerminal()
    {
        _showTerminal = false;
        _isEnter = false;

        _scrollPosition = Vector2.zero;

        terminal = Terminal.Load(tAsset);
        for (int i = 0; i < terminal.notes.Length; i++)
        {
            _notesList.Add(terminal.notes[i]);
        }

        _inputService = ServiceLocator.Container.Single<IInputService>();

        _uiInformer = GetComponentInChildren<UINoteTextScript>();
    }

    private void Update()
    {
        if (_isEnter && _inputService.IsInteractButtonDown() && !_showTerminal)
        {
            _showTerminal = true;
        }
        else if (_isEnter && _inputService.IsInteractButtonDown() && _showTerminal)
        {
            _showTerminal = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Movement>())
        {
            _isEnter = true;
            _uiInformer.Appear(_uiInformerText, 1.2f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player_Movement>())
        {
            _isEnter = false;
            _showTerminal = false;
            _uiInformer.Disappear(1.2f);
        }
    }


    private void OnGUI() 
    {
        if(_showTerminal)
        {
            ShowTerminal();
        }
    }

    private void ShowTerminal()
    {
        GUI.skin = GUISkin;
        GUI.Box(new Rect(Screen.width / 2 - 700, Screen.height - 800, 1400, 700), ""); //Создание бокса с ответами
        GUI.TextArea(new Rect(Screen.width / 2 - 150, Screen.height - 690, 780, 480), _areaText);
        _scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 - 600, Screen.height - 700, 400, 480), _scrollPosition, new Rect(0, 0, 380, 800), false, false);
        for (int i = 0; i < _notesList.Count; i++)
        {
            if (GUI.Button(new Rect(0, 110 * i, 350, 100), _notesList[i].title))
            {
                _areaText = _notesList[i].text;
            }
        }
        GUI.EndScrollView();
    }
}
