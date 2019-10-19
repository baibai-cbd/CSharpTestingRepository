using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadSafeRepository.TestingDataModels;

namespace ThreadSafeRepository.TestingMethods
{
    class YieldPerformanceTestingMethods
    {
        public static void ComparePerformanceYield()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            GenerateMessagesWithYield();
            stopwatch.Stop();
            Console.WriteLine($"time elapsed for 'yield' is {stopwatch.Elapsed}");
            //
            stopwatch.Reset();
            //
            stopwatch.Start();
            GenerateMessages();
            stopwatch.Stop();
            Console.WriteLine($"time elapsed for no 'yield' is {stopwatch.Elapsed}");
        }

        private static IEnumerable<IncomingMessage> GenerateMessagesWithYield()
        {
            for (int i = 0; i < 5000; i++)
            {
                yield return new IncomingMessage($"new message number: {i}");
            }
        }

        private static IEnumerable<IncomingMessage> GenerateMessages()
        {
            List<IncomingMessage> messages = new List<IncomingMessage>(10000);
            for (int i = 0; i < 5000; i++)
            {
                messages.Add(new IncomingMessage($"new message number: {i}"));
            }
            return messages;
        }
    }
}
