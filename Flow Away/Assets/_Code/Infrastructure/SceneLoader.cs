using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infrustructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action onLoaded = null, LoadSceneMode sceneMode = LoadSceneMode.Single)
        {
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded, sceneMode));
        }

        private IEnumerator LoadScene(string name, Action onLoaded = null, LoadSceneMode sceneMode = LoadSceneMode.Single)
        {
            if(SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name, sceneMode);

            while(!waitNextScene.isDone)
            {
                yield return null;
            }

            onLoaded?.Invoke();
        }
    }
}