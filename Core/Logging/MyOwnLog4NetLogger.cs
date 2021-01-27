using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using log4net.Repository;

namespace WebGalery.Core.Logging
{
    public class MyOwnLog4NetLogger : ISimpleLogger
    {
        #region Construct
        static MyOwnLog4NetLogger()
        {
            try
            {
                ILoggerRepository repository = LogManager.GetRepository(Assembly.GetCallingAssembly());
                var fileInfo = new FileInfo(@"log4net.config");
                XmlConfigurator.Configure(repository, fileInfo);
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
        public MyOwnLog4NetLogger(Type type)
        {
            _log = LogManager.GetLogger(type);
        }

        private readonly ILog _log;
        readonly IDictionary<string, DateTime> _beginTimes = new Dictionary<string, DateTime>();
        #endregion

        public void Begin(string name)
        {
            Begin(name, "");
        }

        public void Begin(string name, string additional)
        {
            var beginTime = DateTime.Now;
            if (_beginTimes.ContainsKey(name))
            {
                Error($"WRONG USAGE OF BEGIN: {name} ALREADY EXISTS!");
                return;
            }
            _beginTimes[name] = beginTime;
            Debug($"{name} BEGIN \t\t {_beginTimes[name]} {additional}");
        }

        public void End(string name)
        {
            End(name, "");
        }

        public void End(string name, string additional)
        {
            var endTime = DateTime.Now;
            if (!_beginTimes.ContainsKey(name))
            {
                Error($"WRONG USAGE OF END: NO BEGIN FOR {name}");
                return;
            }
            var beginTime = _beginTimes[name];
            _beginTimes.Remove(name);
            Debug($"{name} END \t\t {endTime} \t\t DURATION: {endTime - beginTime} {additional}");
        }

        public void Debug(FormattableString formattableMessage)
        {
            if (_log.IsDebugEnabled)
                DoLog(formattableMessage, _log.Debug);
        }

        public void Debug(string message)
        {
            DoLog(message, _log.Debug);
        }

        public void Debug(Exception ex)
        {
            if (_log.IsDebugEnabled)
                DoLog(ex, _log.Debug);
        }

        public void Debug(object commonObject)
        {
            if (_log.IsDebugEnabled)
                DoLog(commonObject, _log.Debug);
        }

        public void Error(FormattableString formattableMessage)
        {
            if (_log.IsErrorEnabled)
                DoLog(formattableMessage, _log.Error);
        }

        public void Error(string message)
        {
            DoLog(message, _log.Error);
        }

        public void Error(Exception ex)
        {
            if (_log.IsErrorEnabled)
                DoLog(ex, _log.Error);
        }

        public void Error(object commonObject)
        {
            if (_log.IsErrorEnabled)
                DoLog(commonObject, _log.Error);
        }

        public void Info(FormattableString formattableMessage)
        {
            if (_log.IsInfoEnabled)
                DoLog(formattableMessage, _log.Info);
        }

        public void Info(string message)
        {
            if (_log.IsInfoEnabled)
                DoLog(message, _log.Info);
        }

        public void Info(Exception ex)
        {
            if (_log.IsInfoEnabled)
                DoLog(ex, _log.Info);
        }

        public void Info(object commonObject)
        {
            if (_log.IsInfoEnabled)
                DoLog(commonObject, _log.Info);
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
            string toLog = $"EXCEPTION: {ex}";
            logAction(toLog);
        }
        private void DoLog(object commonObject, Action<string> logAction)
        {
            logAction(commonObject.ToString());
        }
        #endregion
    }
}
