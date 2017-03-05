using UnityEngine;

namespace Logger
{
    class UnityLogger : ILoggerService
    {
        public override void Log(Level level, string message)
        {
            switch (level)
            {
                case Level.Error:
                    Debug.LogError(message);
                    break;
                case Level.Warning:
                    Debug.LogWarning(message);
                    break;
                case Level.Info:
                case Level.Config:
                case Level.Fine:
                    Debug.Log(message);
                    break;
            }
        }
        public override void Log(Level level, string format, params object[] args)
        {
            switch (level)
            {
                case Level.Error:
                    Debug.LogErrorFormat(format, args);
                    break;
                case Level.Warning:
                    Debug.LogWarningFormat(format, args);
                    break;
                case Level.Info:
                case Level.Config:
                case Level.Fine:
                    Debug.LogFormat(format, args);
                    break;
            }
        }
    }
}