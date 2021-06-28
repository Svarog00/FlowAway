using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Invisibility : Gadget
{

    [Header("Invisibility")]
    [SerializeField] private float _maxTime = 0f;
    private float _curTime;
    private float _fade = 1f;

    private Material _material;

    private bool _isActive;
    private bool _isChanging;

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

    private void Start()
    {
        _isActive = false;
        _isChanging = false;
        _material = GetComponentInChildren<SpriteRenderer>().material;
        CheckAviability();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isActive)
        {
            Disappear();
            _curTime -= Time.deltaTime;
            _gadgetManager.Timer(_curTime, _maxTime, _gadgetName);
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

    public override void HandleActivate()
    {
        if(CanActivate)
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
            _fade -= Time.deltaTime;
            if (_fade <= 0f)
            {
                _isChanging = false;
                _fade = 0f;
            }
        }
        _material.SetFloat("_Fade", _fade);
    }

    private void Appear()
    {
        if(_isChanging)
        {
            _fade += Time.deltaTime;
            if (_fade >= 1f)
            {
                _fade = 1f;
                _isActive = false;
                _isChanging = false;
                gameObject.tag = "Player";
            }
            _material.SetFloat("_Fade", _fade);
        }
    }
}
