using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
	class Sphere : Primitive
	{
		public V3 Center { get; set; }
		public float Radius { get; set; }


		public Sphere(V3 center, float radius, Material material) : base(material)
		{
			this.Center = center;
			this.Radius = radius;
		}

		public override V3 GetNormal(V3 point)
		{
			return (point - Center).Normalized();
		}

		public override Intersection IsIntersected(Ray ray)
		{
			return Intersection.Compute(ray, this);
		}

		public override V3 GetDu(float u, float v)
		{
			return Radius * new V3(Math.Cosf(v) * -Math.Sinf(u), Math.Cosf(v) * Math.Cosf(u), 0);
		}

		public override V3 GetDv(float u, float v)
		{
			return Radius * new V3(-Math.Sinf(v) * Math.Cosf(u), -Math.Sinf(v) * Math.Sinf(u), Math.Cosf(v));
		}

		public override void NormalizeUV(float u, float v, out float normalizedU, out float normalizedV)
		{
			normalizedU = u / Math.DPI;
			normalizedV = (v + Math.PI2) / Math.PI;
		}
	}
}
