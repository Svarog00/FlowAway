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
    [Header("Invisibility")]
    private Material material;

    private bool _isActive;
    private bool _isChanging;

    [SerializeField] private float _maxTime = 0f;
    private float _curTime;
    private float fade = 1f;

    private void Start()
    {
        CanActivate = QuestValues.Instance.GetStage(gadgetName) > 0 ? true : false;
        _isActive = false;
        _isChanging = false;
        material = GetComponentInChildren<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Control();

        if(_isActive)
        {
            Disappear();
            _curTime -= Time.deltaTime;
            GadgetManager.Timer(_curTime, _maxTime, gadgetName);
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

    private void Control()
    {
        if (Input.GetButtonDown("First Module") && CanActivate)
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
