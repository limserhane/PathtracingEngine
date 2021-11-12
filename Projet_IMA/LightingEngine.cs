using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Projet_IMA
{
	static class LightingEngine
	{
		public static Color ComputeDiffuseLighting(V3 inDirection, Color inLighting, Color pointColor, V3 normal)
		{
			float diffuseTerm = Math.Maxf(0, V3.Dot(normal, - inDirection));

			return (inLighting * pointColor) * diffuseTerm;
		}

		public static Color ComputeSpecularLighting(V3 inDirection, Color inLighting, V3 outDirection, V3 normal, float shininess)
		{
			V3 R = inDirection.Reflect(normal);
			V3 D = outDirection.Normalized();

			float specularTerm = Math.Powf(Math.Maxf(0, V3.Dot(R, D)), shininess);

			return inLighting * specularTerm;
		}

		static private Color ComputeAmbientLighting(Color lightColor, Color pointColor)
		{
			return lightColor * pointColor;
		}

		static public Color ComputeIndirectLighting(V3 outDirection, Color inLighting, V3 inDirection, Color pointColor, V3 normal, Material material)
		{
			Color diffuse = material.Roughness * ComputeDiffuseLighting(outDirection, inLighting, pointColor, normal);

			Color specular = (1 - material.Roughness) * ComputeSpecularLighting(inDirection, inLighting, outDirection, normal, material.Shininess);

			return diffuse + specular;
		}

		static public Color ComputeDirectLighting(V3 outDirection, Intersection origin, Scene scene)
		{
			if (scene.Lights.Count == 0) return Color.ZERO;

			Color diffuseComponent = Color.ZERO;
			Color specularComponent = Color.ZERO;
			Color ambientComponent = Color.ZERO;

			V3 point = origin.Point;
			Primitive primitive = origin.Primitive;
			float u = origin.U;
			float v = origin.V;

			primitive.NormalizeUV(u, v, out float normalizedU, out float normalizedV);

			Color pointColor = primitive.Material.GetColor(normalizedU, normalizedV);

			V3 normal = primitive.Material.GetBumpedNormal(primitive, point, u, v, normalizedU, normalizedV);

			foreach (Light light in scene.Lights)
			{
				if (!light.IsLighting(point, primitive, scene.Primitives))
				{
					continue;
				}

				V3 lightIncidence = light.GetIncidence(point);
				Color lightColor = light.GetColor(point);

				diffuseComponent += ComputeDiffuseLighting(lightIncidence, lightColor, pointColor, normal);
				specularComponent += (1 - primitive.Material.Roughness) * ComputeSpecularLighting(lightIncidence, lightColor, outDirection, normal, primitive.Material.Shininess);
			}

			if (scene.AmbientLight != null)
			{
				ambientComponent = ComputeAmbientLighting(scene.AmbientLight.Color, pointColor);
			}

			return diffuseComponent + specularComponent + ambientComponent;
		}
	}
}
