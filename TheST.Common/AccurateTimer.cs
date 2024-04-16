using System.Diagnostics;

namespace TheST.Common
{
    public sealed class AccurateTimer : IDisposable
    {
        private const float EscapeGap = 1.05f;

        private const byte SleepThresholdState1 = 10;

        private const byte SleepThresholdState2 = 20;

        private const byte SleepThresholdState3 = 40;

        private readonly float _intervalInMilliseconds;

        private readonly float TickLength = 1000f / Stopwatch.Frequency;

        private bool _isStarted;

        private bool _isThreadPreloaded;

        private float _paddingTime;

        private Stopwatch _stopwatch = new Stopwatch();

        private ThreadPriority _threadPriority = ThreadPriority.AboveNormal;

        public AccurateTimer(float intervalInMilliseconds)
        {
            _intervalInMilliseconds = intervalInMilliseconds;
        }

        public AccurateTimer(float intervalInMilliseconds, ThreadPriority preferredThreadPriority) : this(intervalInMilliseconds)
        {
            _threadPriority = preferredThreadPriority;
        }

        public event EventHandler? Elapsed;

        public long InvokeInterval { get; private set; }

        public bool IsRunning => _isStarted;

        public bool WaitForEvents { get; set; }

        private float CalculateElapsed => _stopwatch.ElapsedTicks * TickLength + _paddingTime;

        public void Dispose()
        {
            Stop();
            _isThreadPreloaded = false;
        }

        public void Reset()
        {
            if (_isStarted)
                _stopwatch.Restart();
            else
                _stopwatch.Reset();

            _paddingTime = 0;
        }

        public void Start()
        {
            if (!IsRunning)
            {
                _isStarted = true;
                _stopwatch.Restart();

                if (!_isThreadPreloaded)
                {
                    StartRunnerThread();
                }
            }
        }

        public void Stop()
        {
            _isStarted = false;
            _stopwatch.Stop();
        }

        private AccurateTimer Preload()
        {
            if (!_isThreadPreloaded)
            {
                _isThreadPreloaded = true;
                StartRunnerThread();
            }
            return this;
        }

        private void Run()
        {
            float actualDifference;
            float elapsed;

            while (_isThreadPreloaded || _isStarted)
            {
                if (!_isStarted && _isThreadPreloaded)
                {
                    Thread.Sleep(2);
                    continue;
                }

                elapsed = CalculateElapsed;
                actualDifference = _intervalInMilliseconds - elapsed;

                if (actualDifference <= 0)
                {
                    _paddingTime = elapsed - _intervalInMilliseconds;
                    InvokeInterval = _stopwatch.ElapsedMilliseconds;

                    if (_isStarted) _stopwatch.Restart();

                    try
                    {
                        Elapsed?.Invoke(this, EventArgs.Empty);

                        if (WaitForEvents && _isStarted) _stopwatch.Restart();
                    }
                    catch (Exception)
                    {
                        // Ignored
                    }
                }

                Spin(actualDifference - EscapeGap);
            }
        }

        private void Spin(float difference)
        {
            if (difference >= SleepThresholdState3)
            {
                Thread.Sleep(8);
            }
            else if (difference >= SleepThresholdState2)
            {
                Thread.Sleep(2);
            }
            else if (difference >= SleepThresholdState1)
            {
                Thread.Sleep(1);
            }

            Thread.SpinWait(8);
        }

        private void StartRunnerThread()
        {
            Thread runnerThread = new Thread(new ThreadStart(Run));
            runnerThread.IsBackground = true;
            runnerThread.Priority = _threadPriority;
            runnerThread.Start();
        }
    }
}