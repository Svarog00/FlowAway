using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gadget : MonoBehaviour
{
    [Header("Gadget Base")]
    [SerializeField] protected string _gadgetName;
    protected GadgetManager _gadgetManager;

    public bool CanActivate { get; set; }
    // Start is called before the first frame update
    public void Awake()
    {
        _gadgetManager = FindObjectOfType<GadgetManager>();
        _gadgetManager.OnGadgetActivate += GadgetManager_OnGadgetActivate;
    }

    public virtual void HandleActivate() { }

    protected void CheckAviability()
    {
        CanActivate = QuestValues.Instance.GetStage(_gadgetName) > 0;
    }

    private void GadgetManager_OnGadgetActivate(object sender, GadgetManager.OnGadgetActivateEventArgs e)
    {
        if (e.name == _gadgetName)
        {
            CanActivate = true;
            _gadgetManager.OnGadgetActivate -= GadgetManager_OnGadgetActivate;
        }
    }
}
