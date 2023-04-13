using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class Item
    {
        public int Id;
        public string Name;
        public string Description;

        public Sprite Image;
    }

    public class InventoryModel
    {
        public IEnumerable<Item> Items => _items;

        private List<Item> _items;

        public InventoryModel()
        {
            _items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public bool PickItem(int id)
        {
            if(_items.Count == 0)
                return false;

            return _items.Contains(_items
                .Where((Item item) => item.Id == id)
                .First());
        }

        public void DeleteItem(Item item)
        {
            _items.Remove(item);
        }
    }
}