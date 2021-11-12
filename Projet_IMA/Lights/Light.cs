using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
	abstract class Light
	{
		protected Color _color;

		protected Light(Color color)
		{
			_color = color;
		}

		public abstract V3 GetIncidence(V3 targetPoint);

		public bool IsLighting(V3 point, Primitive primitive, List<Primitive> primitives)
		{
			Ray lightRay = GetIncidentRay(point);

			Intersection intersection = Intersection.GetNearest(lightRay, primitives);

			if (intersection == null)
			{
				return true;
			}
			else
			{
				return primitive == intersection.Primitive;
			}
		}

		public abstract Ray GetIncidentRay(V3 targetPoint);

		public virtual Color GetColor(V3 _)
		{
			return _color;
		}
	}
}
