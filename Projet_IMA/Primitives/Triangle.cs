using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
	class Triangle : Parallelogram
	{
		public Triangle(V3 a, V3 b, V3 c, Material material) : base(a, b, c, material)
		{ }

		public override Intersection IsIntersected(Ray ray)
		{
			return Intersection.Compute(ray, this);
		}
	}
}
