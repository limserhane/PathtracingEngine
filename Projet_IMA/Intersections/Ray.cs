using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
	struct Ray
	{
		public V3 Origin;
		public V3 Direction;

		public Ray(V3 origin, V3 direction)
		{
			this.Origin = origin;

			this.Direction = direction;
			this.Direction.Normalize();
		}

		public V3 GetPoint(float t)
		{
			return Origin + t * Direction;
		}

		public void MoveOrigin(float t)
		{
			Origin = GetPoint(t);
		}

		static public Ray operator-(Ray ray)
		{
			return new Ray(ray.Origin, -ray.Direction);
		}
	}
}
