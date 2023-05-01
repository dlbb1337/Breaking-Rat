using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreakingRat.Infrastructure
{
    public interface IPayLoadedState<TPay>:IExitableState
    {
        void Enter(TPay pay);
    }
}
