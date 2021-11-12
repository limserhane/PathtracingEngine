using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
	class Intersection
	{
		public readonly Primitive Primitive;
		public readonly V3 Point;
		public readonly float U;
		public readonly float V;

		public Intersection(Primitive primitive, V3 point, float u, float v)
		{
			Primitive = primitive;
			Point = point;
			U = u;
			V = v;
		}

		public static Intersection Compute(Ray ray, Sphere sphere)
		{
			float a = ray.Direction.Norm2();
			float b = 2 * V3.Dot(ray.Direction, ray.Origin - sphere.Center);
			float d = (ray.Origin - sphere.Center).Norm2() - sphere.Radius * sphere.Radius;

			float discriminant = b * b - 4 * a * d;

			if (discriminant < 0)
			{
				return null;
			}

			discriminant = Math.Sqrtf(discriminant);

			float t1 = (-b - discriminant) / (2 * a);
			float t2 = (-b + discriminant) / (2 * a);

			if (t1 >= 0 && t2 >= 0)
			{
				V3 point = ray.GetPoint(t1);
				Math.InvertSphericalCoordinates(point - sphere.Center, sphere.Radius, out float u, out float v);
				return new Intersection(sphere, point, u, v);
			}

			else if (t1 < 0 && t2 >= 0)
			{
				V3 point = ray.GetPoint(t2);
				Math.InvertSphericalCoordinates(point - sphere.Center, sphere.Radius, out float u, out float v);
				return new Intersection(sphere, point, u, v);
			}

			else
			{
				return null;
			}
		}

		public static Intersection Compute(Ray ray, Parallelogram parallelogram)
		{
			V3 n = parallelogram.GetNormal(parallelogram.A);

			float t = V3.Dot(parallelogram.A - ray.Origin, n) / V3.Dot(ray.Direction, n);

			if (t < 0)
			{
				return null;
			}

			V3 I = ray.GetPoint(t);

			V3 vAI = I - parallelogram.A;

			float u = V3.Dot(parallelogram.vK, vAI);
			float v = (V3.Dot(vAI, parallelogram.vAC) - u * V3.Dot(parallelogram.vAB, parallelogram.vAC)) / parallelogram.vAC.Norm2(); // Helder - simon

			if (0 <= u && u <= 1 && 0 <= v && v <= 1)
			{
				return new Intersection(parallelogram, I, u, v);
			}
			else
			{
				return null;
			}
		}

		public static Intersection Compute(Ray ray, Triangle triangle)
		{
			Intersection intersection = Compute(ray, (Parallelogram)triangle);

			if(intersection == null)
			{
				return null;
			}

			if(intersection.U + intersection.V > 1)
			{
				return null;
			}

			return intersection;
		}

			public static Intersection GetNearest(Ray ray, List<Primitive> primitives)
		{
			Intersection nearestIntersection = new Intersection(null, ray.GetPoint(Scene.MAX_DISTANCE), 0.0f, 0.0f);

			foreach (Primitive primitive in primitives)
			{
				Intersection currentIntersection = primitive.IsIntersected(ray);

				if (currentIntersection != null && (ray.Origin - currentIntersection.Point).Norm2() < (ray.Origin - nearestIntersection.Point).Norm2()) // norm2 faster than norm for the same comparison result
				{
					nearestIntersection = currentIntersection;
				}
			}

			return nearestIntersection.Primitive == null ? null : nearestIntersection;
		}
	}
}
