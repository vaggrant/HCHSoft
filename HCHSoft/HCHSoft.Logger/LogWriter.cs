/********************************************************************************
** Author: 
** Date:   2011-05-26
** LogWriter:
**          Logger format info to file
*********************************************************************************/

using System;
using System.IO;
using log4net;

namespace HCHSoft.Logger
{
    /// <summary>
    /// LogWrite class for Action logger 
    /// </summary>
    public class LogWriter
    {
        private readonly string _configFile = "HCHSoft.Log.Config";

        //Init ILog object
        private static readonly ILog _action = LogManager.GetLogger("ActionLog");

        //define private static a LogWriter instance
        private static LogWriter _instance = null;

        /// <summary>
        /// Read config file of log
        /// </summary>
        public LogWriter()
        {
            this.LoadLogConfig();
        }

        /// <summary>
        /// Load Confog File and Init
        /// </summary>
        private void LoadLogConfig()
        {
            var logConfigFileName = AppDomain.CurrentDomain.BaseDirectory + _configFile;
            FileInfo fInfo = new FileInfo(logConfigFileName);
            if (!fInfo.Exists)
            {
                throw new ApplicationException("Failed to load log configuration!");
            }
            else
            {
                try
                {
                    log4net.Config.XmlConfigurator.Configure(fInfo);
                }
                catch
                {
                    throw new ApplicationException("Failed to parse log configuration!");
                }
            }
        }

        /// <summary>
        /// Get LogWriter only one object
        /// </summary>
        public static LogWriter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LogWriter();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Get a defined logger
        /// </summary>
        public ILog ActionLogger
        {
            get { return _action; }
        }
    }
}
