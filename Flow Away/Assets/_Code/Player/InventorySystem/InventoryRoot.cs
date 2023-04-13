using System;
using UnityEngine;

namespace InventorySystem
{
    public class InventoryRoot : MonoBehaviour
    {
        public InventoryModel InventoryModel => _inventoryModel;

        [SerializeField] private ItemDatabase _itemDatabase;
        [SerializeField] private GameObject _itemInstance;
        
        private InventoryModel _inventoryModel;

        void Start()
        {
            _inventoryModel = new InventoryModel();
        }

        public void AddItem(int itemId)
        {
            var item = _itemDatabase.GetItem(itemId);
            _inventoryModel.AddItem(item);
        }

        public void SetInventory(InventoryModel inventory)
        {
            _inventoryModel = inventory;
        }

        public void DropItem(int itemId)
        {
            var item = _itemDatabase.GetItem(itemId);
            _inventoryModel.DeleteItem(item);

            var itemObject = Instantiate(_itemInstance);
            itemObject.GetComponent<ItemInstance>().SetItem(item);
        }
    }
}