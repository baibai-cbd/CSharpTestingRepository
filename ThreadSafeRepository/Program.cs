using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThreadSafeRepository.Model;
using ThreadSafeRepository.Repository;

namespace ThreadSafeRepository
{
    class Program
    {

        static void Main(string[] args)
        {
            // Random Generator
            var random = new Random();

            while (true)
            {
                var i = Console.ReadLine();

                switch (i)
                {
                    #region MultithreadTests
                    case "1":
                        Console.WriteLine("try insert with 2 repos, 2 contexts, 1 connection setting");
                        TryTwoReposWithSameConn();
                        break;

                    case "2":
                        Console.WriteLine("try insert with 2 repos, 1 contexts, 1 connection setting");
                        TryTwoRepoWithSameContext();
                        break;

                    case "4":
                        Console.WriteLine("try insert with 2 repos, 2 contexts, 2 connection setting");
                        Console.WriteLine("point is to check time created time and delayed time in DB");
                        
                        break;

                    case "3":
                        Console.WriteLine("try multi-thread with 2 repos, 2 contexts, 2 connection setting");
                        ThreadedRunningMethods.ThreadedTwoRepoTwoContextTwoConn();
                        break;

                    case "5":
                        Console.WriteLine("try multi-thread with 2 repos, 2 contexts, 1 connection setting");
                        ThreadedRunningMethods.ThreadedTwoRepoTwoContextOneConn();
                        break;
                    #endregion
                    //
                    // doing different stuff here, trying disposing objects
                    //
                    case "d":
                        Console.WriteLine("try dispose a message entered here, please enter message");
                        string input = Console.ReadLine();
                        using (var m = new IncomingMessage(input))
                        {
                        MessageProcessingMethods.DisplayAndDisposeMessage(m);
                        }
                        break;

                    case "yieldtest":
                        MiscTestingMethods.ComparePerformanceYield();
                        break;

                    case "nullconditioning":
                        IncomingMessage mm = null;
                        string a = "sdsd";
                        if (a == mm?.message)
                        {
                            Console.WriteLine("can use as this");
                        } else
                        {
                            Console.WriteLine("not equal");
                        }
                        break;


                    #region FluentAPI
                    // Fluent api testing was not successful
                    case "fluentapi1":
                        var model2Context1 = new Model2();
                        var model2Repo1 = new Model2Repo(model2Context1);
                        var xrefB = model2Repo1.CreateABCPair("white", "coool", "10732", 23059);
                        break;

                    // Simply persist a entity here
                    case "fluentapi2":
                        var model2Context2 = new Model2();
                        var model2Repo2 = new Model2Repo(model2Context2);
                        bool tempBool = random.Next(2) == 0 ? true : false;
                        double tempNum = random.NextDouble();
                        var smallEntityD = model2Repo2.CreateSmallEntityD(tempBool, $"SomeNumber:{tempNum}");
                        Console.WriteLine(smallEntityD);
                        break;
                    #endregion

                    // Try delete directly with entities vs entities -> Ids -> entities
                    // Result: not big difference between the 2 ways
                    case "deletesInRepo":
                        int entityCount = 500;
                        var model2Context3 = new Model2();
                        var model2Repo3 = new Model2Repo(model2Context3);
                        // clear entities
                        model2Repo3.RemoveAllSmallEntityDs();
                        // random generate 100 entities
                        for (int j = 0; j < entityCount; j++)
                        {
                            model2Repo3.CreateSmallEntityD(random.Next(2) == 0 ? true : false, $"SomeNumber:{random.NextDouble()}");
                        }
                        var stopwatch = new Stopwatch();
                        stopwatch.Start();
                        var entities = model2Repo3.GetSmallEntityDsByBool(true);
                        var count = model2Repo3.RemoveSmallEntityDsByEntities(entities);
                        stopwatch.Stop();
                        Console.WriteLine($"time elapsed for 'removeByEntities' is {stopwatch.Elapsed}");
                        //
                        stopwatch.Reset();
                        //
                        // clear entities
                        model2Repo3.RemoveAllSmallEntityDs();
                        // random generate 100 entities
                        for (int j = 0; j < entityCount; j++)
                        {
                            model2Repo3.CreateSmallEntityD(random.Next(2) == 0 ? true : false, $"SomeNumber:{random.NextDouble()}");
                        }
                        stopwatch.Start();
                        var ids = model2Repo3.GetSmallEntityDIdsByBool(true);
                        var count1 = model2Repo3.RemoveSmallEntityDsByIds(ids);
                        stopwatch.Stop();
                        Console.WriteLine($"time elapsed for 'removeByIds' is {stopwatch.Elapsed}");

                        break;
                    //
                    //
                    default:
                        Console.WriteLine("DO NOTHING......");
                        break;
                }
            }
        }

        static void TryTwoReposWithSameConn()
        {
            // no error
            EntityConnection conn = new EntityConnection("metadata=res://*/Model.Model1.csdl|res://*/Model.Model1.ssdl|res://*/Model.Model1.msl;provider=System.Data.SqlClient;provider connection string=';data source=DESKTOP-RYZEN\\SQLEXPRESS;initial catalog=LocalThreadSafe;integrated security=True;max pool size=1;MultipleActiveResultSets=True;App=EntityFramework';");

            var context1 = new LocalThreadSafeEntities(conn, false);
            var context2 = new LocalThreadSafeEntities(conn, false);
            var repo1 = new UnsafeRepository(context1);
            var repo2 = new UnsafeRepository(context2);
            Console.WriteLine("context1 connection: " + context1.Database.Connection.GetHashCode());
            Console.WriteLine("context1 connection: " + context2.Database.Connection.GetHashCode());
            //
            repo1.CreateUsingSP(23, 3453, 11);
            repo2.CreateUsingSP(9, 776, 22);
        }

        static void TryTwoRepoWithSameContext()
        {
            // no error
            var context = new LocalThreadSafeEntities();
            var repo1 = new UnsafeRepository(context);
            var repo2 = new UnsafeRepository(context);
            //
            repo1.CreateUsingSP(89, 8674, 33);
            repo2.CreateUsingSP(3, 299, 44);
        }
    }
}
