using System.Runtime.CompilerServices;
using PG4500_2015_Innlevering2.General;
using PG4500_2015_Innlevering2.Robocode;

namespace PG4500_2015_Innlevering2.AI_States.Wheels
{
	public class DrvWheelsEngage : State
	{
		private Point2D _targetPosition;


		public DrvWheelsEngage() : base("Engage")
		{
			// Intentionally left blank.
		}


		// Called once when we transition into this state.
		public override void EnterState()
		{

			base.EnterState();
			_targetPosition = Robot.Enemy.Position;		
		}


		public override string ProcessState()
		{
			Point2D p = MapHelper.ConvertFromColMap(8, 8);
			Robot.Seek(p);

			Point2D Rpos = new Point2D(Robot.X, Robot.Y);

			if (Rpos.Distance(p).IsCloseToZero(0.0001))
				return "Idle";
			else
				return "Engage";
		}
	}
}
