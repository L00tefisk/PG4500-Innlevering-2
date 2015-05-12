using PG4500_2015_Innlevering2.General;
using PG4500_2015_Innlevering2.Robocode;

namespace PG4500_2015_Innlevering2.AI_States.Turret
{
	class DrvTurrentIdle : State
	{
		public DrvTurrentIdle() : base("Idle")
		{

		}

		public override void EnterState()
		{
			base.EnterState();
		}
		public override string ProcessState()
		{
			Robot.SetTurnGunRight(MathHelpers.normalizeBearing(Robot.RadarHeading - Robot.GunHeading));
			return null;
		}

	}
}
