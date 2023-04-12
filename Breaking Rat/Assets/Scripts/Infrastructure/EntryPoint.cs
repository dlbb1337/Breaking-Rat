using BreakingRat.CompositionRoot;
using UnityEngine;

namespace BreakingRat.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
        private InfrastructureDependencies _dependencies;
        private Game _game;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            _dependencies = new();

            _game = new(_dependencies.Machine);
        }
    }
}
