using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    public event EventHandler<OnInvisibilityEnableEventArgs> OnInsibilityEnable;
    public class OnInvisibilityEnableEventArgs
    {
        public bool isActive;
    }

    [SerializeField] private float time = 0f;

    Material material;
    private bool _canActivate;
    private bool _isActive;
    private bool _isChanging;
    private float _curTime;
    private float fade = 1f;

    private void Start()
    {
        _canActivate = QuestValues.Instance.GetStage("Invisibility") > 0 ? true : false;
        //_canActivate = true;
        _isActive = false;
        _isChanging = false;
        material = GetComponentInChildren<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && _canActivate)
        {
            if (!_isActive)
            {
                gameObject.tag = "InvisiblePlayer";
                _isActive = true;
                _isChanging = true;
                _curTime = time;
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
