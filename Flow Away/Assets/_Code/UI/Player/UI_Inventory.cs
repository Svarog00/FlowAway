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

    private InventoryRoot _inventoryRoot;
    private IInputService _inputService;

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
    }

    private void OpenInventoryUI()
    {
        _inventoryWindow.SetActive(_isOpen);
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
}
