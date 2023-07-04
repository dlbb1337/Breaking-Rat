using BreakingRat.Assets.Scripts.Core.Domain.Entities;

namespace BreakingRat.Assets.Scripts.Core.Application.Abstractions.Services
{
    public interface IProgressService
    {
        Progress Progress { get; }

        void SaveProgress();

        void LoadProgress();
    }
}
