using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSafeRepository.TestingMethods
{
    public class MoqTestingMethods
    {
        public void Test1()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            var mockClassB = MockTestClassBSetup().Object;
            var classA = new ClassA(mockClassB);
            Console.WriteLine(classA.ANextDouble());
        }

        public Mock<IClassB> MockTestClassBSetup()
        {
            var mockClassB = new Mock<IClassB>();
            var rand = new Random(5);
            Console.WriteLine($"in MockTestClassBSetup, rand:{rand.GetHashCode()}");
            mockClassB.Setup(b => b.BNextDouble()).Returns(() => 
            {
                Console.WriteLine($"in lambda, rand:{rand.GetHashCode()}");
                return rand.NextDouble(); 
            });

            return mockClassB;
        }
    }

    public interface IClassA
    {
        double ANextDouble();
    }

    public interface IClassB
    {
        double BNextDouble();
    }

    public class ClassA : IClassA
    {
        private readonly IClassB _classB;
        public ClassA(IClassB classB)
        {
            _classB = classB;
        }

        public double ANextDouble() => _classB.BNextDouble();
    }
}
