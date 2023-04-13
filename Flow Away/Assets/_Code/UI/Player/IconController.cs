using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconController : MonoBehaviour
{
	[SerializeField] private string _gadgetName;
	[SerializeField] private GameObject _visual;
	[SerializeField] private Image _image;

	private bool _canActivate = false;

	private bool _isFilling;

	private void Start()
	{
		_canActivate = QuestValues.Instance.GetStage(_gadgetName) > 0;
		_visual.SetActive(_canActivate); 
		
		GadgetManager gadget = FindObjectOfType<GadgetManager>();
		if(_canActivate)
			gadget.OnGadgetCooldown += OnGadgetCooldown;
		else
			gadget.OnGadgetActivate += Gadget_OnGadgetActivate;

		_isFilling = false;
	}
	//Icon should appear after binded gadget activated
    private void Gadget_OnGadgetActivate(object sender, GadgetManager.OnGadgetActivateEventArgs e)
    {
		if(_gadgetName == e.name)
        {
			_visual.SetActive(true);
			GadgetManager gadget = FindObjectOfType<GadgetManager>();
			gadget.OnGadgetCooldown += OnGadgetCooldown;
			gadget.OnGadgetActivate -= Gadget_OnGadgetActivate;
		}
    }
	//Changing icon appearance due to timer
    private void OnGadgetCooldown(object sender, GadgetManager.OnGadgetCooldownEventArgs e)
	{
		if (e.name == _gadgetName && !_isFilling)
		{
			_image.fillAmount = 0;
			_isFilling = true;
		}
		else if(e.name == _gadgetName && _isFilling)
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
