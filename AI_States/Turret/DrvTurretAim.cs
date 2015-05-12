using System;
using System.Drawing;
using PG4500_2015_Innlevering2.General;
using PG4500_2015_Innlevering2.Robocode;
using Robocode;

namespace PG4500_2015_Innlevering2.AI_States.Turret
{
	class DrvTurretAim : State
	{
		bool IsGunTurning = false;
		public DrvTurretAim() : base("Aim")
		{

		}

		private Vector2D Constrain(Vector2D v)
		{
			v.X = MathHelpers.Clamp(v.X, 0, Robot.BattleFieldWidth);
			v.Y = MathHelpers.Clamp(v.Y, 0, Robot.BattleFieldHeight);
			return v;
		}

		readonly double _bVelocity = 20 - 3 * Rules.MAX_BULLET_POWER;
		double _bulletDistance = 0;

		private Vector2D PredictPos(EnemyData e, int t)
		{
			int n;
			Vector2D target = new Vector2D(e.Position.X, e.Position.Y);
			for (n = 0; n <= t; n++)
			{
				target.X += e.Velocity*Math.Sin(e.HeadingRadians + e.TurnRateRadians * n);
				target.Y += e.Velocity*Math.Cos(e.HeadingRadians + e.TurnRateRadians * n);
				Constrain(target);
				_bulletDistance = _bVelocity * n;
				if (_bulletDistance > e.Distance)
					break;
			}
			return target;
			
		}
		private double CalculateBearing(Vector2D target)
		{
			double targetRotation = Math.Atan2(target.X - Robot.X, target.Y - Robot.Y) * (180 / Math.PI);

			
			if (targetRotation > 0)
				return (MathHelpers.normalizeBearing(targetRotation - Robot.GunHeading));
			else
				return (MathHelpers.normalizeBearing(targetRotation - Robot.GunHeading + 360));
			
		}
		private double CalculateFirePower()
		{
			if (Robot.Enemy.Distance > Robot.FireThreshold)
				return -1;
			else
				return (Robot.Enemy.Distance / _bulletDistance) * Rules.MAX_BULLET_POWER;
		}
		double bulletPower = 0;
		long fireTime = 0;
		private Vector2D _target;
		public override string ProcessState()
		{

			if (fireTime == Robot.Time &&
			    MathHelpers.IsCloseToZero(Robot.GunTurnRemaining) &&
			    bulletPower > 0 &&
			    MathHelpers.IsCloseToZero(Robot.GunHeat))
			{
				Robot.SetFire(bulletPower);
				Robot.Out.WriteLine("Firing with bullet power " + Math.Round(bulletPower*1000)/1000);
			}
			_target = PredictPos(Robot.Enemy, 100);
			Robot.SetTurnGunRight(CalculateBearing(_target));
			bulletPower = CalculateFirePower();
			fireTime = Robot.Time + 1;
			Robot.DrawLineAndTarget(Color.Gold, new Point2D(Robot.Enemy.Position), new Point2D(_target));
			return null;
			/*if (!IsGunTurning)
			{
				Robot.SetTurnGunRight(MathHelpers.normalizeBearing(calculateBearing(Robot.Enemy, 40)));
				IsGunTurning = true;
			}
			else if (Robot.GunTurnRemaining == 0.0)
				IsGunTurning = false;
			
			if (Robot.HasLock && Robot.GunHeat == 0.0 && !IsGunTurning) // and if the barrel is angeled correctly
				return "Fire";
			else if (Robot.HasLock)
				return "Aim";
			else
				return "Idle";*/
		}
	}
}
