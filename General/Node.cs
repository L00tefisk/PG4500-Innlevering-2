using System;
using System.Collections.Generic;

namespace PG4500_2015_Innlevering2.General
{
	class Node
	{
        public readonly Point2D position;
        private List<Node> neighbours;
        public Node(Point2D point)
        {
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
					for(int i = 0; i < h;)
					{
						if (neighbours[i].position.X < 0 || neighbours[i].position.Y < 0 || neighbours[i].position.X > 15 | neighbours[i].position.Y > 11)
						{
							neighbours.RemoveAt(i++);
							h--;
						}
						else
							i++;
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
