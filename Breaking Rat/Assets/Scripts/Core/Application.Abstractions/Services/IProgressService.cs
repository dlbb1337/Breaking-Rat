namespace BreakingRat.Data.Services
{
    public interface IProgressService
    {
        Progress Progress { get; }

        void SaveProgress();

        void LoadProgress();
    }
}
