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
                    for(int y = -1; y < 2; y++)
                    {
                        for(int x = -1; x < 2; x++)
                        {
                            if(x != 0 && y != 0)
                                neighbours.Add(new Node(new Point2D(x, y)));
                        }
                    }
                }

                return neighbours;
            }
        }

	}
}
