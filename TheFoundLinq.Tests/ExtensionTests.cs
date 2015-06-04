using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TheFoundLinq.Extensions;

namespace TheFoundLinq.Tests
{
    [TestFixture]
    public class ExtensionTests
    {
        [Test]
        [TestCase("CommonPropertyValue")]
        [TestCase(null)]
        public void Test_Distinct(string commonPropValue)
        {
            int count = 10;
            var items = GenerateItems(count);
            for (int i = 0; i < count; i += 2)
            {
                items[i].StrProp = commonPropValue;
            }

            var expected = items
                .GroupBy(x => x.StrProp)
                .Select(g => new
                {
                    StrProp = g.Key,
                    Count = g.Count()
                })
                .ToList();

            var result = items.Distinct(x => x.StrProp)
                .ToList();

            Assert.AreEqual(expected.Count, result.Count);
            for(int i = 0; i< expected.Count; i++)
            {
                Assert.AreEqual(result[i].StrProp, expected[i].StrProp);
            }
        }

        [Test]
        public void Test_Except()
        {
            int firstCount = 10;
            int secondCount = 5;

            var first = GenerateItems(firstCount);
            var second = GenerateItems(0, secondCount, 2);

            var expected = first
                .Where(x => !second.Any(i => i.StrProp == x.StrProp))
                .ToList();

            var result = first.Except(second, x => x.StrProp)
                .ToList();

            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(result[i].StrProp, expected[i].StrProp);
            }
        }

        [Test]
        public void Test_Union()
        {
            int firstCount = 10;
            int secondCount = 10;

            int intersectionCount = 5;

            var first = GenerateItems(firstCount);
            var second = GenerateItems(firstCount - intersectionCount, secondCount);

            var secondMinusIntersection = second
                .Where(s => !first.Any(f => f.StrProp == s.StrProp))
                .ToList();

            var expected = first
                .ToList();
            expected.AddRange(secondMinusIntersection);

            var result = first.Union(second, x => x.StrProp)
                .ToList();

            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(result[i].StrProp, expected[i].StrProp);
            }
        }

        [Test]
        public void Test_Intersect()
        {
            int firstCount = 10;
            int secondCount = 10;

            int intersectionCount = 5;

            var first = GenerateItems(firstCount);
            var second = GenerateItems(firstCount - intersectionCount, secondCount);

            var expected = first
                .Where(f => second.Any(s => s.StrProp == f.StrProp))
                .ToList();

            var result = first.Intersect(second, x => x.StrProp)
                .ToList();

            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(result[i].StrProp, expected[i].StrProp);
            }
        }

        [Test]
        public void Test_SequenceEqual()
        {
            int count = 10;
            
            var first = GenerateItems(count);
            var second = GenerateItems(count);

            foreach (var item in second)
            {
                item.IntProp += count;
            }

            var resultEqual = first
                .SequenceEqual(second, x => x.StrProp);

            var resultNonEqual = first
                .SequenceEqual(second, x => x.IntProp);

            Assert.IsTrue(resultEqual);
            Assert.IsFalse(resultNonEqual);
        }

        [Test]
        public void Test_ForEachThen()
        {
            int count = 10;
            var items = GenerateItems(count);
            string commonPropValue = "CommonValue";
            int commonIntValue = 10;

            int numberOfItemsCount = 0;
            
            var newItems = items
                .ForEachThen(i =>
                {
                    numberOfItemsCount++;
                })
                .ForEachThen(i => i.StrProp = commonPropValue)
                .ForEachThen(i => i.IntProp = commonIntValue)
                .ToList();

            foreach (var item in newItems)
            {
                Assert.AreEqual(commonPropValue, item.StrProp);
                Assert.AreEqual(commonIntValue, item.IntProp);
            }

            Assert.AreEqual(numberOfItemsCount, newItems.Count);
        }

        private List<TestSubject> GenerateItems(int count)
        {
            return GenerateItems(0, count);
        }

        private List<TestSubject> GenerateItems(int startIndex, int count, int indexIncrement = 1)
        {
            var result = new List<TestSubject>();
            int totalCount = startIndex + count;
            for (int i = startIndex; i < totalCount; i += indexIncrement)
            {
                var item = new TestSubject();
                item.StrProp = "String Property " + i;
                item.IntProp = i;
                item.GuidProp = Guid.NewGuid();
                item.DateProp = DateTime.Now.AddMinutes(i);
                result.Add(item);
            }

            return result;
        }

        public class TestSubject
        {
            public string StrProp { get; set; }
            public int IntProp { get; set; }
            public Guid GuidProp { get; set; }
            public DateTime DateProp { get; set; }
        }

    }
}
