using Assets.Scripts.Infrastructure.Services;
using System.Collections.Generic;
using UnityEngine;

public class TerminalWorldInstance : MonoBehaviour
{
    private const string _uiInformerText = "Press E to open terminal";

    [SerializeField] private TextAsset _tAsset;
    [SerializeField] private UINoteTextScript _uiInformer;
    [SerializeField] private UI_TerminalWindow _terminalUiWindow;

    private TerminalModel _model;

    private bool _isPlayerNearby;
    private bool _isActive;

    private IInputService _inputService;

    private void Awake()
    {
        InitializeTerminal();
    }

    private void Start()
    {
        _terminalUiWindow = FindObjectOfType<UI_TerminalWindow>();
    }

    private void InitializeTerminal()
    {
        _isPlayerNearby = false;
        _isActive = false;

        _model = new TerminalModel();
        _model = _model.Load(_tAsset);

        _inputService = ServiceLocator.Container.Single<IInputService>();

        _uiInformer = GetComponentInChildren<UINoteTextScript>();
    }

    private void Update()
    {
        if(!_isPlayerNearby)
        {
            return;
        }

        if(_inputService.IsInteractButtonDown())
        {
            if(_isActive)
            {
                _terminalUiWindow.CloseWindow();
                _isActive = false;
                return;
            }

            _isActive = true;
            _terminalUiWindow.ShowWindow(_model);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl>())
        {
            _isPlayerNearby = true;
            _uiInformer.Appear(_uiInformerText, 1.2f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerControl>())
        {
            _isPlayerNearby = false;
            _terminalUiWindow.CloseWindow();
            _uiInformer.Disappear(1.2f);
        }
    }
}
