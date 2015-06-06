using System;
using System.Collections.Generic;

namespace PG4500_2015_Innlevering2.General
{
	public class Node
	{
		public readonly Location position;
		public Node Parent {set; get;}
		public double f { set; get; }
		public double g { set; get; }
		public double h { set; get; }
		public double totalCost { set; get; }
        public Node(int x, int y, double TotalCost)
        {
			position = new Location(x, y);
			f = 0;
			g = 0;
			h = 0;
			totalCost = TotalCost;
        }
		public bool isEqual(Location n)
		{
			return (position.X == n.X && position.Y == n.Y);
		}

	}
	// override object.Equals
}
