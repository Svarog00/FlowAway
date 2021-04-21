using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gadget : MonoBehaviour
{
    public bool CanActivate { get; set; }
    [Header("Gadget Base")]

    public string gadgetName;

    protected GadgetManager GadgetManager;

    // Start is called before the first frame update
    public void Awake()
    {
        GadgetManager = FindObjectOfType<GadgetManager>();
        GadgetManager.OnGadgetActivate += GadgetManager_OnGadgetActivate;
    }

    private void GadgetManager_OnGadgetActivate(object sender, GadgetManager.OnGadgetActivateEventArgs e)
    {
        if (e.name == gadgetName)
        {
            CanActivate = true;
        }
    }
}
