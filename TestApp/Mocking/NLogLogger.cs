namespace TestApp.Mocking
{

    public class NLogLogger : ILogger
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public void Error(string message)
        {
            Logger.Error(message);
        }

        public void Info(string message)
        {
            Logger.Info(message);
        }
    }





}