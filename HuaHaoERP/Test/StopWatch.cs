using System;
using System.Diagnostics;

namespace HuaHaoERP.Test
{
    class StopWatch
    {
        private static StopWatch _instance;
        private static readonly object _locker = new object();
        private Stopwatch _stopWatch;

        private StopWatch()
        {
            _stopWatch = new Stopwatch();
        }

        internal static StopWatch GetInstance()
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new StopWatch();
                    }
                }
            }
            return _instance;
        }

        internal void Start()
        {
            if (_instance == null)
            {
                return;
            }
            _stopWatch.Restart();
        }

        internal void Stop()
        {
            if (_instance == null)
            {
                return;
            }
            _stopWatch.Stop();
            Console.WriteLine("总运行时间：" + _stopWatch.Elapsed);
            Console.WriteLine("测量实例得出的总运行时间（毫秒为单位）：" + _stopWatch.ElapsedMilliseconds);
            Console.WriteLine("总运行时间(计时器刻度标识)：" + _stopWatch.ElapsedTicks);
        }

    }
}
