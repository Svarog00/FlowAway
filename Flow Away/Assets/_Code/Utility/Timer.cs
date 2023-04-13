using System.Collections;
using System;
using UnityEngine;
using Assets.Scripts.Infrustructure;

class Timer
{
    private float _time;
    private Action _onTimeAction;
    private ICoroutineRunner _coroutineRunner;

    public Timer(ICoroutineRunner coroutineRunner, Action onTimeAction)
    {
        _coroutineRunner = coroutineRunner;
        _onTimeAction = onTimeAction;
    }

    public void StartTimer(float time)
    {
        _coroutineRunner.StartCoroutine(CountTime(time));
    }

    private IEnumerator CountTime(float time)
    {
        _time = time;
        while(_time >= 0)
        {
            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                _onTimeAction();
                yield break;
            }
            yield return null;
        }
    }
}
