using System.Collections.Generic;

namespace DoublyLinkedList.Tests.Helpers
{
	/// <summary>
	/// Provides comparison of elements of doubly linked lists with unique data. <see cref="DoubleLink"/>.
	/// </summary>
	public class DoubleLinkEqualityComparer : IEqualityComparer<DoubleLink>
	{
		public bool Equals(DoubleLink x, DoubleLink y)
		{
			if (ReferenceEquals(x, y))
				return true;
			if (x == null || y == null)
				return false;

			return x.Value == y.Value && x.Rand?.Value == y.Rand?.Value;
		}

		public int GetHashCode(DoubleLink obj)
		{
			unchecked
			{
				return ((obj.Rand?.Value != null ? obj.Rand.Value.GetHashCode() : 0) * 397) ^
				       (obj.Value != null ? obj.Value.GetHashCode() : 0);
			}
		}
	}
}