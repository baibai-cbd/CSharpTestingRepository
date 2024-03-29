﻿using BenchmarkDotNet.Running;
using System;
using ThreadSafeRepository.DapperRepository;
using ThreadSafeRepository.Model;
using ThreadSafeRepository.Repository;
using ThreadSafeRepository.TestingDataModels;
using ThreadSafeRepository.TestingMethods;

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
                        ThreadedRunningMethods.TryTwoReposWithSameConn();
                        break;

                    case "2":
                        Console.WriteLine("try insert with 2 repos, 1 contexts, 1 connection setting");
                        ThreadedRunningMethods.TryTwoRepoWithSameContext();
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

                    #region DisposeObjects
                    // Testing different stuff here, trying disposing objects
                    case "d":
                        Console.WriteLine("try dispose a message entered here, please enter message");
                        string input = Console.ReadLine();
                        using (var m = new IncomingMessage(input))
                        {
                        MessageProcessingMethods.DisplayAndDisposeMessage(m);
                        }
                        break;
                    #endregion

                    #region YieldTest
                    // Result: yield return is faster than the common adding to list, then return pattern
                    case "yieldtest":
                        YieldPerformanceTestingMethods.ComparePerformanceYield();
                        break;
                    #endregion

                    #region NullConditioning
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
                    #endregion

                    #region FluentAPI
                    // TODO: Fluent api testing was not successful
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

                    #region DeletesPerformanceInRepo
                    // Try delete directly with entities vs entities -> Ids -> entities
                    // Result: not big difference between the 2 ways
                    case "deletesInRepo":
                        RepoDeletePerformanceTestingMethods.RemoveByEntities(1000);
                        RepoDeletePerformanceTestingMethods.RemoveByIds(1000);
                        break;
                    #endregion

                    #region DapperMultiMapping
                    // Two layers of Multi mapping need 2 dictionaries to store data
                    // And add reference to dictionary and upper layer only when it's not found in the dictionary
                    case "dapperMulti":
                        var dapperRepo = new EmployeeRepository("Model2");
                        var employee = dapperRepo.GetEmployeeWithAllData(1);
                        Console.WriteLine($"This employee {employee.Name} has {employee.Projects.Count} of projects records...");
                        break;
                    #endregion

                    #region LinqExecutionOrder
                    case "LinqExe1":
                        LinqExecutionTestingMethods.WhereSelectExecution();
                        break;

                    case "LinqExe2":
                        LinqExecutionTestingMethods.WhereSelectTakeExecution();
                        break;

                    case "Tutorial":
                        LinqExecutionTestingMethods.TutorialMethod();
                        break;
                    #endregion

                    #region LazyLoadingDemo
                    case "LazyLoadingOn":
                        var guid1 = LazyLoadingTestingMethods.CreateDataForLazyLoadingTesting();
                        Console.WriteLine("-");
                        LazyLoadingTestingMethods.LoadDataWithDifferentLazyLoadingSetup(lazyLoading: true, siteID: guid1);
                        break;

                    case "LazyLoadingOff":
                        var guid2 = LazyLoadingTestingMethods.CreateDataForLazyLoadingTesting();
                        Console.WriteLine("-");
                        LazyLoadingTestingMethods.LoadDataWithDifferentLazyLoadingSetup(false, guid2);
                        break;
                    #endregion

                    #region MoqTest
                    case "MockABC":
                        var moqClass = new MoqTestingMethods();
                        moqClass.Test1();
                        break;
                    #endregion

                    #region DelegateComparerBenchmark
                    // Result: identical performance between the two ways
                    case "delegateComparer":
                        BenchmarkRunner.Run<DelegateComparerBenchmark>();
                        break;
                        #endregion


                    default:
                        Console.WriteLine("DO NOTHING......");
                        break;
                }
            }
        }
    }
}
