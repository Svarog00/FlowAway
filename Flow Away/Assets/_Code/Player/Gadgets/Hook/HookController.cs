using UnityEngine;

public class HookController : Gadget
{
    [SerializeField] private float _cooldownTime;
    [SerializeField] private GameObject _hookInstance;

    private void Start()
    {
        _hookInstance.SetActive(false);
    }

    public override void HandleActivate()
    {
        if(!IsUnlocked)
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