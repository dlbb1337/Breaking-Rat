using BreakingRat.GameLogic.Services;
using BreakingRat.Infrastructure.Factory;

namespace BreakingRat.GameLogic.DeathLogic.Services
{
    public class DeathService : IDeathService
    {
        private readonly IPauseService _pauseService;
        private readonly IRecordService _recordService;
        private readonly IFactory _factory;

        public DeathService(IPauseService pauseService, IRecordService recordService, IFactory factory)
        {
            _pauseService = pauseService;
            _recordService = recordService;
            _factory = factory;
        }

        public void Death()
        {
            _pauseService.PauseGame();
            _recordService.UpdateRecord();
            _factory.CreateDeathScreen();
        }
    }
}
