using UnityEngine;

public class GadgetActivator : MonoBehaviour
{
    private const string PlayerTag = "Player";
    
    public string GadgetName;
    
    private GadgetManager _gadgetManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.tag == PlayerTag)
        {
            if(_gadgetManager == null)
            {
                _gadgetManager = collision.gameObject.GetComponent<GadgetManager>();
            }
            Activate();
        }
    }

    private void Activate()
    {
        _gadgetManager.ActivateGadget(GadgetName);
    }
}
