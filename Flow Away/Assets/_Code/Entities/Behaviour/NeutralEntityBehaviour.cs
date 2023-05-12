using Assets.Scripts.Infrastructure.Services;
using InventorySystem;
using UnityEngine;

public class NeutralEntityBehaviour : AgentBehaviour
{
    private const string PlayerTag = "Player";
    [SerializeField] private UINoteTextScript _note;
    [SerializeField] private TextAsset _textAsset;

    private UI_DialogueWindow _dialogueWindow;
    private InventoryRoot _playerInventory;
    private PlayerControl _playerControl;

    private IInputService _inputService;

    private bool _canStartDialogue;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        _canStartDialogue = false;

        _inputService = ServiceLocator.Container.Single<IInputService>();
        _note = GetComponentInChildren<UINoteTextScript>();

        _dialogueWindow = FindObjectOfType<UI_DialogueWindow>();
    }

    private void Update()
    {
        StateMachine.Work();

        if(!_canStartDialogue)
        {
            return;
        }

        if (_inputService.IsInteractButtonDown())
        {
            ShowDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.tag.Contains(PlayerTag))
        {
            return;
        }

        _note.Appear("Press E to talk.", 2f);
        _canStartDialogue = true;

        if (_playerControl == null)
        {
            _playerControl = collision.GetComponent<PlayerControl>();
            _playerInventory = collision.GetComponent<InventoryRoot>();
            _dialogueWindow.SetPlayerInventory(_playerInventory);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Contains(PlayerTag))
        {
            _note.Disappear(2f);
            CloseDialogue();
        }
    }

    private void ShowDialogue()
    {
        _playerControl.CanAttack = false;
        _dialogueWindow.ShowWindow(_textAsset);
        _note.Disappear(2f);
    }

    private void CloseDialogue()
    {
        _dialogueWindow.CloseWindow();
        _playerControl.CanAttack = true;
        _canStartDialogue = false;
    }
}
