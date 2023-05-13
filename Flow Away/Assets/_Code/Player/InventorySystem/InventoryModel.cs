using System.Collections.Generic;
using System.Linq;

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
    }
}