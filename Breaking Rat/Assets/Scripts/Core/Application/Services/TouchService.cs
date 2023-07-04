using BreakingRat.Assets.Scripts.Core.Application.Abstractions.Services;
using BreakingRat.Assets.Scripts.Infrastructure.Persistence.Services.InputSystem;
using System;
using UnityEngine;

namespace BreakingRat.Assets.Scripts.Core.Application.Services
{
    public class TouchService : ITouchService
    {
        private readonly InputService _inputService;

        public event Action<Vector2> TouchBegun;
        public event Action<Vector2> TouchContinues;
        public event Action<Vector2> TouchEnded;

        public TouchService(InputService inputService)
        {
            _inputService = inputService;

            _inputService.onEnable += () =>
            {
                _inputService.PlayerControl.ScreenInput.Turn.started += Turn_started;
                _inputService.PlayerControl.ScreenInput.Turn.performed += Turn_performed;
                _inputService.PlayerControl.ScreenInput.Turn.canceled += Turn_canceled;
            };

            _inputService.onDisable += () =>
            {
                _inputService.PlayerControl.ScreenInput.Turn.started -= Turn_started;
                _inputService.PlayerControl.ScreenInput.Turn.performed -= Turn_performed;
                _inputService.PlayerControl.ScreenInput.Turn.canceled -= Turn_canceled;
            };

            _inputService.OnDisable();
            _inputService.OnEnable();
        }

        private void Turn_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            TouchContinues?.Invoke(obj.ReadValue<Vector2>());
        }

        private void Turn_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            TouchBegun?.Invoke(obj.ReadValue<Vector2>());
        }

        private void Turn_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            TouchEnded?.Invoke(obj.ReadValue<Vector2>());
        }
    }
}
