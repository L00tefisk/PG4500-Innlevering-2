using System;
using System.Drawing;
using PG4500_2015_Innlevering2.General;
using Robocode;

//TODO: BYTT NAMESPACE NAVN
namespace PG4500_2015_Innlevering2.Robocode
{
	/// <summary>
	/// Extended AdvancedRobot functionality for PG4500 robots.
	/// version: 1.0
	/// author: Tomas Sandnes - santom@westerdals.no
	/// </summary>
	public class AdvancedRobotEx : AdvancedRobot 
	{
		// NOTE: The ORDER that Robocode runs is as follows: 
		//		 Firing PITFALL: Gun fires before it turns!
		//       (Source: http://robocode.sourceforge.net/help/physics/physics.html)
		//       1. Battle view is (re)painted. 
		//		 2. All robots Execute their code until they take action (and then paused). 
		//		 3. Time is updated (time = time + 1). 
		//		 4. All bullets move and check for collisions. This includes firing bullets. 
		//		 5. All robots move (gun, radar, heading, acceleration, velocity, distance, in that order). 
		//		 6. All robots perform scans (and collect team messages). 
		//		 7. All robots are resumed to take new action. 
		//		 8. Each robot processes its event queue.


		// P R O P E R T I E S 
		// -------------------

		public EnemyData Enemy { get; set; }  // Stored info about our current radar target. (Wiped each round/match.)
		public double FireThreshold { get; set; } //Maximum distance the enemy can have before our robot stops trying to shoot it
		public bool HasLock { get; set; }


		// P U B L I C   M E T H O D S 
		// ---------------------------

		public AdvancedRobotEx()
		{
			Enemy = new EnemyData();
		}


		public bool DistanceCompleted()
		{
			return Math.Abs(DistanceRemaining).IsCloseToZero();
		}


		public bool TurnCompleted()
		{
			return Math.Abs(TurnRemaining).IsCloseToZero();
		}

		public void Seek(Point2D target)
		{

			Node destination = new Node(this, target);
			SetTurnRight(destination.Angle);
			DrawLineAndTarget(Color.Red, new Point2D(X, Y), target);
			
			SetAhead(destination.Distance);
			Console.WriteLine("Distance = " + destination.Distance);
		}

		public void Flee(Point2D target)
		{
			Node destination = new Node(this, target);
			DrawLineAndTarget(Color.Red, new Point2D(X, Y), target);
			SetTurnRight(destination.Angle);
			if (TurnCompleted())
				SetAhead(-destination.Distance);

		}


		/// <summary>
		/// Method to draw half-transparent targeting-line (from start to end) & targeting-box (the size of a robot) 
		/// on the battlefield. The idea is to use this for visual debugging: Set start point to own robot's position, 
		/// and end point to where you mean the bullet to go. Then see if this really is where the bullet is heading: 
		/// 1) If the targeting-box is off the spot you wanted it, you got a bug in your target prediction code.
		/// 2) If the targeting-box is on the spot, but the bullet is off the line (and center of the box), you 
		///    got a bug in your "gun turning and firing" code.
		/// </summary>
		public void DrawLineAndTarget(Color drawColor, Point2D start, Point2D end)
		{
			// Set color to a semi-transparent one.
			Color halfTransparent = Color.FromArgb(128, drawColor);
			// Draw line and rectangle.
			Graphics.DrawLine(new Pen(halfTransparent), (int)start.X, (int)start.Y, (int)end.X, (int)end.Y);
			Graphics.FillRectangle(new SolidBrush(halfTransparent), (int)(end.X - 17.5), (int)(end.Y - 17.5), 36, 36);
		}


		/// <summary>
		/// Method to draw half-transparent robot indicator box (size somewhat bigger than a robot) covering enemy.
		/// </summary>
		public void DrawRobotIndicator(Color drawColor, Point2D target)
		{
			// Set color to a semi-transparent one.
			Color halfTransparent = Color.FromArgb(128, drawColor);
			// Draw rectangle at target.
			Graphics.FillRectangle(new SolidBrush(halfTransparent), (int)(target.X - 26.5), (int)(target.Y - 26.5), 54, 54);
		}


		/// <summary>
		/// If current enemy gets killed, clear the EnemyData variable.
		/// </summary>
		public override void OnRobotDeath(RobotDeathEvent deadRobot)
		{
			if (deadRobot.Name == Enemy.Name) {
				Enemy.Clear();
			}
		}


		/// <summary>
		/// If one of our bullets just hit our currently tracked target, update the stored data (Energy property) of our target.
		/// </summary>
		public override void OnBulletHit(BulletHitEvent hitData)
		{
			if (Enemy.Name == hitData.VictimName) {
				Enemy.Energy = hitData.VictimEnergy;
			}
		}
	}
}
