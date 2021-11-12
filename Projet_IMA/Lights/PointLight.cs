using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
	class PointLight : Light
	{
		private V3 _position;
		private float _attenuation;

		public PointLight(V3 position, Color color,float attenuation=0.0f) : base(color)
		{
			_position = position;
			_attenuation = attenuation;
		}

		public V3 GetPosition()
		{
			return _position;
		}

		public override V3 GetIncidence(V3 targetPoint)
		{
			return (targetPoint - _position).Normalized();
		}

		public override Ray GetIncidentRay(V3 targetPoint)
		{
			return new Ray(_position, GetIncidence(targetPoint));
		}

		public override Color GetColor(V3 point)
		{
			float distance2 = (point - _position).Norm2();
			return _color / (1 + _attenuation * distance2);
		}

	}
}
