using System;
using System.Collections.Generic;
using System.Linq;

namespace DoublyLinkedList.Tests.Helpers
{
	public static class DoubleLinkedListFactory
	{
		/// <summary>
		/// Generator doubly-linked list <see cref="DoubleLinkedList"/>.
		/// </summary>
		/// <param name="count">The number of items in list.</param>
		/// <returns>Doubly linked list.</returns>
		public static DoubleLinkedList GetDoubleLinkedList(int count)
		{
			var testNodes = new List<DoubleLink>();
			foreach (var i in Enumerable.Range(0, count))
			{
				var node = new DoubleLink
				{
					Prev = testNodes.LastOrDefault(),
					Value = $"Node #{i} data",
				};

				if (testNodes.Any())
					testNodes.Last().Next = node;

				testNodes.Add(node);
			}

			var testListRand = new DoubleLinkedList
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
	}
}