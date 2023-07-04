using BreakingRat.GameLogic.Services;
using UnityEngine;
using Zenject;

namespace BreakingRat
{
    public class FadeCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        private float _multiplier = .01f;
        private bool _fadeIn;
        private float _cooldownTime;
        private float _durationTime;
        private IPauseService _pauseService;
        private bool _isPaused = true;

        [Inject]
        private void Construct(IPauseService pauseService)
        {
            _pauseService = pauseService;

            _pauseService.Pause += _pauseService_Pause;
            _pauseService.Unpause += _pauseService_Unpause;
        }

        private void OnEnable()
        {
            if (_pauseService is null == false)
            {
                _pauseService.Pause += _pauseService_Pause;
                _pauseService.Unpause += _pauseService_Unpause;
            }
        }

        private void OnDisable()
        {
            _pauseService.Pause -= _pauseService_Pause;
            _pauseService.Unpause -= _pauseService_Unpause;
        }

        private void _pauseService_Unpause()
        {
            _isPaused = false;
        }

        private void _pauseService_Pause()
        {
            _isPaused = true;
        }

        public float TimeBeforeStart { get; set; }
        public float Cooldown { get; set; }
        public float Duration { get; set; }
        public float Speed { get; set; }
        public CanvasGroup CanvasGroup => _canvasGroup;

        public void FadeIn()
        {
            _canvasGroup.alpha += Speed * _multiplier;
        }

        public void FadeOut()
        {
            _canvasGroup.alpha -= Speed * _multiplier;
        }

        private void FixedUpdate()
        {
            if (_isPaused) return;

            _cooldownTime += Time.fixedDeltaTime;

            if (CooldownTimePassed() == false)
            {
                return;
            }

            _canvasGroup.alpha = 0;
            FadeIn();

            _durationTime += Time.fixedDeltaTime;

            if (DurationTimePassed())
            {
                _cooldownTime = 0;
                _durationTime = 0;
            }

        }

        private void Fade()
        {
            if (_fadeIn && _canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += Speed * _multiplier;
            }
            else if (_fadeIn == false && _canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= Speed * _multiplier;
            }
        }

        private bool DurationTimePassed()
        {
            return _durationTime > Duration;
        }

        private bool CooldownTimePassed()
        {
            return _cooldownTime > Cooldown;
        }


        //private IEnumerator DoFadeIn(CanvasGroup curtain, float speed)
        //{
        //    curtain.alpha = 0;
        //    var multiplier = .01f;

        //    while (curtain.alpha <= 1)
        //    {
        //        curtain.alpha += speed * multiplier;
        //        yield return new WaitForSeconds(.1f);
        //    }
        //}

        //private IEnumerator DoFadeOut(CanvasGroup curtain, float speed)
        //{
        //    var multiplier = .01f;
        //    while (curtain.alpha > 0)
        //    {
        //        curtain.alpha -= speed * multiplier;
        //        yield return new WaitForSeconds(.1f);
        //    }
        //    UnityEngine.Object.Destroy(curtain.GetComponentInParent<Canvas>().gameObject);
        //}
    }
}
