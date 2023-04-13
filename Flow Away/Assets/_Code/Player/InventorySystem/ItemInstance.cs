using Assets.Scripts.Infrastructure.Services;
using InventorySystem;
using UnityEngine;

public class ItemInstance : MonoBehaviour
{
    private const string UiText = "Press E to pick up";

    [SerializeField] private UINoteTextScript _uiNoteText;
    [SerializeField] private int _bindedItemId;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private InventoryRoot _playerInventory;
    private bool _canPickUp;
    private IInputService _input;

    public void Awake()
    {
        _input = ServiceLocator.Container.Single<IInputService>();
    }

    public void Update()
    {
        if(!_canPickUp)
        {
            return;
        }

        if(_input.IsInteractButtonDown())
        {
            PickUp();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            return;
        }

        _playerInventory = collision.gameObject.GetComponent<InventoryRoot>();
        _uiNoteText.Appear(UiText, 1.2f);
        _canPickUp = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        _uiNoteText.Disappear(1.2f);
        _canPickUp = false;
    }

    public void SetItem(Item item)
    {
        _bindedItemId = item.Id;
        _spriteRenderer.sprite = item.Image;
    }

    private void PickUp()
    {
        _playerInventory.AddItem(_bindedItemId);
        gameObject.SetActive(false);
    }
}
