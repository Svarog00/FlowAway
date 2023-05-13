using Assets.Scripts.Infrastructure.Services;
using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryWindow;
    [SerializeField] private GameObject _inventoryGridView;
    [SerializeField] private GameObject _itemDescriptionView;
    [SerializeField] private GameObject _itemGridInstancePrefab;

    [SerializeField] private GameObject _dropButton;
    [SerializeField] private GameObject _useButton;

    private InventoryRoot _inventoryRoot;
    private IInputService _inputService;

    private int _selectedItemId;
    private bool _isOpen = false;

    // Start is called before the first frame update
    void Awake()
    {
        _inputService = ServiceLocator.Container.Single<IInputService>();
    }

    private void Start()
    {
        _inventoryWindow.SetActive(_isOpen);
    }

    // Update is called once per frame
    void Update()
    {
        if(_inputService.IsOpenInventoryButtonDown())
        {
            _isOpen = !_isOpen;
            OpenInventoryUI();
        }
    }

    public void SetInventory(InventoryRoot inventoryRoot)
    {
        _inventoryRoot = inventoryRoot;
    }

    public void SelectItem(Item item)
    {
        _itemDescriptionView.GetComponent<TMP_Text>().text = item.Description;
        _selectedItemId = item.Id;

        _dropButton.SetActive(true);
        _useButton.SetActive(item.IsUsable);
        _useButton.GetComponent<Button>().onClick.AddListener(() => UseItem(item));
    }

    public void DropItem()
    {
        _inventoryRoot.DropItem(_selectedItemId);
        _selectedItemId = 0;

        ResetDescriptionWindow();
        UpdateGrid();
    }

    private void OpenInventoryUI()
    {
        _inventoryWindow.SetActive(_isOpen);
        _selectedItemId = 0;

        ResetDescriptionWindow();
        UpdateGrid();
    }

    private void UpdateGrid()
    {
        foreach(Transform button in _inventoryGridView.transform)
        {
            Destroy(button.gameObject);
        }

        foreach (var item in _inventoryRoot.InventoryModel.Items)
        {
            GameObject itemButtonPrefab = Instantiate(_itemGridInstancePrefab, _inventoryGridView.transform);
            itemButtonPrefab.GetComponent<Image>().sprite = item.Image;
            itemButtonPrefab.GetComponent<Button>().onClick.AddListener(() => SelectItem(item));
        }
    }

    private void UseItem(Item item)
    {
        item.Use(_inventoryRoot.gameObject);
        _inventoryRoot.DeleteItem(item.Id);

        ResetDescriptionWindow();
        UpdateGrid();
    }

    private void ResetDescriptionWindow()
    {
        _itemDescriptionView.GetComponent<TMP_Text>().text = string.Empty;
        _dropButton.SetActive(false);
        _useButton.SetActive(false);
    }
}
