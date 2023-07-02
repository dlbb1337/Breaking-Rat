using BreakingRat.UI.Factory;
using GameConsole.CommandTools;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace BreakingRat.Commands
{
    public class UIFactoryCommandContainer : ICommandContainer
    {
        private readonly IUIFactory _factory;
        private readonly List<CanvasGroup> _curtains = new();
        private readonly CoroutineRunner _runner = CoroutineRunner.instance;
        private IEnumerator enumerator;

        public UIFactoryCommandContainer(IUIFactory factory)
        {
            enumerator = InnerCoroutine();
            _factory = factory;
        }

        public void ResizeCamera(float size)
        {
            var camera = Camera.main;
            Debug.Log($"size before: {camera.orthographicSize}");
            camera.orthographicSize = size;
            Debug.Log($"size after: {camera.orthographicSize}");
        }

        public async Task CreateCurtainAsync()
        {
            _curtains.Add(await _factory.InstantiateCurtainAsync());
        }

        public void DestroyCurtain()
        {
            if (_curtains.Count > 0)
            {
                Object.Destroy(_curtains[0].GetComponentInParent<Canvas>().gameObject);
                _curtains.RemoveAt(0);
            }
        }

        public void StartCoroutine()
        {
            enumerator = InnerCoroutine();
            _runner.StartCoroutine(enumerator);
        }

        public void StopCoroutine()
        {
            _runner.StopCoroutine(enumerator);
        }

        public void StopAllCoroutines()
        {
            _runner.StopAllCoroutines();
        }

        private IEnumerator Coroutine()
        {
            _runner.StartCoroutine(InnerCoroutine());
            yield return null;
        }

        private IEnumerator InnerCoroutine()
        {
            int i = 0;
            while (i < 5)
            {
                i++;
                yield return null;
            }
        }
    }
}
