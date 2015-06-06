using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG4500_2015_Innlevering2.General
{
	public class PriorityQueue<T>
	{
		// I'm using an unsorted array for this example, but ideally this
		// would be a binary heap. Find a binary heap class:
		// * https://bitbucket.org/BlueRaja/high-speed-priority-queue-for-c/wiki/Home
		// * http://visualstudiomagazine.com/articles/2012/11/01/priority-queues-with-c.aspx
		// * http://xfleury.github.io/graphsearch.html
		// * http://stackoverflow.com/questions/102398/priority-queue-in-net

		private List<Tuple<Node, double>> elements = new List<Tuple<Node, double>>();
		public Node this[int index]
		{
			get{ return elements[index].Item1; }
		}
		public double Count
		{
			get { return elements.Count; }
		}

		public void Enqueue(Node item, double priority)
		{
			elements.Add(Tuple.Create(item, priority));
		}

		public Node Dequeue()
		{
			int bestIndex = 0;

			for (int i = 0; i < elements.Count; i++)
			{
				if (elements[i].Item2 < elements[bestIndex].Item2)
				{
					bestIndex = i;
				}
			}

			Node bestItem = elements[bestIndex].Item1;
			elements.RemoveAt(bestIndex);
			return bestItem;
		}
		
		public bool Contains(Node node)
		{
			for (int i = 0; i < elements.Count; i++)
			{
				if (node.isEqual(elements[i].Item1.position))
					return true;
			}
			return false;
		}

	}

}
