using BreakingRat.Assets.Scripts.Infrastructure.Persistence.Services.Input.InputSystem;
using System;
using UnityEngine;

namespace BreakingRat.Assets.Scripts.Infrastructure.Persistence.Services.InputSystem
{
    public class InputService : MonoBehaviour
    {
        public PlayerControl PlayerControl { get; private set; }
        public event Action onEnable;
        public event Action onDisable;

        public void OnEnable()
        {
            PlayerControl = new();
            PlayerControl.Enable();
            onEnable?.Invoke();
        }

        public void OnDisable()
        {
            PlayerControl.Disable();
            onDisable?.Invoke();
        }
    }
}
