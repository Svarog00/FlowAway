using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Invisibility : Gadget
{
    [Header("Invisibility")]
    [SerializeField] private float _maxTime = 0f;
    private float _curActiveTime;
    private float _fade = 1f;

    [SerializeField] private float _cooldownTime;
    private float _cooldownCurTime;

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
        get => _maxTime;
        set => _maxTime = value;
    }

    private void Start()
    {
        _isActive = false;
        _isChanging = false;

        _material = GetComponentInChildren<SpriteRenderer>().material;
    }

    private void Update()
    {
        if(_isActive)
        {
            _curActiveTime -= Time.deltaTime;
            
            if(_curActiveTime <= 0f)
            {
                _isActive = false;
                _isChanging = true;
                _cooldownCurTime = _cooldownTime;
                StartCoroutine(Appear());
                StartCoroutine(CooldownAbility());
            }
        }
    }

    public override void HandleActivate()
    {
        if(!IsUnlocked || _cooldownCurTime > 0f)
        {
            return;
        }

        if (!_isActive)
        {
            _isChanging = true;
            _curActiveTime = _maxTime;
            StartCoroutine(Disappear());

            OnInsibilityEnable?.Invoke(this, new OnInvisibilityEnableEventArgs { isActive = _isActive });
        }
        else
        {
            _isChanging = true;
            StartCoroutine(Appear());

            OnInsibilityEnable?.Invoke(this, new OnInvisibilityEnableEventArgs { isActive = _isActive });
        }
    }

    private IEnumerator CooldownAbility()
    {
        while(_cooldownCurTime > 0f)
        {
            _cooldownCurTime -= Time.deltaTime;
            GadgetManager.CooldownTimer(_cooldownCurTime, _cooldownTime, _gadgetName);

            yield return null;
        }
    }

    private IEnumerator Appear()
    {
        while(_isChanging)
        {
            _fade += Time.deltaTime;
            if (_fade >= 1f)
            {
                _fade = 1f;
                _isActive = false;
                _isChanging = false;
                gameObject.tag = "Player";

                yield break;
            }
            _material.SetFloat("_Fade", _fade);

            yield return null;
        }
    }

    private IEnumerator Disappear()
    {
        while (_isChanging)
        {
            _fade -= Time.deltaTime;
            if (_fade <= 0f)
            {
                _fade = 0f;
                _isActive = true;
                _isChanging = false;
                gameObject.tag = "InvisiblePlayer";

                yield break;
            }
            _material.SetFloat("_Fade", _fade);

            yield return null;
        }
    }

}
