using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
	struct V3
	{
		public float X;
		public float Y;
		public float Z;

		public float Norm()
		{
			return (float) Math.Sqrtf(X * X + Y * Y + Z * Z);
		}

		public float Norm2()
		{
			return X * X + Y * Y + Z * Z;
		}

		public void Normalize()
		{
			float n = Norm();
			if (n == 0) return;
			X /= n;
			Y /= n;
			Z /= n;
		}
		public V3 Normalized()
		{
			V3 v = this;
			v.Normalize();
			return v;
		}

		public V3(V3 t)
		{
			X = t.X;
			Y = t.Y;
			Z = t.Z;
		}

		public V3(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		V3(int x, int y, int z)
		{
			X = (float)x;
			Y = (float)y;
			Z = (float)z;
		}

		public V3(float k)
		{
			X = k;
			Y = k;
			Z = k;
		}

		public static V3 Zero()
		{
			return new V3(0, 0, 0);
		}

		public static V3 operator +(V3 a, V3 b)
		{
			V3 t;
			t.X = a.X + b.X;
			t.Y = a.Y + b.Y;
			t.Z = a.Z + b.Z;
			return t;
		}

		public static V3 operator -(V3 a, V3 b)
		{
			V3 t;
			t.X = a.X - b.X;
			t.Y = a.Y - b.Y;
			t.Z = a.Z - b.Z;
			return t;
		}

		public static V3 operator -(V3 a)
		{
			V3 t;
			t.X = -a.X;
			t.Y = -a.Y;
			t.Z = -a.Z;
			return t;
		}

		public static V3 operator ^ (V3 a, V3 b)
		{
			V3 t;
			t.X = a.Y * b.Z - a.Z * b.Y;
			t.Y = a.Z * b.X - a.X * b.Z;
			t.Z = a.X * b.Y - a.Y * b.X;
			return t;
		}

		public V3 Reflect(V3 normal)
		{
			return this - 2.0f * V3.Dot(normal, this) * normal;
		}

		public static V3 operator * (V3 a,V3 b)
		{
			V3 v;
			v.X = a.X * b.X;
			v.Y = a.Y * b.Y;
			v.Z = a.Z * b.Z;
			return v;
		}

	   

		public static V3 operator *(float a, V3 b)
		{
			V3 t;
			t.X = b.X*a;
			t.Y = b.Y*a;
			t.Z=  b.Z*a;
			return t;
		}

		public static V3 operator *(V3 b, float a)
		{
			V3 t;
			t.X = b.X*a;
			t.Y = b.Y*a;
			t.Z=  b.Z*a;
			return t;
		}

		public static V3 operator /(V3 b, float a)
		{
			V3 t;
			t.X = b.X/a;
			t.Y = b.Y/a;
			t.Z=  b.Z/a;
			return t;
		}

		public static float Dot(V3 a, V3 b)
		{
			return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
		}

		public override String ToString()
		{
			return String.Format("({0}; {1}; {2})", X, Y, Z);
		}
	}
}
