using BreakingRat.Data.Services;
using GameConsole.CommandTools;

namespace BreakingRat.Commands
{
    public class ProgressCommands : ICommandContainer
    {
        private readonly IProgressService _progressService;

        public ProgressCommands(IProgressService progressService)
        {
            _progressService = progressService;
        }
    }
}
