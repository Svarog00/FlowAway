using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Invisibility : Gadget
{
    public event EventHandler<OnInvisibilityEnableEventArgs> OnInsibilityEnable;

    public class OnInvisibilityEnableEventArgs
    {
        public bool isActive;
    }

    public float MaxTime 
    {
        get { return _maxTime; }
        set { _maxTime = value; }
    }

    public GadgetActivator gadgetActivator;
    public bool CanActivate { get; set; }

    Material material;
    [SerializeField] private float _maxTime = 0f;
    private bool _isActive;
    private bool _isChanging;
    private float _curTime;
    private float fade = 1f;

    public void Awake()
    {
        if(gadgetActivator)
        {
            gadgetActivator.OnGadgetActivated += GadgetActivator_OnGadgetActivated;
        }
    }

    private void GadgetActivator_OnGadgetActivated(object sender, GadgetActivator.OnGadgetActivatedEventArgs e)
    {
        if(e.gadgetName == "Invisibility")
        {
            CanActivate = true;
        }
    }

    private void Start()
    {
        CanActivate = QuestValues.Instance.GetStage("Invisibility") > 0 ? true : false;
        _isActive = false;
        _isChanging = false;
        material = GetComponentInChildren<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("First Module") && CanActivate)
        {
            if (!_isActive)
            {
                gameObject.tag = "InvisiblePlayer";
                _isActive = true;
                _isChanging = true;
                _curTime = MaxTime;
                OnInsibilityEnable?.Invoke(this, new OnInvisibilityEnableEventArgs { isActive = _isActive });
            }
            else
            {
                gameObject.tag = "Player";
                _isChanging = true;
                _isActive = false;
                OnInsibilityEnable?.Invoke(this, new OnInvisibilityEnableEventArgs { isActive = _isActive });
            }
        }

        if(_isActive)
        {
            Disappear();
            _curTime -= Time.deltaTime;
            base.Timer(_curTime, "Invisibility");
            if(_curTime <= 0f)
            {
                _isActive = false;
                _isChanging = true;
            }
        }

        if(!_isActive)
        {
            Appear();
        }    
    }

    private void Disappear()
    {
        if (_isChanging == true)
        {
            fade -= Time.deltaTime;
            if (fade <= 0f)
            {
                _isChanging = false;
                fade = 0f;
            }
        }
        material.SetFloat("_Fade", fade);
    }

    private void Appear()
    {
        if(_isChanging)
        {
            fade += Time.deltaTime;
            if (fade >= 1f)
            {
                fade = 1f;
                _isActive = false;
                _isChanging = false;
                gameObject.tag = "Player";
            }
            material.SetFloat("_Fade", fade);
        }
    }
}
