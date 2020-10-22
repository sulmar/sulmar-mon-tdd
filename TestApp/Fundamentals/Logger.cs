using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TestApp
{
    public class LogEventArgs : EventArgs
    {
        public LogEventArgs(DateTime logDate)
        {
            LogDate = logDate;
        }

        public DateTime LogDate { get; set; }
    }

    public class Logger
    {
        public string LastMessage { get; set; }

        public event EventHandler<LogEventArgs> MessageLogged;

        public void Log(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException();

            LastMessage = message;

            // Write the log to a storage
            // ...

            Thread.Sleep(TimeSpan.FromSeconds(0.5));

           MessageLogged?.Invoke(this, new LogEventArgs(DateTime.UtcNow));
        }
    }
}
