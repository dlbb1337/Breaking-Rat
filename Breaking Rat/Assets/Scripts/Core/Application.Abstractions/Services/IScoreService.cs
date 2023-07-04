using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreakingRat.GameLogic.Services
{
    public abstract class IScoreService 
    {
        public virtual int Score { get; private set; } = 0;

        public static IScoreService operator ++(IScoreService scoreService)
        {
            scoreService.Score++;
            return scoreService;
        }

        public static implicit operator int(IScoreService scoreService)
        {
            return scoreService.Score;
        }
    }
}
