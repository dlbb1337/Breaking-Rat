using BreakingRat.Assets.Scripts.Core.Domain.Data.Obstacles;
using System.Collections.Generic;
using UnityEngine;

namespace BreakingRat.Assets.Scripts.Core.Domain.Data
{
    [CreateAssetMenu(fileName = "LevelStaticData", menuName = "StaticData/LevelStaticData")]
    public class LevelStaticData : ScriptableObject
    {
        [SerializeField] private int _levelId;
        [SerializeField] private DeadzoneStaticData _deadzoneStaticData;
        [SerializeField] private PlayerStaticData _playerStaticData;
        [SerializeField] private MazesStaticData _mazesStaticData;
        [SerializeField] private List<ObstaclesStaticData> _obstacles;

        public int LevelId => _levelId;
        public MazesStaticData MazesStaticData => _mazesStaticData;
        public PlayerStaticData PlayerStaticData => _playerStaticData;
        public DeadzoneStaticData DeadzoneStaticData => _deadzoneStaticData;
        public List<ObstaclesStaticData> ObStacles => _obstacles;
    }
}
