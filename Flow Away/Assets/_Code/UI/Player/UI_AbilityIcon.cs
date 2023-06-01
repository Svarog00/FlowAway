using UnityEngine;
using UnityEngine.UI;

public class UI_AbilityIcon : MonoBehaviour
{
	[SerializeField] private string _gadgetName;
	[SerializeField] private GameObject _visual;
	[SerializeField] private Image _image;

	private bool _canActivate = false;

	private bool _isFilling = false;

	private void Start()
	{
		_canActivate = QuestValues.Instance.GetStage(_gadgetName) > 0;
		_visual.SetActive(_canActivate); 
		
		GadgetManager gadget = FindObjectOfType<GadgetManager>();
        gadget.OnGadgetActivate += Gadget_OnGadgetActivate;

		if(_canActivate)
		{
            gadget.OnGadgetCooldown += OnGadgetCooldown;
		}
	}

	//Icon should appear after binded gadget activated
    private void Gadget_OnGadgetActivate(object sender, GadgetManager.OnGadgetActivateEventArgs e)
    {
		if(_gadgetName == e.Name)
        {
            _visual.SetActive(e.IsActive);
			GadgetManager gadget = FindObjectOfType<GadgetManager>();
			gadget.OnGadgetCooldown += OnGadgetCooldown;
		}
    }
	//Changing icon appearance due to timer
    private void OnGadgetCooldown(object sender, GadgetManager.OnGadgetCooldownEventArgs e)
	{
        //_visual.SetActive(true);

        if (e.name != _gadgetName)
		{
			return;
		}

		if (!_isFilling)
		{
			_image.fillAmount = 0;
			_isFilling = true;
		}
		else
        {
			ChangeIconVisibility(e.curTime);
			if (_image.fillAmount == 1)
				_isFilling = false;
        }
	}

	private void ChangeIconVisibility(float time)
	{
		_image.fillAmount = time;
	}
}
