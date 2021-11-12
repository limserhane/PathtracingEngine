using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
	static class RaycastEngine
	{

		static public Color Process(int pixelX, int pixelY, Scene scene)
		{
			V3 pixelPosition = new V3(pixelX, 0, pixelY);

			V3 cameraDirection = pixelPosition - scene.CameraPosition;
			Ray camera = new Ray(scene.CameraPosition, cameraDirection);

			Intersection first = Intersection.GetNearest(camera, scene.Primitives);

			Color color = first == null ? 
				scene.BackgroundColor
				:
				LightingEngine.ComputeDirectLighting(-cameraDirection, first, scene); // take account of direct lighting from lights

			return color;
		}

		static public Color ProcessPathTracing(int pixelX, int pixelY, Scene scene, int spp, int diffuseBounces, int specularBounces)
		{
			V3 pixelPosition = new V3(pixelX, 0, pixelY);

			V3 cameraDirection = pixelPosition - scene.CameraPosition;
			Ray camera = new Ray(scene.CameraPosition, cameraDirection);

			Intersection first = Intersection.GetNearest(camera, scene.Primitives);

			if (first == null)
			{
				return scene.BackgroundColor;
			}

			Color result = Color.ZERO;
			for (int i = 0; i < spp; i++)
			{
				result += TracePath(-camera.Direction, first, diffuseBounces, specularBounces, scene);
			}
			result /= spp;

			return result;
		}

		static private Color TracePath(V3 outDirection, Intersection origin, int diffuseBounces, int specularBounces, Scene scene)
		{
			V3 point = origin.Point;
			Primitive primitive = origin.Primitive;
			Material material = origin.Primitive.Material;
			float u = origin.U;
			float v = origin.V;

			origin.Primitive.NormalizeUV(u, v, out float normalizedU, out float normalizedV);

			Color pointColor = material.GetColor(normalizedU, normalizedV);

			Color emitted = material.Emissive * pointColor; // if surface is a emitting light

			V3 normal = material.GetBumpedNormal(primitive, point, u, v, normalizedU, normalizedV);

			V3 inDirection; // point towards "origin"
			if (material.Roughness == 1.0f || Math.RandP(1.0f) < material.Roughness) // probability of a diffuse bounce or a specular bounce depending of the surface roughness
			{
				if (diffuseBounces == 0) // maximum count reached : won't bounce any more
				{
					return emitted; 
				}
				inDirection = -GetRandomDirectionInHemisphere(normal); // diffuse bounce : assume all probabilities are equal = distribution uniforme // origin.Primitive.GetNormal(origin.Point) ?
				diffuseBounces--;
			}
			else
			{
				if (specularBounces == 0) // maximum count reached : won't bounce any more
				{
					return emitted; 
				}
				inDirection = outDirection.Reflect(normal); // specular bounce : assume ideal case, the ray is perfectly reflected = dirac
				specularBounces--;
			}

			Ray nextRay = new Ray(origin.Point, -inDirection);
			nextRay.MoveOrigin(0.1f); // so that ray starts outside of the suface - adjust if objects are close to eachother

			Intersection next = Intersection.GetNearest(nextRay, scene.Primitives);

			if (next == null) return emitted; // bounce doesn't hit

			Color inLighting = TracePath(inDirection, next, diffuseBounces, specularBounces, scene);

			Color indirect = LightingEngine.ComputeIndirectLighting(inDirection, inLighting, outDirection, pointColor, normal, material);

			return emitted + indirect; // rendering equation
		}

		static public V3 GetRandomDirectionInSphere()
		{
			float theta = 2 * Math.PI * Math.RandP(1.0f);
			float phi = Math.Acosf(Math.RandNP(1.0f));

			V3 random = new V3(
				Math.Cosf(theta) * Math.Sinf(phi),
				Math.Sinf(theta) * Math.Sinf(phi),
				Math.Cosf(phi)
			);

			return random;
		}
		static public V3 GetRandomDirectionInHemisphere(V3 top)
		{
			V3 random = GetRandomDirectionInSphere();

			if (V3.Dot(random, top) < 0) // vector must be point the same direction as the normal
				random = -random;

			return random;
		}
	}
}