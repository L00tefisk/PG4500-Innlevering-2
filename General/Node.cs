using System;

namespace PG4500_2015_Innlevering2.General
{
	class Node
	{
        public readonly Point2D position;

        public Node(Point2D point)
        {
            position.X = point.X;
            position.Y = point.Y;
        }
	}
}
