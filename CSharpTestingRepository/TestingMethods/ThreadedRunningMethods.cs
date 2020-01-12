using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThreadSafeRepository.Model;
using ThreadSafeRepository.Repository;

namespace ThreadSafeRepository.TestingMethods
{
    class ThreadedRunningMethods
    {
        public static void TryTwoReposWithSameConn()
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

        public static void TryTwoRepoWithSameContext()
        {
            // no error
            var context = new LocalThreadSafeEntities();
            var repo1 = new UnsafeRepository(context);
            var repo2 = new UnsafeRepository(context);
            //
            repo1.CreateUsingSP(89, 8674, 33);
            repo2.CreateUsingSP(3, 299, 44);
        }

        public static void ThreadedTwoRepoTwoContextTwoConn()
        {
            // no error
            Thread t1 = new Thread(new ThreadStart(RunSPofRepo1));
            t1.Name = "thread1";
            Thread t2 = new Thread(new ThreadStart(RunSPofRepo2));
            t2.Name = "thread2";
            t1.Start();
            t2.Start();
        }

        public static void ThreadedTwoRepoTwoContextOneConn()
        {
            // gives error
            EntityConnection conn = new EntityConnection("metadata=res://*/Model.Model1.csdl|res://*/Model.Model1.ssdl|res://*/Model.Model1.msl;provider=System.Data.SqlClient;provider connection string=';data source=DESKTOP-RYZEN\\SQLEXPRESS;initial catalog=LocalThreadSafe;integrated security=True;max pool size=10;MultipleActiveResultSets=True;App=EntityFramework';");
            var context1 = new LocalThreadSafeEntities(conn, false);
            var context2 = new LocalThreadSafeEntities(conn, false);
            var repo1 = new UnsafeRepository(context1);
            var repo2 = new UnsafeRepository(context2);

            Thread t1 = new Thread(new ParameterizedThreadStart(RunSPofRepoWithRepo1));
            t1.Name = "thread1";
            Thread t2 = new Thread(new ParameterizedThreadStart(RunSPofRepoWithRepo2));
            t2.Name = "thread2";
            t1.Start(repo1);
            t2.Start(repo2);
        }


        static void RunSPofRepo1()
        {
            LocalThreadSafeEntities context = new LocalThreadSafeEntities();
            UnsafeRepository repo = new UnsafeRepository(context);
            Console.WriteLine(Thread.CurrentThread.Name);
            repo.CreateUsingSP(5, 720, 55);
        }

        static void RunSPofRepo2()
        {
            LocalThreadSafeEntities context = new LocalThreadSafeEntities();
            UnsafeRepository repo = new UnsafeRepository(context);
            Console.WriteLine(Thread.CurrentThread.Name);
            repo.CreateUsingSP(66, 8800, 66);
        }

        static void RunSPofRepoWithRepo1(object repository)
        {
            var repo = (UnsafeRepository)repository;
            Console.WriteLine(Thread.CurrentThread.Name);
            repo.CreateUsingSP(2, 67676, 77);
        }

        static void RunSPofRepoWithRepo2(object repository)
        {
            var repo = (UnsafeRepository)repository;
            Console.WriteLine(Thread.CurrentThread.Name);
            repo.CreateUsingSP(4, 1234, 88);
        }
    }
}
