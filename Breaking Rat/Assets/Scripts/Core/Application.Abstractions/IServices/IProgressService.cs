using BreakingRat.Domain.Entities;

namespace BreakingRat.Application.Abstractions.IServices
{
    public interface IProgressService
    {
        Progress Progress { get; }

        void SaveProgress();

        void LoadProgress();
    }
}
