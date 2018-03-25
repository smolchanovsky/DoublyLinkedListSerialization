using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DoublyLinkedList
{
    /// <summary>
    /// Doubly-linked list where each element contains links to the previous, next and random elements.
    /// </summary>
    public class ListRand : IEnumerable<ListNode>
    {
        public ListNode Head { get; set; }
        public ListNode Tail { get; set; }
        public int Count { get; set; }

        /// <summary>
        /// Binary serialization of list.
        /// </summary>
        /// <param name="s">FileStream for result of serializing list.</param>
        public void Serialize(FileStream s)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));

            try
            {
                // Indexing list items
                var nodeIndices = new Dictionary<ListNode, int>();
                foreach ((ListNode node, int index) in this.Select((node, index) => (node, index)))
                {
                    nodeIndices.Add(node, index);
                }

                // Serialization of the list in format: data element, index of element
                using (var binaryWriter = new BinaryWriter(s))
                {
                    foreach (var node in this)
                    {
                        binaryWriter.Write(node.Data);
                        binaryWriter.Write(nodeIndices[node.Rand]);
                    }
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
        /// <param name="s">FileStream that contains the serialized list.</param>
        public void Deserialize(FileStream s)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));

            try
            {
                var deserializedNodes = new List<ListNode>();
                var randomNodeIndices = new Dictionary<ListNode, int>();    // dictionary containing index of random element for each list item

                using (var binaryReader = new BinaryReader(s))
                {
                    while (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
                    {
                        var node = new ListNode
                        {
                            Prev = deserializedNodes.LastOrDefault(),
                            Data = binaryReader.ReadString(),
                        };

                        var randNodeIndex = binaryReader.ReadInt32();
                        randomNodeIndices.Add(node, randNodeIndex);

                        if (deserializedNodes.Any())
                            deserializedNodes.Last().Next = node;

                        deserializedNodes.Add(node);
                    }
                }

                Head = deserializedNodes.FirstOrDefault();
                Tail = deserializedNodes.LastOrDefault();
                Count = deserializedNodes.Count;

                foreach (var node in this)
                {
                    var randIndex = randomNodeIndices[node];    // index of random element for node
                    node.Rand = deserializedNodes[randIndex];
                }
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

        #region IEnumerable

        /// <summary>
        /// Returns generic enumerator of list items.
        /// </summary>
        /// <returns>Generic enumerator for list.</returns>
        public IEnumerator<ListNode> GetEnumerator()
        {
            var current = Head;

            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }

        /// <summary>
        /// Returns enumerator of list items.
        /// </summary>
        /// <returns>Non-generic enumerator for list.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}