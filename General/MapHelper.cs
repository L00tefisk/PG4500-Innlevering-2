using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        static public int Heuristic(Point2D a, Point2D b)
        {
            return (int)(Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y));
        }
        static public int Cost(Point2D current, Point2D next)
        {
            int dX = next.X - current.X;
            int dY = next.Y - current.Y;
            //if abs(dX + dY) == 0, then the node is adjacent and not diagonal.
            if (Math.Abs(dX + dY) == 0)
                return 1;
            else
                return 2;
        }
        static public Dictionary<Node, Node> findPath(Point2D start, Point2D end)
		{
            int f = 0, g = 0, h = 0;
            PriorityQueue<Node> openList = new PriorityQueue<Node>();
            List<Node> closedList = new List<Node>();
            Dictionary<Node, int> distance = new Dictionary<Node, int>();
            Dictionary<Node, Node> parent = new Dictionary<Node, Node>();
            
            Node nodeStart = new Node(start);
            Node goal = new Node(end);
            openList.Enqueue(nodeStart, 0);
            parent.Add(nodeStart, nodeStart);
            distance.Add(nodeStart, 0);

            

            while (openList.Count > 0)
            {
                Node current = openList.Dequeue();
                if (current.Equals(goal))
                    break;

                foreach (Node next in current.Neighbours)
                {
                    // Calculate new cost, check the neighbours if they are passable.
                    if(ColMap[next.position.X][next.position.Y] == 0)
                    {
                        int newCost = distance[current] + Cost(current.position, next.position);
                         
                        // Check if the neighbour isn't already in the open queue
                        if(!distance.ContainsKey(next) || newCost < distance[next])
                        {
                            distance.Add(next, newCost);
                            int priority = newCost + Heuristic(current.position, next.position);
                            openList.Enqueue(next, priority);
                            parent.Add(next, current);
                        }
                    }
                }   
            }

            return parent;
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

/*
cost is sometimes written as w or d or l or length
cost_so_far is usually written as g or d or distance
heuristic is usually written as h
In A*, the priority is usually written as f, where f = g + h
came_from is sometimes written as π or parent or previous or prev
frontier is usually called OPEN
visited is the union of OPEN and CLOSED
locations such as current and next are written with letters u, v

 priority => f = g + h
 * 
 *  var OPEN = new PriorityQueue<Location>();
    OPEN.Enqueue(start, 0);

    parent.Add(start, start);
    distance.Add(start, 0);

    while (frontier.Count > 0)
    {
        var current = OPEN.Dequeue();

        if (current.Equals(goal))
        {
            break;
        }

        foreach (var next in graph.Neighbors(current))
        {
            int newCost = distance[current]
                + graph.Cost(current, next);
            if (!distance.ContainsKey(next)
                || newCost < costSoFar[next])
            {
                distance.Add(next, newCost);
                int priority = newCost + Heuristic(next, goal);
                OPEN.Enqueue(next, priority);
                parent.Add(next, current);
            }
        }
    }
*/