using System;

namespace Voltaic.Logging
{
    public class LogManager
    {
        public event Action<LogMessage> Output;

        public LogSeverity MinSeverity { get; }

        public LogManager(LogSeverity minSeverity)
        {
            MinSeverity = minSeverity;
        }

        public void Log(LogSeverity severity, string source, Exception ex)
        {
            try
            {
                if (severity <= MinSeverity)
                    Output?.Invoke(new LogMessage(severity, source, null, ex));
            }
            catch { }
        }
        public void Log(LogSeverity severity, string source, string message, Exception ex = null)
        {
            try
            {
                if (severity <= MinSeverity)
                    Output.Invoke(new LogMessage(severity, source, message, ex));
            }
            catch { }
        }
        public void Log(LogSeverity severity, string source, FormattableString message, Exception ex = null)
        {
            try
            {
                if (severity <= MinSeverity)
                    Output.Invoke(new LogMessage(severity, source, message.ToString(), ex));
            }
            catch { }
        }

        public void Critical(string source, Exception ex)
            => Log(LogSeverity.Critical, source, ex);
        public void Critical(string source, string message, Exception ex = null)
            => Log(LogSeverity.Critical, source, message, ex);
        public void Critical(string source, FormattableString message, Exception ex = null)
            => Log(LogSeverity.Critical, source, message, ex);

        public void Error(string source, Exception ex)
            => Log(LogSeverity.Error, source, ex);
        public void Error(string source, string message, Exception ex = null)
            => Log(LogSeverity.Error, source, message, ex);
        public void Error(string source, FormattableString message, Exception ex = null)
            => Log(LogSeverity.Error, source, message, ex);

        public void Warning(string source, Exception ex)
            => Log(LogSeverity.Warning, source, ex);
        public void Warning(string source, string message, Exception ex = null)
            => Log(LogSeverity.Warning, source, message, ex);
        public void Warning(string source, FormattableString message, Exception ex = null)
            => Log(LogSeverity.Warning, source, message, ex);

        public void Info(string source, Exception ex)
            => Log(LogSeverity.Info, source, ex);
        public void Info(string source, string message, Exception ex = null)
            => Log(LogSeverity.Info, source, message, ex);
        public void Info(string source, FormattableString message, Exception ex = null)
            => Log(LogSeverity.Info, source, message, ex);

        public void Verbose(string source, Exception ex)
            => Log(LogSeverity.Verbose, source, ex);
        public void Verbose(string source, string message, Exception ex = null)
            => Log(LogSeverity.Verbose, source, message, ex);
        public void Verbose(string source, FormattableString message, Exception ex = null)
            => Log(LogSeverity.Verbose, source, message, ex);

        public void Debug(string source, Exception ex)
            => Log(LogSeverity.Debug, source, ex);
        public void Debug(string source, string message, Exception ex = null)
            => Log(LogSeverity.Debug, source, message, ex);
        public void Debug(string source, FormattableString message, Exception ex = null)
            => Log(LogSeverity.Debug, source, message, ex);

        public ILogger CreateLogger(string name) => new Logger(this, name);
    }
}
