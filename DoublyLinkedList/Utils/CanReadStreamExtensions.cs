using System.IO;

namespace DoublyLinkedList.Utils
{
	public static class CanReadStreamExtensions
	{
		public static bool CanRead(this Stream stream) => 
			stream.Position != stream.Length;

		public static bool CanRead(this BinaryReader binaryReader) => 
			CanRead(binaryReader.BaseStream);
	}
}