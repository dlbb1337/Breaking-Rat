using System;
using UnityEngine;

namespace BreakingRat.Infrastructure.Services.Input
{
    public interface ITouchService 
    {
        public event Action<Vector2> TouchBegun;
        public event Action<Vector2> TouchContinues;
        public event Action<Vector2> TouchEnded;
    }
}
