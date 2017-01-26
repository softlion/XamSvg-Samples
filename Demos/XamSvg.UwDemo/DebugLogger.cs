using System;
using System.Diagnostics;
using XamSvg.Shared;

namespace XamSvg.UwDemo
{
    class DebugLogger : ILogger
    {
        public bool TraceEnabled { get; set; } = true;

        public void Trace(Func<string> s, string method = null, int lineNumber = 0)
        {
            Debug.WriteLine($"{method}@{lineNumber}: {s()}");
        }
    }
}