using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Logger.Epicor;

namespace ArmProductionScheduler
{
   public class EpicorLogger
    {
        #region ****************************** Attributes ******************************
        public Guid LoggerId { get; private set; }
        public ISEpicorLogger EpicLogger { get; private set; }

        #endregion ****************************** Attributes ******************************

        #region ****************************** Constructor ******************************

        public EpicorLogger(EpicConnector.Connection.Environments epicEnv)
        {
            LoggerId = Guid.NewGuid();
            DatabaseConnections.EpicConnections externalDbConnection;

            switch (epicEnv)
            {
                case EpicConnector.Connection.Environments.None:
                    externalDbConnection = DatabaseConnections.EpicConnections.Test_External;
                    break;
                case EpicConnector.Connection.Environments.Dev:
                    externalDbConnection = DatabaseConnections.EpicConnections.Dev_External;
                    break;
                case EpicConnector.Connection.Environments.Test:
                    externalDbConnection = DatabaseConnections.EpicConnections.Test_External;
                    break;
                case EpicConnector.Connection.Environments.Live:
                    externalDbConnection = DatabaseConnections.EpicConnections.Live_External;
                    break;
                case EpicConnector.Connection.Environments.TJC:
                    externalDbConnection = DatabaseConnections.EpicConnections.TJC_External;
                    break;
                default:
                    externalDbConnection = DatabaseConnections.EpicConnections.Test_External;
                    break;
            }

            EpicLogger = EpicorLoggerFactory.CreateEpicorLogger(externalDbConnection, epicEnv != EpicConnector.Connection.Environments.Dev);
        }

        #endregion ****************************** Constructor ******************************

        #region ****************************** Methods ******************************

        public void AddLog(EventLogEntryType eventLogType, string xmlString, string logMessage, bool save = true, [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
        {
            EpicLogger.AddLog(new Logger.Epicor.Database.EpicorColumns(
                LoggerId,
                eventLogType,
                DateTime.Now,
                logMessage,
                Environment.UserName,
                System.Diagnostics.Process.GetCurrentProcess().ProcessName,
                EpicLogger.StringToXmlDocument(xmlString),
                methodName,
                lineNumber,
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()));

            if (save)
            {
                EpicLogger.Save();
            }
        }

        #endregion ****************************** Methods ******************************
    }
}
