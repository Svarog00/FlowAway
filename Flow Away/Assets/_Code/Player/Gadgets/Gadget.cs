using UnityEngine;

public abstract class Gadget : MonoBehaviour
{
    public bool IsUnlocked => _isUnlocked;
    public string Name => _gadgetName;

    [Header("Gadget Base")]

    [SerializeField] protected string _gadgetName;
    [SerializeField] protected GadgetManager GadgetManager;

    private bool _isUnlocked;

    public abstract void HandleActivate();

    public void ToggleUnlock(bool value)
    {
        _isUnlocked = value;
    }
}
