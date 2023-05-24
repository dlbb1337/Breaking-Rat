using System;
using UnityEngine;

namespace BreakingRat.Infrastructure.Services.Input.InputSystem
{
    public class InputService : MonoBehaviour
    {
        public PlayerControl PlayerControl { get; private set; }
        public event Action onEnable;
        public event Action onDisable;

        private void OnEnable()
        {
            PlayerControl = new();
            PlayerControl.Enable();
            onEnable?.Invoke();
        }

        private void OnDisable()
        {
            PlayerControl.Disable();
            onDisable?.Invoke();
        }
    }
}
