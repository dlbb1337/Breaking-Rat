using BreakingRat.Assets.Scripts.Core.Application.Abstractions.Services;

namespace BreakingRat.Assets.Scripts.Core.Application.Services
{
    public class DeathService : IDeathService
    {
        private readonly IPauseService _pauseService;
        private readonly IRecordService _recordService;
        private readonly IFactory _factory;
        private readonly IAdsService _adsService;

        public DeathService(
            IPauseService pauseService,
            IRecordService recordService,
            IFactory factory,
            IAdsService adsService)
        {
            _pauseService = pauseService;
            _recordService = recordService;
            _factory = factory;
            _adsService = adsService;
        }

        public void Death()
        {
            _pauseService.PauseGame();
            _recordService.UpdateRecord();
            _factory.CreateDeathScreenAsync();
            _adsService.ShowAd();
        }
    }
}
