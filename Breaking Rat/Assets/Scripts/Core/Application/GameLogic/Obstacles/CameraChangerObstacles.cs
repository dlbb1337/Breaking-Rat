using BreakingRat.Data.Obstacles;
using BreakingRat.GameLogic.Location.MazeLogic;
using BreakingRat.UI;
using BreakingRat.UI.Factory;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace BreakingRat.GameLogic.Obstacles
{
    public class CameraChangerObstacles : IObstacles
    {
        //private readonly CoroutineRunner _coroutineRunner;
        //private readonly IUIFactory _UIFactory;
        //private bool _screenFaderStarted = false;
        //private IEnumerator _screenFader;
        //private IEnumerator _decreaserSize;

        public int ObstacleId { get; } = 1;

        public CameraChangerObstacles(IUIFactory UIFactory)
        {
            //_coroutineRunner = CoroutineRunner.instance;
            //_UIFactory = UIFactory;
        }

        public Task AddAsync(Maze maze, ObstaclesStaticData data)
        {
            //var cameraChanger = data as CameraChangerObstacleData;
            //if (cameraChanger is null)
            //    return;

            //if (cameraChanger.Resize)
            //    InitializeResizer(maze, cameraChanger);

            //if (cameraChanger.Fade && _screenFaderStarted == false)
            //    StartScreenFader(cameraChanger);
            return Task.CompletedTask;
        }

        //    private void InitializeResizer(Maze maze, CameraChangerObstacleData cameraChanger)
        //    {
        //        if (_decreaserSize is null)
        //            _decreaserSize = ChangeCameraSize(cameraChanger.MinCameraSize, false, cameraChanger.ResizeSpeed);

        //        maze.ExitTrigger.Enter.AddListener(collider => OnMazeExit(cameraChanger));

        //        LaunchSizeChanger(cameraChanger);
        //    }

        //    private void OnMazeExit(CameraChangerObstacleData cameraChanger)
        //    {
        //        LaunchSizeChanger(cameraChanger);
        //    }

        //    private void LaunchSizeChanger(CameraChangerObstacleData cameraChanger)
        //    {
        //        _coroutineRunner.StartCoroutine(SizeChanger(cameraChanger));
        //    }

        //    private IEnumerator SizeChanger(CameraChangerObstacleData cameraChanger)
        //    {
        //        _coroutineRunner.StopCoroutine(_decreaserSize);
        //        var increaserSize = _coroutineRunner.StartCoroutine
        //            (ChangeCameraSize(cameraChanger.MaxCameraSize, true, cameraChanger.ResizeSpeed));

        //        yield return new WaitForSeconds(cameraChanger.ResizeCameraDuration);

        //        _coroutineRunner.StopCoroutine(increaserSize);
        //        _decreaserSize = ChangeCameraSize(cameraChanger.MinCameraSize, false, cameraChanger.ResizeSpeed);
        //        _coroutineRunner.StartCoroutine(_decreaserSize);
        //    }

        //    private IEnumerator ChangeCameraSize(float size, bool increase, float speed)
        //    {
        //        var camera = Camera.main;
        //        var multiplier = .001f;

        //        if (increase)
        //        {
        //            if (camera.orthographicSize >= size)
        //                yield break;


        //            var follow = camera.GetComponent<Follow>();
        //            follow.X = false;

        //            while (camera.orthographicSize < size)
        //            {
        //                camera.orthographicSize += speed * multiplier;

        //                yield return null;
        //            }

        //            camera.orthographicSize = size;
        //        }
        //        else
        //        {
        //            if (camera.orthographicSize <= size)
        //                yield break;

        //            var follow = camera.GetComponent<Follow>();
        //            follow.X = true;
        //            while (camera.orthographicSize > size)
        //            {
        //                camera.orthographicSize -= speed * multiplier;

        //                yield return null;
        //            }

        //            camera.orthographicSize = size;
        //        }
        //    }

        //    private void StartScreenFader(CameraChangerObstacleData cameraChanger)
        //    {
        //        _screenFaderStarted = true;
        //        if (_screenFader is null)
        //            _screenFader = ScreenFader(cameraChanger);

        //        if (_screenFader is null == false)
        //            _coroutineRunner.StopCoroutine(_screenFader);

        //        _coroutineRunner.StartCoroutine(_screenFader);
        //    }

        //    private IEnumerator ScreenFader(CameraChangerObstacleData data)
        //    {
        //        yield return new WaitForSecondsRealtime(data.SecondsToFadeStart);
        //        while (true)
        //        {
        //            yield return new WaitForSecondsRealtime(data.CoolDownTime);
        //            _coroutineRunner.StartCoroutine(FadeScreen(data.FadeDuration,data.FadeSpeed ));
        //        }
        //    }

        //    private IEnumerator FadeScreen(float duration, float speed)
        //    {
        //        var curtain = _UIFactory.InstantiateCurtainAsync();
        //        curtain.alpha = 0;

        //        var fadeIn = DoFadeIn(curtain,speed);
        //        var fadeOut = DoFadeOut(curtain, speed);

        //        Show(fadeIn);

        //        yield return new WaitForSecondsRealtime(duration);

        //        Close(fadeIn, fadeOut);
        //    }

        //    private void Close(IEnumerator fadeIn, IEnumerator fadeOut)
        //    {
        //        _coroutineRunner.StopCoroutine(fadeIn);
        //        _coroutineRunner.StartCoroutine(fadeOut);
        //    }

        //    private void Show(IEnumerator fadeIn)
        //    {
        //        _coroutineRunner.StartCoroutine(fadeIn);
        //    }

        //    private IEnumerator DoFadeIn(CanvasGroup curtain, float speed)
        //    {
        //        curtain.alpha = 0;
        //        var multiplier = .01f;

        //        while (curtain.alpha <= 1)
        //        {
        //            curtain.alpha += speed * multiplier;
        //            yield return new WaitForSeconds(.1f);
        //        }
        //    }

        //    private IEnumerator DoFadeOut(CanvasGroup curtain, float speed)
        //    {
        //        var multiplier = .01f;
        //        while (curtain.alpha > 0)
        //        {
        //            curtain.alpha -= speed * multiplier;
        //            yield return new WaitForSeconds(.1f);
        //        }
        //        UnityEngine.Object.Destroy(curtain.GetComponentInParent<Canvas>().gameObject);
        //    }
    }
}
