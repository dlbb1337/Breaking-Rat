using BreakingRat.Application.CompositionRoot;
using BreakingRat.Application.UI;
using BreakingRat.Infrastructure.CompositionRoot;
using UnityEngine;
using Zenject;

namespace BreakingRat.Presentation.CompositionRoot
{
    public class ProjectContextInstaller : MonoInstaller
    {
        [SerializeField] private GameCurtain _gameCurtain;

        public override void InstallBindings()
        {
            Container
                .BindDataServices()
                .BindSceneLoader()
                .BindGameStateMachine(_gameCurtain);
        }
    }
}