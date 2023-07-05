using BreakingRat.Application.Abstractions.IServices;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BreakingRat.Infrastructure.Persistence.SceneManagment
{
    public class SceneLoader : ISceneLoaderService
    {
        private readonly CoroutineRunner _coroutineRunner;

        public SceneLoader()
        {
            _coroutineRunner = CoroutineRunner.instance;
        }

        public void LoadScene(string sceneName, Action onLoaded)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            var coroutine = _coroutineRunner.StartCoroutine(Wait(asyncOperation, onLoaded));
        }

        private IEnumerator Wait(AsyncOperation operation, Action onLoaded)
        {
            while (operation.isDone == false)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}
