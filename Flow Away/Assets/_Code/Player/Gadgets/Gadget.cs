using UnityEngine;

public abstract class Gadget : MonoBehaviour
{
    [Header("Gadget Base")]
    [SerializeField] protected string _gadgetName;
    [SerializeField] protected GadgetManager GadgetManager;

    public bool IsUnlocked => _isUnlocked;

    private bool _isUnlocked;

    public void Awake()
    {
        GadgetManager.OnGadgetActivate += GadgetManager_OnGadgetActivate;
    }

    public abstract void HandleActivate();

    private void GadgetManager_OnGadgetActivate(object sender, GadgetManager.OnGadgetActivateEventArgs e)
    {
        if (e.name == _gadgetName)
        {
            _isUnlocked = true;
            GadgetManager.OnGadgetActivate -= GadgetManager_OnGadgetActivate;
        }
    }
}
