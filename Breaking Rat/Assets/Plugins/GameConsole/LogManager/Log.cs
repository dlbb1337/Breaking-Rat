using UnityEngine;

namespace GameConsole.LogManager
{

    public struct Log
    {
        private string _message;
        private string _stackTrace;
        private LogType _logType;

        public Log(string message, string stackTrace, LogType logType) : this()
        {
            _message = message;
            _stackTrace = stackTrace;
            _logType = logType;
        }

        public string Message => _message;

        public string StackTrace => _stackTrace;

        public LogType LogType => _logType;

        public override string ToString() => Message + "\n";

        public string ToString(bool withStackTrace)
        {
            if (withStackTrace)
                return ToString() + StackTrace;
            else
                return ToString();
        }
    }

}