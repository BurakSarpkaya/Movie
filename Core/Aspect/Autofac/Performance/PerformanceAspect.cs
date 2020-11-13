using Castle.DynamicProxy;
using Core.Utility.Interceptors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.Aspect.Autofac.Performance
{
    public class PerformanceAspect:MethodInterception
    {
        private int _interval;

        public PerformanceAspect(int interval)
        {
            _interval = interval;
        }

        Stopwatch stopwatch = new Stopwatch();
        protected override void OnBefore(IInvocation invocation)
        {
            stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (stopwatch.Elapsed.TotalSeconds>_interval)
            {
                Debug.WriteLine($"Performance:{invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}--->{stopwatch.Elapsed.TotalSeconds}");
            }
            stopwatch.Reset();
        }
    }
}
