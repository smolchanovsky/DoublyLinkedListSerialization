using System.IO;
using System.Text;
using DoublyLinkedList.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace DoublyLinkedList.Tests
{
    public class DoubleLinkedListSerializerTests
    {
	    private readonly DoubleLinkedListSerializer linkedListSerializer;
	    private readonly DoubleLinkedList linkedList;

		public DoubleLinkedListSerializerTests()
        {
	        linkedListSerializer = new DoubleLinkedListSerializer();

			linkedList = DoubleLinkedListFactory.GetDoubleLinkedList(count: 3);
			linkedList.Head.Rand = linkedList.Tail;
			linkedList.Head.Next.Rand = linkedList.Head;
			linkedList.Tail.Rand = linkedList.Head.Next;
		}

		[Fact]
        public void Serialize_CorrectDoubleLinkedList_Success()
        {
            using (var resultStream = new MemoryStream())
            {
	            linkedListSerializer.Serialize(linkedList, resultStream, leaveOpen: true);

	            resultStream.ToArray().Should().Equal(GetExpectedStream().ToArray());
            }
		}

        [Fact]
        public void Deserialize_CorrectDoubleLinkedList_Success()
        {
            using (var expectedStream = GetExpectedStream())
            {
	            var result = linkedListSerializer.Deserialize(expectedStream, leaveOpen: true);

	            result.Should().Equal(linkedList, new DoubleLinkEqualityComparer().Equals);
            }
        }

	    private MemoryStream GetExpectedStream()
	    {
		    var expectedStream = new MemoryStream();
		    using (var binaryWriter = new BinaryWriter(expectedStream, Encoding.UTF8, leaveOpen: true))
		    {
			    binaryWriter.Write(linkedList.Head.Value);
			    binaryWriter.Write(2); // Tail index
			    binaryWriter.Write(linkedList.Head.Next.Value);
			    binaryWriter.Write(0); // Head index
			    binaryWriter.Write(linkedList.Tail.Value);
			    binaryWriter.Write(1); // Head.Next index
		    }

		    expectedStream.Position = 0;
		    return expectedStream;
		}
    }
}