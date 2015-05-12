using System;
using PG4500_2015_Innlevering2.General;
using PG4500_2015_Innlevering2.Robocode;
using Robocode;

namespace PG4500_2015_Innlevering2.AI_States.Radar
{
	class DrvRadarSearch : State
	{
		public DrvRadarSearch() : base("Search")
		{

		}

		public override string ProcessState()
		{
			if (Robot.Enemy.Name == null) // We haven't seen the enemy yet
				Robot.SetTurnRadarRight(Rules.RADAR_TURN_RATE);
			else // We lost the enemy, search in the direction of the last known position
			{
				// Get the angle between the radarheading and the robot.
				double angle = MathHelpers.normalizeBearing( -Robot.RadarHeading) + // X and Y are swapped in atan2 because of robocode's weird coordinate system.
						(Math.Atan2(Robot.Enemy.Position.X - Robot.X, Robot.Enemy.Position.Y - Robot.Y) * (180 / 3.1415));

				angle = MathHelpers.normalizeBearing(angle);
				if (angle > -0.001 && angle < 0.001)
					Robot.SetTurnRadarRight(Rules.RADAR_TURN_RATE);
				else 
					Robot.SetTurnRadarRight(angle);
			}
			return null;
		}
	}
}
