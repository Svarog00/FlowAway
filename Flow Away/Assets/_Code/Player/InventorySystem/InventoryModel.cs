using System.Collections.Generic;

namespace InventorySystem
{
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
            if(item == null)
            {
                return;
            }

            _items.Add(item);
        }

        public bool PickItem(Item item)
        {
            return _items.Contains(item);
        }

        public void DeleteItem(Item item)
        {
            if(item == null)
            {
                return;
            }

            _items.Remove(item);
        }

        public void ClearInventory()
        {
            if(_items.Count == 0)
            {
                return;
            }

            _items.Clear();
        }
    }
}