namespace TheST
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message);
        void Warn(string message);
        void Debug(string message);
        void Info(string message, Exception exception);
        void Error(string message, Exception exception);
        void Warn(string message, Exception exception);
        void Debug(string message, Exception exception);

    }
}
