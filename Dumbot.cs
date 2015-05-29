using System;
using System.Drawing;
using PG4500_2015_Innlevering2.AI_States.Radar;
using PG4500_2015_Innlevering2.AI_States.Wheels;
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
		// P R I V A T E / P R O T E C T E D   V A R S
		// -------------------------------------------

		private readonly FiniteStateMachine _radarFSM;
		private readonly FiniteStateMachine _wheelsFSM;

		private bool hasEnemyFired = false;

		// P U B L I C   M E T H O D S 
		// ---------------------------

		public klemag_hyleiv_DumBot()
		{
			FireThreshold = 1000;
			// Defining the possible states for this fsm. (Also, the 1st one listed becomes the default state.)
			_radarFSM = new FiniteStateMachine(new State[] {new DrvRadarSearch(), new DrvRadarLock()});
			_wheelsFSM = new FiniteStateMachine(new State[] {new DrvWheelsIdle(), new DrvWheelsEngage()});
		}


		public override void Run()
		{
			InitBot();

			// Loop forever. (Exiting run means no more robot fun for us!)
			while (true)
			{

				// The state machine doing its "magic".
				if (hasEnemyFired && DistanceCompleted())
					hasEnemyFired = false;
				if (Enemy.PreviousEnergy - Enemy.Energy >= Rules.MIN_BULLET_POWER)
					hasEnemyFired = true;

				_radarFSM.Update();
				_wheelsFSM.Update();


				// Execute any current actions. NOTE: This sometimes triggers a blocking call internally, so this should be the last thing we do in a turn!
				HasLock = false;

				Execute();

			}
			// ReSharper disable once FunctionNeverReturns
		}


		public override void OnScannedRobot(ScannedRobotEvent scanData)
		{
			// Storing data about scan time and Enemy for later use.
			Vector2D offset = CalculateTargetVector(HeadingRadians, scanData.BearingRadians, scanData.Distance);
			Point2D position = new Point2D(offset.X + X, offset.Y + Y);
			Enemy.SetEnemyData(scanData, position);

			// If we're out of energy, don't bother swapping states, as that will just make runtime bugs.
			if (!Energy.IsCloseToZero())
			{
				if (!HasLock)
				{
					
				}
				_radarFSM.Queue("Lock");
			}
			HasLock = true;
		}


		// P R I V A T E   M E T H O D S
		// -----------------------------

		// Inits robot stuff (color and such).
		private void InitBot()
		{
			// Init the FSM.
			_radarFSM.Init(this);
			//_turretFSM.Init(this);
			_wheelsFSM.Init(this);

			// Set some colors on our robot. (Body, gun, radar, bullet, and scan arc.)
			SetColors(
				Color.DarkRed, //Body
				Color.Black, //Gun
				Color.OrangeRed, //Radar
				Color.OrangeRed, //Bullet
				Color.Red //Scan arc
				);
			// NOTE: Total distance each element can move remains the same, whether these ones are true or false. 
			//       Example: Gun swivels a maximum of 20 degrees in addition to what the body swivels (if anything) 
			//       each turn, no matter what IsAdjustGunForRobotTurn is set to.
			IsAdjustGunForRobotTurn = true;
			IsAdjustRadarForGunTurn = true;
			_wheelsFSM.Queue("Engage");
			_radarFSM.Queue("Search");
		}


		/// <summary>
		/// Method to find Vector2D from Robot to Target, according to the battlefield coordinate system.
		/// </summary>
		private Vector2D CalculateTargetVector(double ownHeadingRadians, double bearingToTargetRadians, double distance)
		{
			double battlefieldRelativeTargetAngleRadians = Utils.NormalRelativeAngle(ownHeadingRadians + bearingToTargetRadians);
			Vector2D targetVector = new Vector2D(Math.Sin(battlefieldRelativeTargetAngleRadians)*distance,
				Math.Cos(battlefieldRelativeTargetAngleRadians)*distance);
			return targetVector;
		}
	}
}
