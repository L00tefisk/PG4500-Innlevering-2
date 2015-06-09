using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;


namespace PG4500_2015_Innlevering2.General
{
	static class MapHelper
	{
		private const int TileSize = 50;
		private const int MapWidth = 800;
		private const int MapHeight = 600;

		private const int ColMapWidth = 16;
		private const int ColMapHeight = 12;

		public static int[][] ColMap = {
			new int[] {0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			new int[] {0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0},
			new int[] {1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0},
			new int[] {1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0},
			new int[] {0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			new int[] {0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0},
			new int[] {0, 1, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0},
			new int[] {0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0},
			new int[] {0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
			new int[] {0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0},
			new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0},
			new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		};

		static public List<Node> AStarSearch2(Location Start, Location End, Robocode.AdvancedRobotEx robot)
		{
			bool foundPath = false;
			List<Node> path = new List<Node>();
			List<Node> neighbours = new List<Node>();

			Node start = new Node(Start.X, Start.Y, 0);
			start.Parent = start;
			int counter = 0;

			// initialize the open list
			PriorityQueue<Node> openList = new PriorityQueue<Node>();
			// initialize the closed list
			List<Node> closedList = new List<Node>();

			// put the starting node on the open list (you can leave its f at zero)

			start.f = 0;
			start.g = 0;
			start.h = 0;
			start.Parent = start;
			openList.Enqueue(start, 0);

			robot.Out.WriteLine("Searching...");

			// while the open list is not empty
			while(openList.Count > 0 && !foundPath)
			{
				counter++;
				if (counter > 500) // We couldn't find a path, return empty list;
					return path;

				// find the node with highest priority
				Node current = openList.Dequeue();
				closedList.Add(current);

				
				neighbours.Clear();
				// generate q's 8 successors and set their parents to q
				for (int y = -1; y < 2; y++)
				{
					for (int x = -1; x < 2; x++)
					{
						Location tempPos = new Location(current.position.X + x, current.position.Y + y);
						if (tempPos.X >= 0 && tempPos.X < ColMapWidth &&
							tempPos.Y >= 0 && tempPos.Y < ColMapHeight &&
							ColMap[tempPos.Y][tempPos.X] == 0)
						{
							Node n = new Node(tempPos.X, tempPos.Y, 0);
							n.Parent = current;

							if (Math.Abs(y + x) == 1)
								n.g = 10;
							else
								n.g = 14.1;
							
							neighbours.Add(n);
						}
					}
				}

				//  for each successor
				foreach(Node neighbour in neighbours)
				{
					bool skip = false;

					// if successor is the goal, stop the search
					if (neighbour.isEqual(End))
					{
						foundPath = true;
						current = neighbour;
						break;
					}

					//successor.g = q.g + distance between successor and q
					neighbour.g += current.g;

					//successor.h = distance from goal to successor
					neighbour.h = Heuristic(neighbour.position, End);

					//successor.f = successor.g + successor.h
					neighbour.f = neighbour.g + neighbour.h;

					// if a node with the same position as successor is in the OPEN list \
					// which has a lower f than successor, skip this successor

					for (int i = 0; i < openList.Count; i++)
					{
						int sum = ((neighbour.position.X + neighbour.position.Y) - (openList[i].position.X + openList[i].position.Y));
						if (openList[i].isEqual(neighbour.position) && openList[i].f < neighbour.f)
							skip = true;
					}

					// if a node with the same position as successor is in the CLOSED list \ 
					// which has a lower f than successor, skip this successor
					for (int i = 0; i < closedList.Count; i++)
					{
						int sum = ((neighbour.position.X + neighbour.position.Y) - (closedList[i].position.X + closedList[i].position.Y));
						if (closedList[i].isEqual(neighbour.position) && closedList[i].f < neighbour.f)
							skip = true;
					}

					// otherwise, add the node to the open list
					if (!skip)
						openList.Enqueue(neighbour, neighbour.f);

				}
				
				if (foundPath)
				{
					while (true)
					{
						path.Add(current);
						Console.Out.WriteLine("Node(" + current.position.X + ", " + current.position.Y + ")");
						current = current.Parent;
						if (current.isEqual(Start))
						{
							path.Add(current);
							break;
						}
					}
					path.Reverse();
				}
			}
			
			return path;
		}

        static public int Heuristic(Location a, Location b)
        {
            return 10 * (Math.Abs(a.X - b.X) + (int)Math.Abs(a.Y - b.Y));
        }
        static public int Cost(Location current, Location next)
        {
            int dX = (int)(next.X + current.X);
            int dY = (int)(next.Y + current.Y);
            //if abs(dX + dY) == 1, then the node is adjacent.
            if (Math.Abs(dX + dY) == 1)
                return 10;
            else
                return 15;
        }

		static public Location ConvertToColMap(int x, int y)
		{
			//ReSharper disable PossibleLossOfFraction, as we want to "Floor" the values anyway
			return new Location(x / TileSize, (MapHeight - y) / TileSize);
		}
	}
}
