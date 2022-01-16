using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services
{
    public interface IInputService : IService
    {
        public Vector2 Axis { get; }

        bool IsMeleeAttackButtonDown();
        bool IsShootButtonDown();
        bool IsDashButtonDown();
        bool IsHealButtonDown();
        bool IsInteractButtonDown();
    }
}
