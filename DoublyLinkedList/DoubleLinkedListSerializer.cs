using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DoublyLinkedList.Utils;

namespace DoublyLinkedList
{
	public class DoubleLinkedListSerializer
	{
		/// <summary>
		/// Binary serialization of list.
		/// </summary>
		/// <param name="linkedList"></param>
		/// <param name="stream">FileStream for result of serializing list.</param>
		/// <param name="leaveOpen"></param>
		public void Serialize(DoubleLinkedList linkedList, Stream stream, bool leaveOpen = false)
		{
			if (linkedList == null)
				throw new ArgumentNullException(nameof(linkedList));
			if (stream == null)
				throw new ArgumentNullException(nameof(stream));

			try
			{
				var nodeIndices = linkedList
					.Select((doubleLink, index) => (doubleLink, index))
					.ToDictionary(keyValue => keyValue.doubleLink, keyValue => keyValue.index);

				using (var binaryWriter = new BinaryWriter(stream, Encoding.UTF8, leaveOpen))
					foreach (var doubleLink in linkedList)
					{
						binaryWriter.Write(doubleLink.Value);
						binaryWriter.Write(nodeIndices[doubleLink.Rand]);
					}
			}
			catch (Exception ex) when (ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Serialization error", ex);
			}
		}

		/// <summary>
		/// Deserialization of binary data to list.
		/// </summary>
		/// <param name="stream">FileStream that contains the serialized list.</param>
		/// <param name="leaveOpen"></param>
		public DoubleLinkedList Deserialize(Stream stream, bool leaveOpen = false)
		{
			if (stream == null)
				throw new ArgumentNullException(nameof(stream));

			try
			{
				var deserializedNodes = new List<DoubleLink>();
				var randomNodeIndices = new Dictionary<DoubleLink, int>();    // dictionary containing index of random element for each list item

				using (var binaryReader = new BinaryReader(stream, Encoding.UTF8, leaveOpen))
				{
					while (binaryReader.CanRead())
					{
						var node = new DoubleLink
						{
							Prev = deserializedNodes.LastOrDefault(),
							Value = binaryReader.ReadString(),
						};

						var randNodeIndex = binaryReader.ReadInt32();
						randomNodeIndices.Add(node, randNodeIndex);

						if (deserializedNodes.Any())
							deserializedNodes.Last().Next = node;

						deserializedNodes.Add(node);
					}
				}

				var linkedList = new DoubleLinkedList
				{
					Head = deserializedNodes.FirstOrDefault(),
					Tail = deserializedNodes.LastOrDefault(),
					Count = deserializedNodes.Count
				};

				foreach (var node in linkedList)
				{
					var randIndex = randomNodeIndices[node];    // index of random element for node
					node.Rand = deserializedNodes[randIndex];
				}

				return linkedList;
			}
			catch (Exception ex) when (ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Deserialization error", ex);
			}
		}
	}
}