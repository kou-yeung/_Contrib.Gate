using System;
using System.Collections;
using System.Collections.Generic;

namespace Logger
{
    enum Level : int
    {
        // ログレベル
        Error = 1000,   // (最高値)
        Warning = 900,  // (警告)
        Info = 800,
        Config = 700,
        Fine = 600,

        // 出力制御
        All = int.MinValue, // すべてログ出力
        Off = int.MaxValue, // すべてログオフ
    }

    abstract class ILoggerService
    {
        Level level = Level.All;

        public Level GetLevel()
        {
            return this.level;
        }
        public void SetLevel(Level level)
        {
            this.level = level;
        }

        public abstract void Log(Level level, string message);
        public abstract void Log(Level level, string format, params object[] args);

        public bool IsLoggable(Level level)
        {
            return level >= this.level; 
        }
        // Level.Error
        public void Error(string message)
        {
            if (!IsLoggable(Level.Error)) return;
            Log(Level.Error, message);
        }
        public void Error(string format, params object[] args)
        {
            if (!IsLoggable(Level.Error)) return;
            Log(Level.Error, format, args);
        }
        // Level.Warning
        public void Warning(string message)
        {
            if (!IsLoggable(Level.Warning)) return;
            Log(Level.Warning, message);
        }
        public void Warning(string format, params object[] args)
        {
            if (!IsLoggable(Level.Warning)) return;
            Log(Level.Warning, format, args);
        }
        // Level.Info
        public void Info(string message)
        {
            if (!IsLoggable(Level.Info)) return;
            Log(Level.Info, message);
        }
        public void Info(string format, params object[] args)
        {
            if (!IsLoggable(Level.Info)) return;
            Log(Level.Info, format, args);
        }
        // Level.Config
        public void Config(string message)
        {
            if (!IsLoggable(Level.Config)) return;
            Log(Level.Config, message);
        }
        public void Config(string format, params object[] args)
        {
            if (!IsLoggable(Level.Config)) return;
            Log(Level.Config, format, args);
        }
        // Level.Fine
        public void Fine(string message)
        {
            if (!IsLoggable(Level.Fine)) return;
            Log(Level.Fine, message);
        }
        public void Fine(string format, params object[] args)
        {
            if (!IsLoggable(Level.Fine)) return;
            Log(Level.Fine, format, args);
        }
    }
    class LoggerService : ServiceLocator<ILoggerService>
    {
    }
}
