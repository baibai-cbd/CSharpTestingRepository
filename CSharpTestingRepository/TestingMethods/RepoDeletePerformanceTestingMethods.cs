using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadSafeRepository.Model;
using ThreadSafeRepository.Repository;

namespace ThreadSafeRepository.TestingMethods
{
    class RepoDeletePerformanceTestingMethods
    {
        public static void RemoveByEntities(int entityCount)
        {
            var random = new Random();
            var model2Context = new Model2();
            var model2Repo = new Model2Repo(model2Context);
            // clear entities
            model2Repo.RemoveAllSmallEntityDs();
            // random generate entities
            for (int j = 0; j < entityCount; j++)
            {
                model2Repo.CreateSmallEntityD(random.Next(2) == 0 ? true : false, $"SomeNumber:{random.NextDouble()}");
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var entities = model2Repo.GetSmallEntityDsByBool(true);
            var count = model2Repo.RemoveSmallEntityDsByEntities(entities);
            stopwatch.Stop();
            Console.WriteLine($"time elapsed for 'removeByEntities' is {stopwatch.Elapsed}");
        }

        public static void RemoveByIds(int entityCount)
        {
            var random = new Random();
            var model2Context = new Model2();
            var model2Repo = new Model2Repo(model2Context);
            // clear entities
            model2Repo.RemoveAllSmallEntityDs();
            // random generate entities
            for (int j = 0; j < entityCount; j++)
            {
                model2Repo.CreateSmallEntityD(random.Next(2) == 0 ? true : false, $"SomeNumber:{random.NextDouble()}");
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var ids = model2Repo.GetSmallEntityDIdsByBool(true);
            var count1 = model2Repo.RemoveSmallEntityDsByIds(ids);
            stopwatch.Stop();
            Console.WriteLine($"time elapsed for 'removeByIds' is {stopwatch.Elapsed}");
        }
    }
}
