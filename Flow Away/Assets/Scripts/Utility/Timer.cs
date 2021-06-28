using System.Collections;
using System;
using UnityEngine;

class Timer : MonoBehaviour
{
    private float _time;
    private Action _onTimeAction;

    public Action Action
    {
        private get => _onTimeAction;
        set => _onTimeAction = value;
    }

    private void Update()
    {
        if(_time >= 0)
        {
            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                _onTimeAction();
            }
        }
    }

    public void SetTime(float time)
    {
        _time = time;
    }
}
