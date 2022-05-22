using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSafeRepository.TestingMethods
{
    public class DelegateComparerBenchmark
    {
        private readonly List<TestClassAA> list1;
        private readonly List<TestClassAA> list2;

        public DelegateComparerBenchmark()
        {
            var rand = new Random();
            list1 = new List<TestClassAA>();
            list2 = new List<TestClassAA>();
            //
            list1.Add(new TestClassAA() { Id = rand.Next(1,100), Text = Guid.NewGuid().ToString() });
            list1.Add(new TestClassAA() { Id = rand.Next(1, 100), Text = Guid.NewGuid().ToString() });
            list1.Add(new TestClassAA() { Id = rand.Next(1, 100), Text = Guid.NewGuid().ToString() });
            list1.Add(new TestClassAA() { Id = rand.Next(1, 100), Text = Guid.NewGuid().ToString() });
            list1.Add(new TestClassAA() { Id = rand.Next(1, 100), Text = Guid.NewGuid().ToString() });
            //
            list2.Add(new TestClassAA() { Id = rand.Next(1, 100), Text = Guid.NewGuid().ToString() });
            list2.Add(new TestClassAA() { Id = rand.Next(1, 100), Text = Guid.NewGuid().ToString() });
            list2.Add(new TestClassAA() { Id = rand.Next(1, 100), Text = Guid.NewGuid().ToString() });
            list2.Add(new TestClassAA() { Id = rand.Next(1, 100), Text = Guid.NewGuid().ToString() });
            list2.Add(new TestClassAA() { Id = rand.Next(1, 100), Text = Guid.NewGuid().ToString() });
        }

        [Benchmark]
        public void SortByDelegate()
        {
            list1.Sort(delegate (TestClassAA a, TestClassAA b) { return a.Text.CompareTo(b.Text); });
        }

        [Benchmark]
        public void SortByComparer()
        {
            list2.Sort(Comparer<TestClassAA>.Create((a,b) => a.Text.CompareTo(b.Text)));
        }


    }

    public class TestClassAA
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
