using System.Collections.Generic;
using System.Threading.Tasks;

namespace BreakingRat.Data.Services
{
    public interface IStaticDataService
    {
        public int LevelId { get; }
        public void SetLevelId(int id);
        Task InitializeAsync();

        public LevelStaticData CurrentLevelStaticData { get; }
        public List<LevelStaticData> LevelStaticDatas { get; }
    }
}
