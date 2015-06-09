using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG4500_2015_Innlevering2.General
{
	public class Location
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Location(int x, int y)
		{
			X = x;
			Y = y;
		}

		public bool isEqual(Location l)
		{
			return (X == l.X && Y == l.Y);
		}
	}
}
