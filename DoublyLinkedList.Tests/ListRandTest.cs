using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Xunit;

namespace DoublyLinkedList.Tests
{
    public class ListRandTest : ListRandTestBase
    {
        public string ExpextedFile { get; } = "expectedResult.txt";
        public string ResultFile { get; } = "result.txt";
        public ListRand TestListRand { get; }

        public ListRandTest()
        {
            #region InitTestData

            TestListRand = GenerateListRand(count: 3);
            TestListRand.Head.Rand = TestListRand.Tail;
            TestListRand.Head.Next.Rand = TestListRand.Head;
            TestListRand.Tail.Rand = TestListRand.Head.Next;

            using (var expectedFileStream = new FileStream(ExpextedFile, FileMode.Create, FileAccess.Write))
            {
                using (var binaryWriter = new BinaryWriter(expectedFileStream))
                {
                    binaryWriter.Write(TestListRand.Head.Data);
                    binaryWriter.Write(2);  // Tail index
                    binaryWriter.Write(TestListRand.Head.Next.Data);
                    binaryWriter.Write(0);  // Head index
                    binaryWriter.Write(TestListRand.Tail.Data);
                    binaryWriter.Write(1);  // Head.Next index
                }
            }

            #endregion
        }

        [Fact]
        public void Serialize_CorrectListRand_Success()
        {
            using (var resultFileStream = new FileStream(ResultFile, FileMode.Create, FileAccess.Write))
            {
                TestListRand.Serialize(resultFileStream);
            }

            using (FileStream resultFileStream = new FileStream(ResultFile, FileMode.Open, FileAccess.Read),
                expectedFileStream = new FileStream(ExpextedFile, FileMode.Open, FileAccess.Read))
            {
                using (var md5 = MD5.Create())
                {
                    Assert.Equal(md5.ComputeHash(expectedFileStream), md5.ComputeHash(resultFileStream));
                }
            }
        }

        [Fact]
        public void Deserialize_CorrectListRand_Success()
        {
            using (var expectedFileStream = new FileStream(ExpextedFile, FileMode.Open, FileAccess.Read))
            {
                var resultListRand = new ListRand();
                resultListRand.Deserialize(expectedFileStream);
                Assert.True(TestListRand.SequenceEqual(resultListRand, new ListNodeComparer()));
            }
        }
    }
}