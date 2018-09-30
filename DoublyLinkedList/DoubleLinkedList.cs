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
    public class DoubleLinkedList : IEnumerable<DoubleLink>
    {
        public DoubleLink Head { get; set; }
        public DoubleLink Tail { get; set; }
        public int Count { get; set; }

        #region IEnumerable

        /// <summary>
        /// Returns generic enumerator of list items.
        /// </summary>
        /// <returns>Generic enumerator for list.</returns>
        public IEnumerator<DoubleLink> GetEnumerator()
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