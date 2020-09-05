using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Domain.Logging
{
    public class Log4NetLogger : ISimpleLogger
    {
        #region Construct
        static Log4NetLogger()
        {
            try
            {
                ILoggerRepository repository = log4net.LogManager.GetRepository(Assembly.GetCallingAssembly());
                var fileInfo = new FileInfo(@"log4net.config");
                log4net.Config.XmlConfigurator.Configure(repository, fileInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// <code>System.Reflection.MethodBase.GetCurrentMethod().DeclaringType</code>
        /// </summary>
        /// <param name="loggerName"><code>System.Reflection.MethodBase.GetCurrentMethod().DeclaringType</code></param>
        public Log4NetLogger(Type type)
        {
            log = log4net.LogManager.GetLogger(type);
        }

        private readonly log4net.ILog log;
        IDictionary<string, DateTime> beginTimes = new Dictionary<string, DateTime>();
        #endregion

        public void Begin(string name)
        {
            var beginTime = DateTime.Now;
            if (beginTimes.ContainsKey(name))
            {
                Error($"WRONG USAGE OF BEGIN: {name} ALREADY EXISTS!");
                return;
            }
            beginTimes[name] = beginTime;
            Debug($"{name} BEGIN \t\t {beginTimes[name]}");
        }

        public void End(string name)
        {
            var endTime = DateTime.Now;
            if (!beginTimes.ContainsKey(name))
            {
                Error($"WRONG USAGE OF END: NO BEGIN FOR {name}");
                return;
            }
            var beginTime = beginTimes[name];
            beginTimes.Remove(name);
            Debug($"{name} END \t\t {endTime} \t\t DURATION: {endTime - beginTime}");
        }

        public void Debug(FormattableString formattableMessage)
        {
            if (log.IsDebugEnabled)
                DoLog(formattableMessage, log.Debug);
        }

        public void Debug(string message)
        {
            DoLog(message, log.Debug);
        }

        public void Debug(Exception ex)
        {
            if (log.IsDebugEnabled)
                DoLog(ex, log.Debug);
        }

        public void Debug(object commonObject)
        {
            if (log.IsDebugEnabled)
                DoLog(commonObject, log.Debug);
        }

        public void Error(FormattableString formattableMessage)
        {
            if (log.IsErrorEnabled)
                DoLog(formattableMessage, log.Error);
        }

        public void Error(string message)
        {
            DoLog(message, log.Error);
        }

        public void Error(Exception ex)
        {
            if (log.IsErrorEnabled)
                DoLog(ex, log.Error);
        }

        public void Error(object commonObject)
        {
            if (log.IsErrorEnabled)
                DoLog(commonObject, log.Error);
        }

        public void Info(FormattableString formattableMessage)
        {
            if (log.IsInfoEnabled)
                DoLog(formattableMessage, log.Info);
        }

        public void Info(string message)
        {
            if (log.IsInfoEnabled)
                DoLog(message, log.Info);
        }

        public void Info(Exception ex)
        {
            if (log.IsInfoEnabled)
                DoLog(ex, log.Info);
        }

        public void Info(object commonObject)
        {
            if (log.IsInfoEnabled)
                DoLog(commonObject, log.Info);
        }

        #region Private = the logging
        private void DoLog(string theMessage, Action<string> logAction)
        {
            logAction(theMessage);
        }
        private void DoLog(FormattableString formattableMessage, Action<string> logAction)
        {
            logAction(formattableMessage.ToString());
        }
        private void DoLog(Exception ex, Action<string> logAction)
        {
            string toLog = $"EXCEPTION: {ex.ToString()}";
            logAction(toLog);
        }
        private void DoLog(object commonObject, Action<string> logAction)
        {
            logAction(commonObject.ToString());
        }
        #endregion
    }
}
