using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
	class DirectionalLight : Light
	{
		private V3 _direction;

		public DirectionalLight(V3 direction, Color color) : base(color)
		{
			_direction = direction;
			_direction.Normalize();
		}

		public override V3 GetIncidence(V3 _)
		{
			return _direction;
		}

		public override Ray GetIncidentRay(V3 targetPoint)
		{
			return new Ray(
				new Ray(targetPoint,  - GetIncidence(targetPoint)).GetPoint(Scene.MAX_DISTANCE),
				GetIncidence(targetPoint)
			);
		}

	}
}
