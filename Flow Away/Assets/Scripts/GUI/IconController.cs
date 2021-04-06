using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconController : MonoBehaviour
{
    public string gadgetName;

    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        Gadget gadget = FindObjectOfType<Player_Movement>();
        gadget.OnGadgetCooldown += Dash_OnGadgetCooldown;
    }
    private void Dash_OnGadgetCooldown(object sender, Gadget.OnGadgetCooldownEventArgs e)
    {
        if (e.name == gadgetName)
        {
            _image.fillAmount = 0;
            ReturnNormalValue(e.curTime);
        }
    }

    private void ReturnNormalValue(float time)
    {
        while (time > 0f)
        {
            _image.fillAmount += 0.01f;
            Debug.Log(_image.fillAmount);
            time -= Time.deltaTime;
        }
    }
}
