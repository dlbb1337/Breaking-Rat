namespace GameConsole.LogManager
{
    public class SimpleLogsHandler : ILogHandler
    {
        public Log HandleLog(Log log)
        {
            return log;
        }
    }
}
