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
	public class klemag_hyleiv_DumBot : AdvancedRobotEx
	{
        private List<Node> path;
     
		public klemag_hyleiv_DumBot()
		{

		}


		public override void Run()
		{
			InitBot();
            
			// Loop forever. (Exiting run means no more robot fun for us!)
			while (true)
			{


              // if(DistanceCompleted() && path.Count == 0) // we're at the end of the path
              //    findPath(position, Enemy.Position);
              // else if(DistanceCompleted()) // we're standing still, but there are more paths to go to
              // {
              //    Seek(path[0].position);
              //    path.removeAt(0);
              //  }
              //  
			}
		}


		public override void OnScannedRobot(ScannedRobotEvent scanData)
		{
			// Storing data about scan time and Enemy for later use.
			Vector2D offset = CalculateTargetVector(HeadingRadians, scanData.BearingRadians, scanData.Distance);
			Point2D position = new Point2D(offset.X + X, offset.Y + Y);
			Enemy.SetEnemyData(scanData, position);

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
