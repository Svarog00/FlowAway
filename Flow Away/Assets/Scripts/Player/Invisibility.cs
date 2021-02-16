using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Push");
            if (gameObject.tag == "Player") 
                gameObject.tag = "InvisiblePlayer";
            else gameObject.tag = "Player";
        }
    }
}
