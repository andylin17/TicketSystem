using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem
{
    public class LogHelper
    {
        private Logger _nlogger;
        public LogHelper(string provider)
        {
            _nlogger = NLog.LogManager.GetLogger(provider);
        }

        public void Info(string action, string message, params object[] args)
        {
            try
            {
                string logMessage = message;
                if (args.Length > 0)
                {
                    logMessage = string.Format(message, args);
                }

                _nlogger.Info($"[{action}] | {logMessage}");
            }
            catch { }
        }
        public void Info(string action, long time, string message, params object[] args)
        {
            try
            {
                string logMessage = message;
                if (args.Length > 0)
                {
                    logMessage = string.Format(message, args);
                }

                _nlogger.Info($"[{action}] | [useTime]:{time} | {logMessage}");
            }
            catch { }
        }
        public void Debug(string message, params object[] args)
        {
            try
            {
                string logMessage = message;
                if (args.Length > 0)
                {
                    logMessage = string.Format(message, args);
                }
                _nlogger.Debug($"{logMessage}");
            }
            catch { }
        }
        public void Debug(long time, string message, params object[] args)
        {
            try
            {
                string logMessage = message;
                if (args.Length > 0)
                {
                    logMessage = string.Format(message, args);
                }
                _nlogger.Debug($"[useTime]:{time} | {logMessage}");
            }
            catch { }
        }
        public void Error(Exception ex, string action, string message, params object[] args)
        {
            try
            {
                string logMessage = message;
                if (args.Length > 0)
                {
                    logMessage = string.Format(message, args);
                }
                _nlogger.Error($@"[{action}] - {logMessage} 
                , StackTrace: {ex.StackTrace}");
            }
            catch { }
        }
        public void Trace(string action, long timeSpend, string message, params object[] args)
        {
            try
            {
                string logMessage = message;
                if (args.Length > 0)
                {
                    logMessage = string.Format(message, args);
                }
                _nlogger.Trace($"[{action}] | {logMessage} ,timespend = {timeSpend}");
            }
            catch { }
        }

    }
}
