using UnityEngine;
using Zenject;

namespace BreakingRat.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        private Game _game;

        [Inject]
        private void Construct(Game game)
        {
            _game = game;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }


    }
}
