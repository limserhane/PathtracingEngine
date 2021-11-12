using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
	class Math
	{
		static public float DPI = (float) (System.Math.PI * 2);
		static public float PI  = (float) (System.Math.PI);
		static public float PI2 = (float) (System.Math.PI / 2);
		static public float PI4 = (float) (System.Math.PI / 4);

		static public float Absf(float d) { return System.Math.Abs(d); }
		static public float Maxf(float a, float b) { return System.Math.Max(a, b); }
		static public float Powf(float n, float k) { return (float)System.Math.Pow(n, k); }
		static public float Cosf(float theta) { return (float)System.Math.Cos(theta); }
		static public float Acosf(float d) { return (float)System.Math.Acos(d); }
		static public float Sinf(float theta) { return (float)System.Math.Sin(theta); }
		static public float Sqrtf(float v)	{ return (float)System.Math.Sqrt(v); }

		static public  void InvertSphericalCoordinates(V3 P, float r, out float u, out float v)
		{
			P = P / r;
			if (P.Z >= 1) { u =(float) Math.PI2 ; v = 0; }
			else if (P.Z <= -1) { u = (float)-Math.PI2 ; v = 0; }
			else
			{
				v = (float)System.Math.Asin(P.Z);
				float t = (float) (P.X / Math.Cosf(v));
				if (t <= -1) { u = (float) Math.PI; }
				else if (t >= 1) { u = 0; }
				else
				{
					if (P.Y < 0) u = (float) ( 2 * Math.PI - System.Math.Acos(t));
					else u = (float)System.Math.Acos(t);
				}
			}
		}
		static public float Parsef(string input)
		{
			return float.Parse(input, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
		}

		// ------------------------------------ Random ------------------------------------

		[ThreadStatic] static public Random Ran;

		static public void InitRand() { Ran = new Random(); }
		static public float RandNP(float v) {return ((float)Ran.NextDouble() - 0.5f) * 2 * v; }
		static public float RandP(float v) { return ((float)Ran.NextDouble()) * v; }

	}
}
