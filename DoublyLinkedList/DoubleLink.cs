namespace DoublyLinkedList
{
    /// <summary>
    /// Element of doubly-linked list with link to random element.
    /// </summary>
    public class DoubleLink
    {
        public DoubleLink Prev { get; set; }
        public DoubleLink Next { get; set; }
        public DoubleLink Rand { get; set; }
        public string Value { get; set; }
    }
}