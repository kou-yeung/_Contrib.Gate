using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class ConsoleLogger : ILoggerService
    {
        void Log(ConsoleColor color, string message)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = defaultColor;
        }
        void Log(ConsoleColor color, string format, params object[] args)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(format, args);
            Console.ForegroundColor = defaultColor;
        }
        public override void Log(Level level, string message)
        {
            switch (level)
            {
                case Level.Error:    // (最高値)
                    Log(ConsoleColor.Red, message);
                    break;
                case Level.Warning:  // (警告)
                    Log(ConsoleColor.Yellow, message);
                    break;
                case Level.Info:
                case Level.Config:
                case Level.Fine:
                    Log(ConsoleColor.White, message);
                    break;
            }
        }

        public override void Log(Level level, string format, params object[] args)
        {
            switch (level)
            {
                case Level.Error:    // (最高値)
                    Log(ConsoleColor.Red, format, args);
                    break;
                case Level.Warning:  // (警告)
                    Log(ConsoleColor.Yellow, format, args);
                    break;
                case Level.Info:
                case Level.Config:
                case Level.Fine:
                    Log(ConsoleColor.White, format, args);
                    break;
            }
        }
    }
}
