using System;
using System.Collections.Generic;

namespace ConsolePathfinding.General
{
	public static class MapHelper
	{
		private const int TileSize = 50;
		private const int MapWidth = 800;
		private const int MapHeight = 600;

		private const int ColMapWidth = 16;
		private const int ColMapHeight = 12;
		/*
		public static int[][] ColMap = {
			new int[]{0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			new int[]{0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0},
			new int[]{1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0},
			new int[]{1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0},
			new int[]{0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			new int[]{0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0},
			new int[]{0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0},
			new int[]{0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0},
			new int[]{0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
			new int[]{0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0},
			new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0},
			new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		};
		*/
		public static int[][] ColMap = {
			new int[]{0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			new int[]{0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0},
			new int[]{1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0},
			new int[]{1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0, 1, 1, 0},
			new int[]{0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0},
			new int[]{0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0},
			new int[]{0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0},
			new int[]{0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0},
			new int[]{0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
			new int[]{0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0},
			new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0},
			new int[]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		};
		static public Point2D getRandomPosition()
		{
			Random rand = new Random();
			int x = 5, y = 0;
			while (ColMap[y][x] == 1)
			{
				x = rand.Next(3, ColMapWidth - 3);
				y = rand.Next(3, ColMapHeight - 3);
			}
			return new Point2D(x, y);
		}

		static public Node FindPath(Point2D start, Point2D end)
		{
			List<Node> visited = new List<Node>();
			PriorityQueue<Node> pQueue = new PriorityQueue<Node>();

			Node current = new Node(start);
			current.traveled = 0;
			Node target = new Node(end);

			pQueue.Enqueue(current, 0);
			while (pQueue.Count != 0)
			{
				//Console.Write(current.position);
				current = pQueue.Dequeue();
				//Console.WriteLine(" -> " + current.position);

				if (current.Equals(target))
					return current;

				visited.Add(current);
				ColMap[(int)current.position.Y][(int)current.position.X] += 2;

				Console.Clear();
				for (int y = 0; y < 12; y++)
				{
					for (int x = 0; x < 16; x++)
					{
						Console.Write(ColMap[y][x] + " ");
					}
					Console.Write("\n");
				}
				System.Threading.Thread.Sleep(30);
				foreach (Node n in current.Neighbours)
				{
					n.parent = current;
					n.traveled = current.traveled + n.position.FakeDistance(current.position);
					n.remaining = n.position.Distance(end);
					n.total = n.traveled + n.remaining;
					if (pQueue.Count != 0)
					{
						if (!visited.Contains(n)) // || n.total < pQueue.GetPrioritized().total)
						{
							pQueue.Enqueue(n, n.total);
						}
					}
					else if (!visited.Contains(n))
					{
						pQueue.Enqueue(n, n.total);
					} 
					//else if (visited.Contains(n))
					//	Console.WriteLine(n.position + " is already checked");
				}
					
			}
			return null;
		}

		static public Point2D ConvertToColMap(int x, int y)
		{
			//ReSharper disable PossibleLossOfFraction , as we want to "Floor" the values anyway
			return new Point2D(x / TileSize, y / TileSize);
		}

		static public Point2D ConvertFromColMap(int x, int y)
		{
			return new Point2D((x * TileSize) + (TileSize / 2), MapHeight - (y * TileSize) + (TileSize / 2));
		}
	}
}