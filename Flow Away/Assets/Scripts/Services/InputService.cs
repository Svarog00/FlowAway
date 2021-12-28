
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class InputService : IInputService
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        private const string MeleeStrike = "MeleeStrike";
        private const string GunFire = "GunFire";
        private const string DashButton = "Dash";
        private const string HealButton = "Heal";

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
    }
}
