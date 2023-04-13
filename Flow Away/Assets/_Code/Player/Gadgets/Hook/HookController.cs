using Assets.Scripts.Infrastructure.Factory;
using UnityEngine;

public class HookController : Gadget
{
    [SerializeField] private float _cooldownTime;
    [SerializeField] private GameObject _hookInstance;

    private float _currentCooldownTime;
    
    private void Start()
    {
        _hookInstance.SetActive(false);
    }

    public override void HandleActivate()
    {
        if(!CanActivate)
        {
            return;
        }

        if(_currentCooldownTime > 0)
        {
            return;
        }

        CastHook();
    }

    private void CastHook()
    {
        _hookInstance.SetActive(true);
    }
}