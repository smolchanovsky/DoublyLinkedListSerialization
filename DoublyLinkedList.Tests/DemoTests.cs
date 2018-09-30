using System.IO;
using DoublyLinkedList.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace DoublyLinkedList.Tests
{
    /// <summary>
    /// Tests to demonstrate functionality.
    /// </summary>
    public class DemoTests
    {
	    private readonly DoubleLinkedListSerializer linkedListSerializer = new DoubleLinkedListSerializer();

        /// <summary>
        /// Demonstrates the functionality of a class <see cref="DoublyLinkedList.DoubleLinkedList"/>.
        /// </summary>
        [Fact]
        public void DoubleLinkedList()
        {
            using (var memoryStream = new MemoryStream())
            {
	            var linkedList = DoubleLinkedListFactory.GetDoubleLinkedList(count: 10);
				linkedListSerializer.Serialize(linkedList, memoryStream, leaveOpen: true);

	            memoryStream.Position = 0;
				var deserializedLinkedList = linkedListSerializer.Deserialize(memoryStream, leaveOpen: true);

	            linkedList.Should().Equal(deserializedLinkedList, new DoubleLinkEqualityComparer().Equals);
			}
        }
    }
}