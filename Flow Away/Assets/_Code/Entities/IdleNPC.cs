using Assets.Scripts.Infrastructure.Services;
using InventorySystem;
using UnityEngine;

public class IdleNPC : AgentBehaviour
{
    [SerializeField] private UINoteTextScript _note;
    [SerializeField] private TextAsset _textAsset;

    private UI_DialogueWindow _dialogueWindow;
    private InventoryRoot _playerInventory;
    private PlayerControl _playerControl;

    private IInputService _inputService;

    private bool _canStartDialogue;

    private void Start()
    {
        _canStartDialogue = false;

        _inputService = ServiceLocator.Container.Single<IInputService>();
        _note = GetComponentInChildren<UINoteTextScript>();

        _dialogueWindow = FindAnyObjectByType<UI_DialogueWindow>();
    }

    private void Update()
    {
        if (_canStartDialogue && _inputService.IsInteractButtonDown())
        {
            ShowDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.tag.Contains("Player"))
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
        if (collision.tag.Contains("Player"))
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
