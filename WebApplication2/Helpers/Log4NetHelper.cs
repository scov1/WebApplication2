using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebApplication2.Helpers
{
    public class Log4NetHelper : ILogForHelper
    {
        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().FullName);

        public void Error(string message, Exception ex)
        {
            _log.Error(message, ex);
        }

        public void Warn(string message)
        {
            _log.Warn(message);
        }
    }

    //public static class Log4NetHelper
    //{
    //    private static ILog log = LogManager.GetLogger("Log4NetHelper");

    //    public static ILog Log
    //    {
    //        get { return log; }
    //    }

    //    public static void InitLogger()
    //    {
    //        XmlConfigurator.Configure();
    //    }
    //}
}