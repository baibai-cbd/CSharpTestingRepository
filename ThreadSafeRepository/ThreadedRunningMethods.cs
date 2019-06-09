using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThreadSafeRepository.Model;

namespace ThreadSafeRepository
{
    class ThreadedRunningMethods
    {
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
