using UnityEngine;

public abstract class Gadget : MonoBehaviour
{
    [Header("Gadget Base")]
    [SerializeField] protected string _gadgetName;
    protected GadgetManager GadgetManager;

    public bool CanActivate { get; set; }

    public void Awake()
    {
        GadgetManager = FindObjectOfType<GadgetManager>();
        GadgetManager.OnGadgetActivate += GadgetManager_OnGadgetActivate;
    }

    public abstract void HandleActivate();

    protected void CheckAvailability()
    {
        CanActivate = QuestValues.Instance.GetStage(_gadgetName) > 0;
    }

    private void GadgetManager_OnGadgetActivate(object sender, GadgetManager.OnGadgetActivateEventArgs e)
    {
        if (e.name == _gadgetName)
        {
            CanActivate = true;
            GadgetManager.OnGadgetActivate -= GadgetManager_OnGadgetActivate;
        }
    }
}
