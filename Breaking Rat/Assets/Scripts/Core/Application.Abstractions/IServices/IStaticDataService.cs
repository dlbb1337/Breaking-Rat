using BreakingRat.Domain.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BreakingRat.Application.Abstractions.IServices
{
    public interface IStaticDataService
    {
        public int LevelId { get; }
        public bool Initialized { get; }
        public void SetLevelId(int id);
        Task InitializeAsync();

        public LevelStaticData CurrentLevelStaticData { get; }
        public List<LevelStaticData> LevelStaticDatas { get; }
    }
}
