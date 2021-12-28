
using UnityEngine;

namespace Assets.Scripts.Services
{
    public interface IInputService
    {
        public Vector2 Axis { get; }

        public bool IsMeleeAttackButtonDown();
        public bool IsShootButtonDown();
        public bool IsDashButtonDown();
        public bool IsHealButtonDown();
    }
}
