using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BreakingRat.Data.Services
{
    public class StaticDataService : IStaticDataService
    {
        private const string _levelsPath = "StaticData/Levels";
        private readonly List<LevelStaticData> _levelStaticDatas = new();

        private int _levelId = 1;

        public int LevelId { get => _levelId; private set { _levelId = value;  } }

        public LevelStaticData CurrentLevelStaticData => _levelStaticDatas.FirstOrDefault(x => x.LevelId == LevelId);

        public List<LevelStaticData> LevelStaticDatas => _levelStaticDatas;

        public StaticDataService()
        {
            _levelStaticDatas = Resources.LoadAll<LevelStaticData>(_levelsPath).ToList();
        }

        public void SetLevelId(int id)
        {
            LevelId = id;
        }
    }
}
