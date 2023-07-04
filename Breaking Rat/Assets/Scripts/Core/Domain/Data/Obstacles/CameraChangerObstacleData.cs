using UnityEngine;

namespace BreakingRat.Data.Obstacles
{
    [CreateAssetMenu(fileName = "CameraChangerData", menuName = "StaticData/CameraChangerData")]
    public class CameraChangerObstacleData : ObstaclesStaticData
    {
        [SerializeField] private bool _fade;
        [SerializeField] private float _fadeDuration;
        [SerializeField] private float _coolDownTime;
        [SerializeField] private float _fadeSpeed;
        [SerializeField] private float _secondsToFadeStart;
        [SerializeField] private GameObject _fadecurtainPrefab;
        [SerializeField] private bool _resize;
        [SerializeField] private float _minCameraSize;
        [SerializeField] private float _maxCameraSize;
        [SerializeField] private float _resizeSpeed;
        [SerializeField] private float _resizeCameraDuration;

        public override int ObstacleId { get; } = 1;
        public float FadeDuration => _fadeDuration;
        public GameObject FadeCurtain => _fadecurtainPrefab;
        public float CoolDownTime => _coolDownTime;
        public float MinCameraSize => _minCameraSize;
        public float MaxCameraSize => _maxCameraSize;
        public float ResizeCameraDuration => _resizeCameraDuration;
        public float FadeSpeed => _fadeSpeed;
        public bool Resize => _resize;
        public bool Fade => _fade;
        public float ResizeSpeed => _resizeSpeed;
        public float SecondsToFadeStart => _secondsToFadeStart;
    }
}
