using BreakingRat.Application.Abstractions.IServices;

namespace BreakingRat.Application.Services
{
    public class RecordService : IRecordService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IProgressService _progressService;
        private readonly IScoreService _scoreService;

        public RecordService(IStaticDataService staticDataService, IProgressService progressService, IScoreService scoreService)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
            _scoreService = scoreService;
        }

        public int Record => _progressService.Progress.Records[_staticDataService.CurrentLevelStaticData.LevelId];

        public void UpdateRecord()
        {
            var levelId = _staticDataService.CurrentLevelStaticData.LevelId;
            int score = _scoreService;
            var records = _progressService.Progress.Records;

            if (records.ContainsKey(levelId) == false || records[levelId] < score)
                records[levelId] = score;

            _progressService.SaveProgress();
        }
    }
}
