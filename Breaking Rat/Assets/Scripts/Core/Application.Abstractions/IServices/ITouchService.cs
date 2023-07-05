using System;
using UnityEngine;

namespace BreakingRat.Application.Abstractions.IServices
{
    public interface ITouchService
    {
        public event Action<Vector2> TouchBegun;
        public event Action<Vector2> TouchContinues;
        public event Action<Vector2> TouchEnded;
    }
}
