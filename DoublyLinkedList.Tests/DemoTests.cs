using System.IO;
using System.Linq;
using Xunit;

namespace DoublyLinkedList.Tests
{
    /// <summary>
    /// Tests to demonstrate functionality.
    /// </summary>
    public class DemoTests : ListRandTestBase
    {
        public string DataFile { get; } = "data.txt";

        /// <summary>
        /// Demonstrates the functionality of a class <see cref="DoublyLinkedList.ListRand"/>.
        /// </summary>
        [Fact]
        public void ListRand()
        {
            var testListRand = GenerateListRand(count: 10);
            using (var fileStream = new FileStream(DataFile, FileMode.Create, FileAccess.Write))
            {
                testListRand.Serialize(fileStream);
            }

            var deserializedListRand = new ListRand();
            using (var fileStream = new FileStream(DataFile, FileMode.Open, FileAccess.Read))
            {
                deserializedListRand.Deserialize(fileStream);
            }

            Assert.True(testListRand.SequenceEqual(deserializedListRand, new ListNodeComparer()));
        }
    }
}