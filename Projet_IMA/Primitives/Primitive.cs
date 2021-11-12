using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
	abstract class Primitive
	{
		public Material Material { get; set; }

		protected Primitive(Material material)
		{
			this.Material = material;
		}

		public abstract V3 GetNormal(V3 point);
		public abstract V3 GetDu(float u, float v);
		public abstract V3 GetDv(float u, float v);

		public abstract Intersection IsIntersected(Ray ray);

		public abstract void NormalizeUV(float u, float v, out float normU, out float normV);
	}
}
