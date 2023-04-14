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
    [SerializeField] private GameObject _itemButtonPrefab;
    [SerializeField] private GameObject _dropButton;

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
    }

    private void OpenInventoryUI()
    {
        _inventoryWindow.SetActive(_isOpen);
        _selectedItemId = 0;
        _dropButton.SetActive(false);
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
            GameObject itemButtonPrefab = Instantiate(_itemButtonPrefab, _inventoryGridView.transform);
            itemButtonPrefab.GetComponent<Image>().sprite = item.Image;
            itemButtonPrefab.GetComponent<Button>().onClick.AddListener(() => SelectItem(item));
        }
    }

    public void DropItem()
    {
        _inventoryRoot.DropItem(_selectedItemId);
        _selectedItemId = 0;

        _itemDescriptionView.GetComponent<TMP_Text>().text = string.Empty;
        _dropButton.SetActive(false);
        UpdateGrid();
    }
}
