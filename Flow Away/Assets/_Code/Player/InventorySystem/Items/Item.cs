using System;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items/Item", order = 1)]
    public class Item : ScriptableObject, IUsable
    {
        public int Id;
        public string Name;
        public string Description;
        public bool IsUsable = false;

        public Sprite Image;

        public virtual void Use(GameObject user)
        {
            
        }
    }
}