using Serilog;
using Serilog.Events;
using ISeriLogger = Serilog.ILogger;
namespace TheST.App
{
    public sealed class SeriLogger : ILogger
    {
        private readonly ISeriLogger _logger;
        public SeriLogger()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TheSt");
            if(Directory.Exists(folder) == false)
            {
                Directory.CreateDirectory(folder);
            }
            var filePath = Path.Combine(folder, "logs", "TheST-.txt");
            _logger = new LoggerConfiguration()
                .WriteTo.File(
                    filePath,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.ffffff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Debug
                ).CreateLogger();
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Debug(string message, Exception exception)
        {
            _logger.Debug(exception, message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            _logger.Error(exception, message);
        }

        public void Info(string message)
        {
            _logger.Information(message);
        }

        public void Info(string message, Exception exception)
        {
            _logger.Information(exception, message);
        }

        public void Warn(string message)
        {
            _logger.Warning(message);
        }

        public void Warn(string message, Exception exception)
        {
            _logger.Warning(exception, message);
        }
    }
}
