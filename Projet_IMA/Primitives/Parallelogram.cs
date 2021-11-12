using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
	class Parallelogram : Primitive
	{

		public readonly V3 A; // constant to keep A,B,C consistent with vAB,vAC,... computed at instanciation
		public readonly V3 B;
		public readonly V3 C;

		public readonly V3 vAB;
		public readonly float dAB;
		public readonly V3 vAC;
		public readonly float dAC;
		public readonly V3 vK;
		private readonly V3 _normal;

		public Parallelogram(V3 a, V3 b, V3 c, Material material) : base(material)
		{
			A = a;
			B = b;
			C = c;

			vAB = B - A;
			dAB = vAB.Norm();
			vAC = C - A;
			dAC = vAC.Norm();

			_normal = (vAB ^ vAC).Normalized();

			vK = (vAC ^ _normal) / (vAB ^ vAC).Norm();
		}

		public override V3 GetNormal(V3 _)
		{
			return _normal;
		}

		public override Intersection IsIntersected(Ray ray)
		{
			return Intersection.Compute(ray, this);
		}

		public override V3 GetDu(float u, float v)
		{
			return vAB;
		}

		public override V3 GetDv(float u, float v)
		{
			return vAC;
		}

		public override void NormalizeUV(float u, float v, out float normU, out float normV)
		{
			normU = u;
			normV = v;
		}
	}
}
