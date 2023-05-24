using System.Collections.Generic;

namespace BreakingRat.Data.Services
{
    public interface IStaticDataService
    {
        public int LevelId { get; }
        public void SetLevelId(int id);
        public LevelStaticData CurrentLevelStaticData { get; }
        public List<LevelStaticData> LevelStaticDatas { get; }
    }
}
