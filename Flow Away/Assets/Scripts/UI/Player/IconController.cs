using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconController : MonoBehaviour
{
	public string gadgetName;

	private Image _image;

	private bool _isFilling;

	private void Start()
	{
		_image = GetComponent<Image>();
		GadgetManager gadget = FindObjectOfType<GadgetManager>();
		Player_Health player_Health = FindObjectOfType<Player_Health>();
        player_Health.OnDeath += Player_Health_OnDeath;
		gadget.OnGadgetCooldown += OnGadgetCooldown;
		_isFilling = false;
	}

    private void Player_Health_OnDeath(object sender, EventArgs e)
    {
        
    }

    private void OnGadgetCooldown(object sender, GadgetManager.OnGadgetCooldownEventArgs e)
	{
		if (e.name == gadgetName && !_isFilling)
		{
			_image.fillAmount = 0;
			_isFilling = true;
		}
		else if(e.name == gadgetName && _isFilling)
        {
			ReturnNormalValue(e.curTime);
			if (_image.fillAmount == 1)
				_isFilling = false;
        }
	}

	private void ReturnNormalValue(float time)
	{
		_image.fillAmount = time;
	}
}
