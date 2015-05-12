using PG4500_2015_Innlevering2.Robocode;
using Robocode.Util;

namespace PG4500_2015_Innlevering2.AI_States.Radar
{
	class DrvRadarLock : State
	{
		public DrvRadarLock() : base("Lock")
		{

		}



		public override string ProcessState()
		{
			double radarTurn =
				// Absolute bearing to target
				Robot.HeadingRadians + Robot.Enemy.BearingRadians
				// Subtract current radar heading to get turn required
			- Robot.RadarHeadingRadians;

			Robot.SetTurnRadarRightRadians(Utils.NormalRelativeAngle(radarTurn));
			// ...
			if (Robot.HasLock)
				return "Lock";
			else
				return "Search";
		}
	}
}
