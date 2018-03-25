namespace DoublyLinkedList
{
    /// <summary>
    /// Element of doubly-linked list with link to random element.
    /// </summary>
    public class ListNode
    {
        public ListNode Prev { get; set; }
        public ListNode Next { get; set; }
        public ListNode Rand { get; set; }
        public string Data { get; set; }
    }
}