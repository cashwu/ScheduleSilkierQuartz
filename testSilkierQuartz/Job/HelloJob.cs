using System;
using System.Threading.Tasks;
using Quartz;

namespace testSilkierQuartz.Job
{
    public class HelloJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hello");

            return Task.CompletedTask;
        }
    }
}