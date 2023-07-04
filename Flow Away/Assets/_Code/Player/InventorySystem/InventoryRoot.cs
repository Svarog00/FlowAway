using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class InventoryRoot : MonoBehaviour
    {
        public InventoryModel InventoryModel => _inventoryModel;

        [SerializeField] private ItemDatabase _itemDatabase;
        [SerializeField] private GameObject _itemInstance;
        
        private InventoryModel _inventoryModel;

        void Awake()
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

        public void DeleteItem(int itemId)
        {
            var item = _itemDatabase.GetItem(itemId);
            _inventoryModel.DeleteItem(item);
        }

        public bool PickItem(int itemId)
        {
            var item = _itemDatabase.GetItem(itemId);

            return _inventoryModel.PickItem(item);
        }

        public void DropItem(int itemId)
        {
            var item = _itemDatabase.GetItem(itemId);
            _inventoryModel.DeleteItem(item);

            var itemObject = Instantiate(_itemInstance, gameObject.transform.position, Quaternion.identity);
            itemObject.GetComponent<ItemInstance>().SetItem(item);
        }

        public List<int> GetItemsIds()
        {
            var list = new List<int>();
            foreach(var item in _inventoryModel.Items)
            {
                list.Add(item.Id);
            }

            return list;
        }

        public void LoadItems(List<int> items)
        {
            _inventoryModel.ClearInventory();

            foreach(int id in items)
            {
                AddItem(id);
            }
        }
    }
}