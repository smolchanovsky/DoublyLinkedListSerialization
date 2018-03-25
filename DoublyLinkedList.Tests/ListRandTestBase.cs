using System;
using System.Collections.Generic;
using System.Linq;

namespace DoublyLinkedList.Tests
{
    public class ListRandTestBase
    {
        /// <summary>
        /// Generator doubly-linked list <see cref="ListRand"/>.
        /// </summary>
        /// <param name="count">The number of items in list.</param>
        /// <returns>Doubly linked list.</returns>
        public ListRand GenerateListRand(int count)
        {
            var testNodes = new List<ListNode>();
            foreach (var i in Enumerable.Range(0, count))
            {
                var node = new ListNode
                {
                    Prev = testNodes.LastOrDefault(),
                    Data = $"Node #{i} data",
                };

                if (testNodes.Any())
                    testNodes.Last().Next = node;

                testNodes.Add(node);
            }

            var testListRand = new ListRand
            {
                Count = count,
                Head = testNodes.FirstOrDefault(),
                Tail = testNodes.LastOrDefault()
            };

            var randomIndexGenerator = new Random(testListRand.Count);
            testListRand
                .ToList()
                .ForEach(node =>
                {
                    var randomNodeIndex = randomIndexGenerator.Next(maxValue: testListRand.Count);
                    node.Rand = testNodes[randomNodeIndex];
                });

            return testListRand;
        }

        /// <summary>
        /// Provides comparison of elements of doubly linked lists with unique data. <see cref="ListNode"/>.
        /// </summary>
        public class ListNodeComparer : IEqualityComparer<ListNode>
        {
            public bool Equals(ListNode x, ListNode y)
            {
                if (ReferenceEquals(x, y))
                    return true;

                if (x == null || y == null)
                    return false;

                return x.Data == y.Data && x.Rand?.Data == y.Rand?.Data;
            }

            public int GetHashCode(ListNode obj)
            {
                unchecked
                {
                    return ((obj.Rand?.Data != null ? obj.Rand.Data.GetHashCode() : 0) * 397) ^
                           (obj.Data != null ? obj.Data.GetHashCode() : 0);
                }
            }
        }
    }
}
