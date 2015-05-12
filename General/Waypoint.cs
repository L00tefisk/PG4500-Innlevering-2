using System;
using PG4500_2015_Innlevering2.Robocode;

namespace PG4500_2015_Innlevering2.General
{
	class Waypoint
	{
		protected AdvancedRobotEx Robot;
		public double Distance
		{
			get
			{
				return Math.Sqrt((Destination.X - Robot.X) * (Destination.X - Robot.X) + (Destination.Y - Robot.Y) * (Destination.Y - Robot.Y));
			}
		}
		public double Angle
		{
			get
			{
				double angle = -MathHelpers.normalizeBearing(Robot.Heading) + // X and Y are swapped in atan2 because of robocode's weird coordinate system.
						(Math.Atan2(Destination.X - Robot.X, Destination.Y - Robot.Y) * (180 / 3.1415));
				return MathHelpers.normalizeBearing(angle);
			}
		}
		public Point2D Destination { get; private set; }
		public Waypoint(AdvancedRobotEx robot, Point2D point)
		{
			Destination = point;
			Robot = robot;
		}
	}
}
