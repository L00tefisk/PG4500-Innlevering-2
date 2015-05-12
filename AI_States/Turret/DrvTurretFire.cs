using PG4500_2015_Innlevering2.Robocode;

namespace PG4500_2015_Innlevering2.AI_States.Turret
{
	class DrvTurretFire : State
	{
		public DrvTurretFire() : base("Fire")
		{

		}

		public override void EnterState()
		{
			base.EnterState();
			Robot.SetFireBullet(3);
		}

		public override string ProcessState()
		{
			return "Idle";
		}
	}
}
