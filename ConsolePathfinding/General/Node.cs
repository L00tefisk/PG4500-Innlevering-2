using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsolePathfinding.General
{
	public class Node
	{
		public int id;
		public static int idcount = 0;
        public readonly Point2D position;
        public List<Node> neighbours;
		public Node parent;
		public double traveled;
		public double remaining;
		public double total;
        public Node(Point2D point)
        {
			id = idcount++;
			position = new Point2D();
            position.X = point.X;
            position.Y = point.Y;
        }

        public List<Node> Neighbours
        {
            get
            {
                
                if(neighbours == null)
                {
                    neighbours = new List<Node>(8);
					neighbours.Add(new Node(new Point2D(position.X - 1, position.Y + 1)));
					neighbours.Add(new Node(new Point2D(position.X - 1, position.Y + 0)));
					neighbours.Add(new Node(new Point2D(position.X - 1, position.Y - 1)));
					neighbours.Add(new Node(new Point2D(position.X + 0, position.Y + 1)));

					neighbours.Add(new Node(new Point2D(position.X + 0, position.Y - 1)));
					neighbours.Add(new Node(new Point2D(position.X + 1, position.Y + 1)));
					neighbours.Add(new Node(new Point2D(position.X + 1, position.Y + 0)));
					neighbours.Add(new Node(new Point2D(position.X + 1, position.Y - 1)));

					int h = 8;
					for (int i = 0; i < h;)
					{
						Point2D p = neighbours[i].position;
						if (p.X < 0 || p.Y < 0 || p.X > 15 | p.Y > 11)
						{
							neighbours.RemoveAt(i);
							h--;
						}
						else if (MapHelper.ColMap[(int) p.Y][(int) p.X] == 1)
						{
							//Console.WriteLine("[" + p.X + ", " + p.Y + "] = " + MapHelper.ColMap[(int)p.Y][(int)p.X]);
							neighbours.RemoveAt(i);
							h--;
						}
						else
							i++;
					}
					//Console.WriteLine("Neigbours of "+position);
	                for (int i = 0; i < h; i++)
	                {
					//	Console.WriteLine("\t"+neighbours[i].position);
	                }
                }

                return neighbours;
            }
        }

		public override bool Equals(object obj)
		{
			//       
			// See the full list of guidelines at
			//   http://go.microsoft.com/fwlink/?LinkID=85237  
			// and also the guidance for operator== at
			//   http://go.microsoft.com/fwlink/?LinkId=85238
			//

			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			Node nodeObj = (Node)obj;
			return position.X == nodeObj.position.X && position.Y == nodeObj.position.Y;

			// TODO: write your implementation of Equals() here		
		}

	}
	// override object.Equals
}
