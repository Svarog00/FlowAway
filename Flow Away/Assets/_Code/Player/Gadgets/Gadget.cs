using UnityEngine;

public abstract class Gadget : MonoBehaviour
{
    [Header("Gadget Base")]
    [SerializeField] protected string _gadgetName;
    [SerializeField] protected GadgetManager GadgetManager;

    public bool CanActivate 
    { 
        get => QuestValues.Instance.GetStage(_gadgetName) > 0; 
        private set => CanActivate = value;
    }

    public void Awake()
    {
        GadgetManager.OnGadgetActivate += GadgetManager_OnGadgetActivate;
    }

    public abstract void HandleActivate();

    private void GadgetManager_OnGadgetActivate(object sender, GadgetManager.OnGadgetActivateEventArgs e)
    {
        if (e.name == _gadgetName)
        {
            CanActivate = true;
            GadgetManager.OnGadgetActivate -= GadgetManager_OnGadgetActivate;
        }
    }
}
