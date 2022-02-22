using Assets.Scripts.Infrastructure.Factory;
using UnityEngine;

public class HookController : Gadget
{
    [SerializeField] private GameObject _hookInstance;

    private void Start()
    {
        _hookInstance.SetActive(false);

        CheckAvailability();
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