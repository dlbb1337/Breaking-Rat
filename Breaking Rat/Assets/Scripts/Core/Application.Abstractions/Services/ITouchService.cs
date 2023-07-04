using System;
using UnityEngine;

namespace BreakingRat.Assets.Scripts.Core.Application.Abstractions.Services
{
    public interface ITouchService
    {
        public event Action<Vector2> TouchBegun;
        public event Action<Vector2> TouchContinues;
        public event Action<Vector2> TouchEnded;
    }
}
