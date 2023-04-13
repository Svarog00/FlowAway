using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services
{
    public class InputService : IInputService
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        private const string DashButton = "Dash";

        private const string MeleeStrike = "MeleeStrike";
        private const string GunFire = "GunFire";

        private const string HealButton = "Heal";
        private const string InteractButton = "Interact";
        private const string InvetoryButton = "OpenInventory";

        public Vector2 Axis =>
            new Vector2(Input.GetAxis(HorizontalAxis), Input.GetAxis(VerticalAxis));

        public bool IsDashButtonDown() =>
            Input.GetButtonDown(DashButton);

        public bool IsHealButtonDown() =>
            Input.GetButtonDown(HealButton);

        public bool IsMeleeAttackButtonDown() =>
            Input.GetButtonDown(MeleeStrike);

        public bool IsShootButtonDown() =>
            Input.GetButtonDown(GunFire);

        public bool IsInteractButtonDown() =>
            Input.GetButtonDown(InteractButton);

        public bool IsAbilityButtonDown(string abilityButtonName) =>
            Input.GetButtonDown(abilityButtonName);

        public bool IsOpenInventoryButtonDown() =>
            Input.GetButtonDown(InvetoryButton);
    }
}
