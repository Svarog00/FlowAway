using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemDatabase", order = 1)]
    public class ItemDatabase : ScriptableObject
    {
        [SerializeField] private List<Item> _items;

        public Item GetItem(int id)
        {
            return _items
                .Where((Item item) => item.Id == id)
                .First();
        }
    }
}