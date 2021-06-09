using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityControls : MonoBehaviour
{

    private HotkeysSystem _hotkeysSystem;

    // Start is called before the first frame update
    void Start()
    {
        _hotkeysSystem = new HotkeysSystem(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        _hotkeysSystem.GetInput();
    }
}
