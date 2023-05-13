using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "UsableItem", menuName = "ScriptableObjects/Items/UsableItem", order = 1)]
    public class UsableRandomItem : Item
    {
        public override void Use(GameObject user)
        {
            user.GetComponent<IHealable>().Heal();
        }
    }
}