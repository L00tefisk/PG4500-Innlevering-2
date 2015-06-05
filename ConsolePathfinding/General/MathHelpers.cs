using System;

namespace ConsolePathfinding.General
{
	public static class MathHelpers
	{
		/// <summary>
		/// To clamp the value val to the range [min, max].</summary>
		/// <remarks>
		/// A copy of the source found in this thread: http://stackoverflow.com/questions/2683442/where-can-i-find-the-clamp-function-in-net </remarks>
		public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
		{
			if (val.CompareTo(min) < 0) {
				return min;
			} else if (val.CompareTo(max) > 0) {
				return max;
			} else {
				return val;
			}
		}


		/// <summary>
		/// To compare two doubles without having the "0.333333 != (double)1/3" problem.</summary>
		/// <remarks>
		/// This is a copy of a code snippet found at this url: http://msdn.microsoft.com/en-us/library/ya2zha7s.aspx </remarks>
		public static bool IsCloseTo(this double lhs, double rhs, double tolerance = 0.00001)
		{
			// Define the tolerance for variation in their values. (As a fraction of the lhs value.)
			double difference = Math.Abs(lhs * tolerance);

			// Compare the values. 
			if (Math.Abs(lhs - rhs) <= difference) {
				return true;
			}
			return false;
		}


		/// <summary>
		/// To compare a double to zeroish without the possible offset errors.</summary>
		public static bool IsCloseToZero(this double value, double tolerance = 0.00001)
		{
			return (Math.Abs(value) <= tolerance);
		}

		// normalizes a bearing to between +180 and -180
		public static double normalizeBearing(double angle)
		{
			while (angle > 180) angle -= 360;
			while (angle < -180) angle += 360;
			return angle;
		}
	}
}
