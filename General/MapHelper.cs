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

		static public Point2D getRandomPosition()
		{
			Random rand = new Random();
			int x = 5, y = 0;
			while(ColMap[y][x] == 1)
			{
				x = rand.Next(3, ColMapWidth-3);
				y = rand.Next(3, ColMapHeight-3);
			}
			return new Point2D(x, y);
		}

        static public int Heuristic(Point2D a, Point2D b)
        {
            return 2 * Math.Max((int)(Math.Abs(a.X - b.X)), (int)Math.Abs(a.Y - b.Y));
        }
        static public int Cost(Point2D current, Point2D next)
        {
            int dX = (int)(next.X + current.X);
            int dY = (int)(next.Y + current.Y);
            //if abs(dX + dY) == 1, then the node is adjacent and not diagonal.
            if (Math.Abs(dX + dY) == 1)
                return 1;
            else
                return 2;
        }
        static public Dictionary<Node, Node> findPath(Point2D start, Point2D end, Robocode.AdvancedRobotEx robot)
		{
			
            PriorityQueue<Node> openList = new PriorityQueue<Node>();
            Dictionary<Node, int> distance = new Dictionary<Node, int>();
            Dictionary<Node, Node> parent = new Dictionary<Node, Node>();


			Node nodeStart = new Node(start);
            Node goal = new Node(end);

            robot.Out.WriteLine("nodeStart: " + nodeStart.position.X + " " + nodeStart.position.Y);
            robot.Out.WriteLine("goal: " + goal.position.X + " " + goal.position.Y);

            openList.Enqueue(nodeStart, 0);
            parent.Add(nodeStart, nodeStart);
            distance.Add(nodeStart, 0);

            while (openList.Count > 0)
            {
                Node current = openList.Dequeue();
				if (current.position.X == end.X && current.position.Y == end.Y)
				{
					robot.Out.WriteLine("Found path!");
					break;
				}


				robot.Out.WriteLine("Current Node: " + current.position.Y + " " + current.position.X);
				robot.Out.WriteLine("Neighbours.Count: " + current.Neighbours.Count);
                foreach (Node next in current.Neighbours)
                {
					robot.Out.WriteLine("Next Node: " + next.position.Y + " " + next.position.X);
                    // Calculate new cost, check the neighbours if they are passable.
                    if(next.position.X >= 0 && next.position.Y >= 0 && next.position.X < 16 && next.position.Y < 12 &&
						ColMap[(int)next.position.Y][(int)next.position.X] == 0)
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
			robot.Out.WriteLine("Ran out of options.");
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