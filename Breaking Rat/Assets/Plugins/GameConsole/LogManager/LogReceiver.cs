using System;
using UnityEngine;

namespace GameConsole.LogManager
{

    public class LogReceiver
    {
        public event Action<Log> MessageReceived;

        public LogReceiver()
        {
            Application.logMessageReceivedThreaded += Application_logMessageReceivedThreaded;
        }

        ~LogReceiver()
        {
            Application.logMessageReceivedThreaded -= Application_logMessageReceivedThreaded;
        }

        private void Application_logMessageReceivedThreaded(string condition, string stackTrace, LogType type)
        {
            Log log = new Log(condition, stackTrace, type);

            MessageReceived?.Invoke(log);
        }
    }

}