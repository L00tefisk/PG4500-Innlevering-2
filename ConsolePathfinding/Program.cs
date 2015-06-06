using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsolePathfinding.General;

namespace ConsolePathfinding
{
	class Program
	{
		static void Main(string[] args)
		{
			Point2D start = new Point2D(0, 0);

			Node end  = MapHelper.FindPath(start, new Point2D(12, 10));
			if (end != null)
			{
				MapHelper.ColMap[(int)start.Y][(int)start.X] = 2;
				while (end.parent != null)
				{
					MapHelper.ColMap[(int)end.position.Y][(int)end.position.X] = 2;
					Console.Write("[" + end.position.X + ", " + end.position.Y + "] ->");
					end = end.parent;
				}
				Console.WriteLine("");
				Console.Clear();
				for (int y = 0; y < 12; y++)
				{
					for (int x = 0; x < 16; x++)
					{
						Console.Write(MapHelper.ColMap[y][x] + " ");
					}
					Console.Write("\n");
				}
			}
			else
				Console.WriteLine("Failed");

			
			Console.ReadLine();
		}

	}
}
