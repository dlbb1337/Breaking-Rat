using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreakingRat.GameLogic
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _interpolatePercentage;
        [SerializeField] private bool Y;
        [SerializeField] private bool X;
        [SerializeField] private bool Z;

        public void Construct(Transform target, Vector3 offset, float interpolatePercentage, bool y, bool x, bool z)
        {
            _target = target;
            _offset = offset;
            _interpolatePercentage = interpolatePercentage;
            Y = y;
            X = x;
            Z = z;
        }

        private void LateUpdate()
        {
            var targetPosition = new Vector3
                (X ? _target.position.x  : 0 , Y ? _target.position.y: 0 , Z ? _target.position.z: 0) + _offset;

            transform.position = Vector3.Lerp(transform.position, targetPosition, _interpolatePercentage * 0.01f);
        }
    }
}
