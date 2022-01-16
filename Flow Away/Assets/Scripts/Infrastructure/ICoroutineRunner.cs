using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrustructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}