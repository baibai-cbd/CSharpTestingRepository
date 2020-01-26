using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSafeRepository.TestingMethods
{
    class LinqExecutionTestingMethods
    {
        public static void WhereSelectExecution()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            // demo with where to get even number, select to get number squared
            var results = list.Where(i =>
            {
                Console.WriteLine($"Where-ing..{i}");
                return i % 2 == 0;
            })
            .Select(i =>
            {
                Console.WriteLine($"Selecting..{i}");
                return i * i;
            });

            foreach (var item in results)
            {
                Console.WriteLine($"one result is... {item}");
            }
        }

        public static void WhereSelectTakeExecution()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            // demo with where to get even number, select to get number squared
            var results = list.Where(i =>
            {
                Console.WriteLine($"Where-ing..{i}");
                return i % 2 == 0;
            })
            .Select(i =>
            {
                Console.WriteLine($"Selecting..{i}");
                return i * i;
            })
            .Take(2);

            foreach (var item in results)
            {
                Console.WriteLine($"one result is... {item}");
            }
        }


        public static void TutorialMethod()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

            var results = list.SelectMany(i => 
            {
                Console.WriteLine($"{i} in where");
                return new List<int>() { i, i * 2, i * 3 };
            });

            foreach (var item in results)
            {
                Console.WriteLine($"one result is... {item}");
            }
        }
    }
}
