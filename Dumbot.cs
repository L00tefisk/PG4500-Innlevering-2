using System;
using System.Collections.Generic;
using System.Drawing;
using PG4500_2015_Innlevering2.General;
using PG4500_2015_Innlevering2.Robocode;
using Robocode;
using Robocode.Util;

// ReSharper disable once CheckNamespace
namespace PG4500_2015_Innlevering2
{
	// ReSharper disable once InconsistentNaming
	public class klemag_hyleiv_PathfinderBot : AdvancedRobotEx
	{
     
		public klemag_hyleiv_PathfinderBot()
		{

		}


		public override void Run()
		{
			InitBot();
			Out.WriteLine("Distance Completed? " + DistanceCompleted());

			int index = 0;
			Node[] pathToFollow = new Node[0];
			Dictionary<Node, Node> path = new Dictionary<Node, Node>();
			// Loop forever. (Exiting run means no more robot fun for us!)
			while (true)
			{
              if(DistanceCompleted() && pathToFollow.Length == 0) // we're at the end of the path
			  {
				  Point2D end = MapHelper.getRandomPosition();
				  Point2D start = MapHelper.ConvertToColMap((int)X, (int)Y);
				  Out.WriteLine("Finding new path to: " + end.X + " " + end.Y);
				  Out.WriteLine("From: " + start.X + " " + start.Y);
                  path = MapHelper.findPath(start, end, this);
				  index = 0;
				  pathToFollow = new Node[path.Values.Count];
				  path.Values.CopyTo(pathToFollow, 0);
			  }
              else if(DistanceCompleted()) // we're standing still, but there are more paths to go to
              {
				  Out.WriteLine("Following Path");
				  Node n = pathToFollow[index];
				  Point2D nextPos = MapHelper.ConvertToColMap((int)n.position.X, (int)n.position.Y);
                  Seek(nextPos);
                  index++;
              }

			  Execute();
			}
		}


		public override void OnScannedRobot(ScannedRobotEvent scanData)
		{
			// Storing data about scan time and Enemy for later use.
		//	Vector2D offset = CalculateTargetVector(HeadingRadians, scanData.BearingRadians, scanData.Distance);
			//Point2D position = new Point2D(offset.X + X, offset.Y + Y);
			//Enemy.SetEnemyData(scanData, position);

		}

		// Inits robot stuff (color and such).
		private void InitBot()
		{
			// Set some colors on our robot. (Body, gun, radar, bullet, and scan arc.)
			SetColors(
				Color.DarkRed, //Body
				Color.Black, //Gun
				Color.OrangeRed, //Radar
				Color.OrangeRed, //Bullet
				Color.Red //Scan arc
				);
		}
	}
}
