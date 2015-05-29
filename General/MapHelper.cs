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

		static public List<Node> findPath(Point2D start, Point2D end)
		{
			// create open list
			// create closed list
			// create node from start add to open list
			// create node from end

			// while highest priority in open list isn't the end node
				// Cost = g(current) + movementcost(current, neighbour)
				// 
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
