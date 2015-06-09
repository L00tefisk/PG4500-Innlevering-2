using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
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
			List<Node> path = new List<Node>(0);
			Node nextNode = new Node(0, 0, 0);
			Location target = new Location(0,0);
			Location player = new Location(0,0);
			Execute();

			while (true)
			{
                if (HasLock)
                {
                    LockBot();
                }
                else
                {
					SearchBot();
                }

                if (TurnCompleted() && DistanceCompleted() && path.Count == 0 && Enemy.Velocity.IsCloseToZero() && HasLock) // we're at the end of the path
                {
                    Location end = MapHelper.ConvertToColMap((int)Enemy.Position.X, (int)Enemy.Position.Y);
                    Location start = MapHelper.ConvertToColMap((int)X, (int)Y);

                    //Out.WriteLine("Finding new path to: " + end.X + " " + end.Y);
                   // Out.WriteLine("From: " + start.X + " " + start.Y);
                    path = MapHelper.AStarSearch2(start, end, this);
                }

				if (path.Count != 0)
				{
					for (int i = 0; i < path.Count;)
					{
						nextNode = path[0];
						target = new Location(nextNode.position.X, nextNode.position.Y);
						player = new Location((int)(X / 50), (int)(600 - (Y / 50)));
						target.X = target.X * 50 + 25;
						target.Y = 550 - (target.Y * 50) + 25;
						while (Seek(target, path)) ;
						path.RemoveAt(0);
					}
					
					for (int i = 0; i < path.Count; i++)
					{
						Color halfTransparent = Color.FromArgb(128, Color.Blue);
						// Draw rectangle at target.
						Graphics.FillRectangle(new SolidBrush(halfTransparent), (int)((50 * nextNode.position.X)), (int)(600 - (50 * (nextNode.position.Y+1))), 50, 50);
						Graphics.FillRectangle(new SolidBrush(halfTransparent), (int)((50 * path[i].position.X)), (int)(600 - (50 * (path[i].position.Y+1))), 50, 50);

					}
				}
				HasLock = false;
				Execute();
			}
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
		    Seek(new Location(25, 25));
		}

        public override void OnScannedRobot(ScannedRobotEvent scanData)
        {
            // Storing data about scan time and Enemy for later use.
            Point2D offset = CalculateTargetVector(HeadingRadians, scanData.BearingRadians, scanData.Distance);
            Point2D position = new Point2D(offset.X + X, offset.Y + Y);
            Enemy.SetEnemyData(scanData, position);
			HasLock = true;

        }
        private Point2D CalculateTargetVector(double ownHeadingRadians, double bearingToTargetRadians, double distance)
        {
            double battlefieldRelativeTargetAngleRadians = Utils.NormalRelativeAngle(ownHeadingRadians + bearingToTargetRadians);
            Point2D targetVector = new Point2D(Math.Sin(battlefieldRelativeTargetAngleRadians) * distance,
                                                 Math.Cos(battlefieldRelativeTargetAngleRadians) * distance);
            return targetVector;
        }

        public void SearchBot()
        {
            if (Enemy.Name == null) // We haven't seen the enemy yet
            {
                SetTurnRadarRight(Rules.RADAR_TURN_RATE);
                
            }
            else // We lost the enemy, search in the direction of the last known position
            {
                Console.WriteLine("LOOP 1");
				SetTurnRadarLeft(Rules.RADAR_TURN_RATE);
				//// Get the angle between the radarheading and the robot.
				//double angle = MathHelpers.normalizeBearing(-RadarHeading) +
				//	// X and Y are swapped in atan2 because of robocode's weird coordinate system.
				//			   (Math.Atan2(Enemy.Position.X - X, Enemy.Position.Y - Y) * (180 / 3.1415));

				//angle = MathHelpers.normalizeBearing(angle);
				
				//if (angle > -0.001 && angle < 0.001)
				//{
                    
				//	Console.WriteLine("LOOP 2");
				//}
				//else
				//{
				//	SetTurnRadarRight(angle);
				//	Console.WriteLine("LOOP 3");
				//}
            }
        }

        public void LockBot()
        {
            Console.WriteLine("LOOP 2");
            
            double radarTurn =
                // Absolute bearing to target
                HeadingRadians + Enemy.BearingRadians
                // Subtract current radar heading to get turn required
            - RadarHeadingRadians;

            SetTurnRadarRightRadians(Utils.NormalRelativeAngle(radarTurn));
            // ...
        }
	}
}
