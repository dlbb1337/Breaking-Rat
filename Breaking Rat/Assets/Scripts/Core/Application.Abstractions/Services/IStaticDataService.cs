using BreakingRat.Assets.Scripts.Core.Domain.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BreakingRat.Assets.Scripts.Core.Application.Abstractions.Services
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
