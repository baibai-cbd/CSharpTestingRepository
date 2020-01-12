using System;
using System.Threading;
using ThreadSafeRepository.TestingDataModels;

namespace ThreadSafeRepository.TestingMethods
{
    class MessageProcessingMethods
    {
        public static void DisplayAndDisposeMessage(IncomingMessage message)
        {
            int a = (new Random()).Next(500, 1000);
            Console.WriteLine($"will sleep for {a} ms...");
            Thread.Sleep(a);
            Console.WriteLine(message.message);
            Console.WriteLine($"will sleep for {a} ms again...");
            Thread.Sleep(a);
        }
    }
}
