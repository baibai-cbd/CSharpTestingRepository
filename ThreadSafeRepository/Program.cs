using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadSafeRepository.Model;

namespace ThreadSafeRepository
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var i = Console.Read();
                char c = (char)i;

                switch (c)
                {
                    case '1':
                        Console.WriteLine("try insert with 2 repos, 2 contexts, 1 connection setting");
                        TryRacingReposWithSameConn();
                        break;

                    case '2':
                        Console.WriteLine("try insert with 1 repos, 1 contexts, 1 connection setting");
                        break;

                    case '3':
                        Console.WriteLine("try multi-thread with 2 repos, 2 contexts, 1 connection setting");
                        break;

                    default:
                        Console.WriteLine("DO NOTHING......");
                        break;
                }
            }
        }

        static void TryRacingReposWithSameConn()
        {
            var context1 = new LocalThreadSafeEntities();
            var context2 = new LocalThreadSafeEntities();
            var repo1 = new UnsafeRepository(context1);
            var repo2 = new UnsafeRepository(context2);
            //
            repo1.CreateUsingSP(23, 3453, 11);
            repo2.CreateUsingSP(9, 776, 22);
        }
    }
}
