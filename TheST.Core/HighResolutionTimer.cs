using System.Diagnostics;

namespace TheST
{
    public sealed class HighResolutionTimer : IDisposable
    {
        private const float ESCAPE_GAP = 1.05f;
        private const byte SLEEP_THRESH_STATE_1 = 10;
        private const byte SLEEP_THRESH_STATE_2 = 20;
        private const byte SLEEP_THRESH_STATE_3 = 40;
        private static readonly float TickLength = 1000f / Stopwatch.Frequency;
        private readonly float _msInterval = 0;
        private float _paddingTime = 0;
        private bool _preloadThread = false;
        private ThreadPriority _priority = ThreadPriority.AboveNormal;
        private bool _started = false;
        private Stopwatch _watcher = new Stopwatch();
        public long InvokeInterval { get; private set; }

        /// <summary>
        /// Return true if the timer is running
        /// </summary>
        public bool IsRunning => _started;

        public bool WaitForEvents { get; set; }

        private float CalculateElapsed => _watcher.ElapsedTicks * TickLength + _paddingTime;

        public HighResolutionTimer(float msInterval)
        {
            _msInterval = msInterval;
        }

        public HighResolutionTimer(float interval, ThreadPriority preferPriority) : this(interval)
        {
            _priority = preferPriority;
        }

        public event EventHandler? Elapsed;

        public void Dispose()
        {
            Stop();
            _preloadThread = false;
        }

        public void Reset()
        {
            if (_started)
                _watcher.Restart();
            else
                _watcher.Reset();
            _paddingTime = 0;
        }

        public void Start()
        {
            if (!IsRunning)
            {
                _started = true;
                _watcher.Restart();
                if (!_preloadThread)
                {
                    ExecuteRunnerThread();
                }
            }
        }

        public void Stop()
        {
            _started = false;
            _watcher.Stop();
        }

        private void ExecuteRunnerThread()
        {
            Thread runner = new Thread(new ThreadStart(this.runner));
            runner.IsBackground = true;
            runner.Priority = _priority;
            runner.Start();
        }

        private HighResolutionTimer Preload()
        {
            if (!_preloadThread)
            {
                _preloadThread = true;
                ExecuteRunnerThread();
            }
            return this;
        }

        private void runner()
        {
            float actualDiff = 0;
            float elapsed = 0;
            while (_preloadThread || _started)
            {
                if (!_started && _preloadThread)
                {
                    Thread.Sleep(2);
                    continue;
                }
                elapsed = CalculateElapsed;
                actualDiff = _msInterval - elapsed;
                if (actualDiff <= 0)
                {
                    _paddingTime = elapsed - _msInterval;
                    InvokeInterval = _watcher.ElapsedMilliseconds;
                    // If not ended, then restart the watcher, this boolean to ensure Stop call from another thread still clean the watcher counter.
                    if (_started) _watcher.Restart();
                    try
                    {
                        Elapsed?.Invoke(this, EventArgs.Empty);
                        if (WaitForEvents && _started) _watcher.Restart();
                    }
                    catch (Exception)
                    {
                        // Ignored
                    }
                }
                Spin(actualDiff - ESCAPE_GAP);
            }
        }

        private void Spin(float diff)
        {
            // Thread.Sleep provide a better performance than SpinUntil in a long-term wait
            if (diff >= SLEEP_THRESH_STATE_3)
            {
                Thread.Sleep(8);
            }
            else if (diff >= SLEEP_THRESH_STATE_2)
            {
                Thread.Sleep(2);
            }
            else if (diff >= SLEEP_THRESH_STATE_1)
            {
                Thread.Sleep(1);
            }
            Thread.SpinWait(8);
        }
    }
}