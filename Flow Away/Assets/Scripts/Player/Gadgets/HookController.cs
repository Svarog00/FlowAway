using Assets.Scripts.Infrastructure.Factory;
using UnityEngine;

namespace Assets.Scripts.Player.Gadgets
{
    public class HookController : Gadget
    {
        private GameObject _hookInstance;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _hookInstance = _gameFactory.CreateHookInstance(gameObject.transform);
            _hookInstance.GetComponent<HookInstance>().Initialize(gameObject.transform);
            _hookInstance.SetActive(false);
        }

        public override void HandleActivate()
        {
            CastHook();
        }

        private void CastHook()
        {
            _hookInstance.SetActive(true);
        }
    }
}