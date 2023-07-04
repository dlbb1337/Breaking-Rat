using BreakingRat.Assets.Scripts.Core.Application.Abstractions.Services;
using BreakingRat.Assets.Scripts.Core.Domain.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BreakingRat.Assets.Scripts.Infrastructure.Persistence.Services
{
    public class StaticDataService : IStaticDataService
    {
        private const string _levelsPath = "StaticData/Levels";
        private List<LevelStaticData> _levelStaticDatas = new();

        private int _levelId = 1;

        public bool Initialized { get; } = false;

        public int LevelId { get => _levelId; private set { _levelId = value; } }

        public LevelStaticData CurrentLevelStaticData => _levelStaticDatas.FirstOrDefault(x => x.LevelId == LevelId);

        public List<LevelStaticData> LevelStaticDatas => _levelStaticDatas;

        public async Task InitializeAsync()
        {
            var locationsHandle = Addressables.LoadResourceLocationsAsync
                ("LevelData", typeof(LevelStaticData));

            await locationsHandle.Task;

            var dataHandle = Addressables.LoadAssetsAsync<LevelStaticData>(locationsHandle.Result, d => { });

            await dataHandle.Task;

            Addressables.Release(locationsHandle);

            _levelStaticDatas = dataHandle.Result.ToList();
        }

        public void SetLevelId(int id)
        {
            LevelId = id;
        }
    }
}
