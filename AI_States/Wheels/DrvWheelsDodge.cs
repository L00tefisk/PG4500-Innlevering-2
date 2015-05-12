using PG4500_2015_Innlevering2.General;
using PG4500_2015_Innlevering2.Robocode;

namespace PG4500_2015_Innlevering2.AI_States.Wheels
{
	class DrvWheelsDodge : State
	{
		public DrvWheelsDodge() : base("Dodge")
		{

		}

		public override void EnterState()
		{
			base.EnterState();
		} 

		public override string ProcessState()
		{
			Waypoint[] waypoints = { new Waypoint(Robot, new Point2D(Robot.Enemy.Position.X + 150, Robot.Enemy.Position.Y + 150)),
									 new Waypoint(Robot, new Point2D(Robot.Enemy.Position.X - 150, Robot.Enemy.Position.Y + 150)),
									 new Waypoint(Robot, new Point2D(Robot.Enemy.Position.X + 150, Robot.Enemy.Position.Y - 150)),
									 new Waypoint(Robot, new Point2D(Robot.Enemy.Position.X - 150, Robot.Enemy.Position.Y - 150)) };
			// Keep track of the best waypoint
			int waypointIndex = 0;

			for (int i = 1; i < 4; i++)
			{
				Vector2D waypointToCenter = new Vector2D(waypoints[i].Destination.X - 400, waypoints[i].Destination.Y - 400);
				Vector2D bestWaypointToCenter = new Vector2D(waypoints[waypointIndex].Destination.X - 400, waypoints[waypointIndex].Destination.Y - 400);

				if (waypointToCenter.Length() < bestWaypointToCenter.Length())
					waypointIndex = i;
			}

			Robot.Flee(waypoints[waypointIndex].Destination);
			return "Idle";
		}
	}
}
