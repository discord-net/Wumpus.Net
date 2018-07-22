using System;

namespace Voltaic.Logging
{
    public class NullLogger : ILogger
    {
        public string Name { get; }

        public NullLogger(string name = "Null")
        {
            Name = name;
        }

        public void Log(LogSeverity severity, Exception exception = null) { }
        public void Log(LogSeverity severity, string message, Exception exception = null) { }
        public void Log(LogSeverity severity, FormattableString message, Exception exception = null) { }

        public void Critical(Exception exception) { }
        public void Critical(string message, Exception exception = null) { }
        public void Critical(FormattableString message, Exception exception = null) { }

        public void Error(Exception exception) { }
        public void Error(string message, Exception exception = null) { }
        public void Error(FormattableString message, Exception exception = null) { }

        public void Warning(Exception exception) { }
        public void Warning(string message, Exception exception = null) { }
        public void Warning(FormattableString message, Exception exception = null) { }

        public void Info(Exception exception) { }
        public void Info(string message, Exception exception = null) { }
        public void Info(FormattableString message, Exception exception = null) { }

        public void Verbose(Exception exception) { }
        public void Verbose(string message, Exception exception = null) { }
        public void Verbose(FormattableString message, Exception exception = null) { }

        public void Debug(Exception exception) { }
        public void Debug(string message, Exception exception = null) { }
        public void Debug(FormattableString message, Exception exception = null) { }
    }
}
