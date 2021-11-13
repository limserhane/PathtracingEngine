using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
	struct Scene
	{
		public const float MAX_DISTANCE = 10000;
		public List<Primitive> Primitives { get; set; }
		public V3 CameraPosition { get; set; }
		public List<Light> Lights { get; set; }
		public AmbientLight AmbientLight { get; set; }
		public Color BackgroundColor { get; set; }

		public Scene(V3 cameraPosition)
		{
			CameraPosition = cameraPosition;

			Primitives = new List<Primitive>();

			Lights = new List<Light>();

			AmbientLight = null;

			BackgroundColor = Color.BLACK;
		}

		public static Scene GetSceneByIndex(int index, int width, int height, bool usePathtracing)
		{
			switch (index)
			{
				case 0: return GetSpaceScene(width, height, usePathtracing);
				case 1: return GetCornellScene(width, height, usePathtracing);
				case 2: return GetDeerScene(width, height, usePathtracing);
				default: return GetCornellScene(width, height, usePathtracing);
			}
		}

		public static Scene GetDeerScene(int width, int height, bool usePathtracing)
		{
			int depth = (int)(0.5f * width);

			V3 room = new V3(width, depth, height);

			// Camera
			V3 cameraPosition = new V3(width / 2, -width, 1.5f * height / 2);

			Scene scene = new Scene(cameraPosition)
			{
				BackgroundColor = Color.DARK_GREY
			};

			#region Walls

			float left = 0.1f;
			float right = 0.9f;

			float bottom = 0.0f;
			float top = 1.0f;

			float front = - 2 * width / depth - 1 ;
			float back = 1.0f;

			V3 a = new V3(left, front, top) * room;
			V3 b = new V3(right, front, top) * room;
			V3 c = new V3(right, front, bottom) * room;
			V3 d = new V3(left, front, bottom) * room;

			V3 e = new V3(left, back, top) * room;
			V3 f = new V3(right, back, top) * room;
			V3 g = new V3(right, back, bottom) * room;
			V3 h = new V3(left, back, bottom) * room;

			Primitive leftWall = new Parallelogram(d, h, a, new Material() { Albedo = Color.PINK, });
			scene.Primitives.Add(leftWall);

			Primitive rightWall = new Parallelogram(g, c, f, new Material() { Albedo = Color.MAROON, });
			scene.Primitives.Add(rightWall);

			Primitive topWall = new Parallelogram(e, f, a, new Material() { Albedo = Color.WHITE, });
			scene.Primitives.Add(topWall);

			Primitive bottomWall = new Parallelogram(d, c, h, new Material() { Albedo = Color.WHITE });
			scene.Primitives.Add(bottomWall);

			Primitive backWall = new Parallelogram(h, g, e, new Material() { Albedo = Color.WHITE});
			scene.Primitives.Add(backWall);

			Primitive frontWall = new Parallelogram(c, d, b, new Material() { Albedo = Color.WHITE, });
			scene.Primitives.Add(frontWall);

			#endregion

			#region Lights 

			if(usePathtracing)
			{
				scene.Primitives.Add(new Sphere(
					new V3(0.5f * width, 0.0f * depth, height + 7133),
					7140,
					new Material() { Albedo = Color.WHITE, Emissive = 5.0f }
				));
			}
			else
			{
				scene.Lights.Add(new PointLight(
					new V3(0.5f, -0.5f, 0.9f) * room,
					0.7f * Color.WHITE
				));
			}

			#endregion

			scene.Primitives.AddRange(Mesh.LoadMesh("deerlow.obj", new V3(0.5f, 0.3f, 0.0f) * room, 11.0f, new Material() { Roughness = 1.0f }));

			scene.Primitives.Add(new Sphere(
				new V3(0.2f, -1.2f, 0.4f) * room,
				136,
				new Material() { Texture = new Texture("gold.jpg"), Bumpmap = new Texture("gold_bump.jpg"), Relief = 2.0f}
			));

			scene.Primitives.Add(new Sphere(
				new V3(0.8f, 0.8f, 0.85f) * room,
				100,
				new Material() { Roughness = 0.0f }
			));

			float paintScale = 0.7f;
			float paintWidth = 650.0f;
			float paintHeight = 448.0f;
			V3 paintPosition = new V3(0.15f, 0.9999f * back, 0.25f) * room;

			scene.Primitives.Add(new Parallelogram(
				new V3(paintPosition.X, paintPosition.Y, paintPosition.Z),
				new V3(paintPosition.X + paintScale * paintWidth, paintPosition.Y, paintPosition.Z),
				new V3(paintPosition.X, paintPosition.Y, paintPosition.Z + paintScale * paintHeight),
				new Material() { Texture = new Texture("kanagawa.jpg") }
			));

			return scene;
		}

		public static Scene GetSpaceScene(int width, int height, bool usePathtracing)
		{
			int depth = 2 * width;

			V3 room = new V3(width, depth, height);

			// Camera
			V3 cameraPosition = new V3(width / 2, -width, 0.75f * height);

			Scene scene = new Scene(cameraPosition)
			{
				BackgroundColor = Color.BLACK
			};

			#region Walls

			float left = -3.0f;
			float right = 4.0f;

			float bottom = 0.0f;
			float top = 2.0f;

			float front = -1.0f - 1;
			float back = 2.0f;

			V3 a = new V3(left, front, top) * room;
			V3 b = new V3(right, front, top) * room;
			V3 c = new V3(right, front, bottom) * room;
			V3 d = new V3(left, front, bottom) * room;

			V3 e = new V3(left, back, top) * room;
			V3 f = new V3(right, back, top) * room;
			V3 g = new V3(right, back, bottom) * room;
			V3 h = new V3(left, back, bottom) * room;

			Primitive leftWall = new Parallelogram(d, h, a, new Material() { Albedo = Color.WHITE });
			scene.Primitives.Add(leftWall);

			Primitive rightWall = new Parallelogram(g, c, f, new Material() { Albedo = Color.WHITE, });
			scene.Primitives.Add(rightWall);

			Primitive bottomWall = new Parallelogram(d, c, h, new Material() { Albedo = Color.WHITE, });
			scene.Primitives.Add(bottomWall);

			Primitive backWall = new Parallelogram(h, g, e, new Material() { Texture = new Texture("milkyway.jpg") });
			scene.Primitives.Add(backWall);

			Primitive frontWall = new Parallelogram(c, d, b, new Material() { Albedo = Color.WHITE, });
			scene.Primitives.Add(frontWall);

			#endregion

			#region Lights 

			if(usePathtracing)
			{
				scene.Primitives.Add(new Sphere(
					new V3(-1.0f, 0.0f, 3.2f) * room,
					680,
					new Material() { Emissive = 20.0f }
				));

				scene.Primitives.Add(new Sphere(
					new V3(2.0f, 0.0f, 3.2f) * room,
					340,
					new Material() { Emissive = 15.0f, Texture = new Texture("planets\\sun.jpg") }
				));
			}
			else
			{
				scene.Lights.Add(new PointLight(
					new V3(-1.0f, 0.0f, 3.2f) * room,
					0.9f * Color.WHITE
				));
				scene.Lights.Add(new PointLight(
					new V3(2.0f, 0.0f, 3.2f) * room,
					0.1f * Color.ORANGE
				));
			}

			#endregion

			#region Spheres

			float sphereSize = 220;

			scene.Primitives.Add(new Sphere(
				new V3(-0.1f * width, 0.5f * depth, sphereSize),
				sphereSize,
				new Material() { Roughness = 1.0f }
			));

			scene.Primitives.Add(new Sphere(
				new V3(0.5f * width, 0.5f * depth, sphereSize),
				sphereSize,
				new Material() { Roughness = 0.90f }
			));

			scene.Primitives.Add(new Sphere(
				new V3(1.1f * width, 0.5f * depth, sphereSize),
				sphereSize,
				new Material() { Roughness = 0.0f }
			));

			float ballSize = 100;

			scene.Primitives.Add(new Sphere(
				new V3(-0.1f * width, 0.5f * depth - sphereSize - ballSize, ballSize),
				ballSize,
				new Material()
				{
					Albedo = Color.RED,
				}
			));

			scene.Primitives.Add(new Sphere(
				new V3(0.5f * width, 0.5f * depth - sphereSize - ballSize, ballSize),
				ballSize,
				new Material()
				{
					Albedo = Color.BLUE,
				}
			));

			scene.Primitives.Add(new Sphere(
				new V3(1.1f * width, 0.5f * depth - sphereSize - ballSize, ballSize),
				ballSize,
				new Material()
				{
					Albedo = Color.GREEN,
				}
			));

			#endregion

			#region Planets

			float planetSize = 600;
			V3 planetPosition = new V3(0.0f, -1.5f, 2.5f) * room;
			scene.Primitives.Add(new Sphere(
				planetPosition,
				planetSize,
				new Material() { Texture = new Texture("planets\\jupiter.jpg") }
			));

			float satelliteSize = 0.3f * planetSize;
			scene.Primitives.Add(new Sphere(
				new V3(planetPosition.X + planetSize, planetPosition.Y + planetSize + satelliteSize, planetPosition.Z - planetSize + satelliteSize),
				satelliteSize,
				new Material() { Texture = new Texture("planets\\saturn.jpg") }
			));

			#endregion


			return scene;
		}
		
		public static Scene GetCornellScene(int width, int height, bool usePathtracing)
		{
			int depth = (int)(0.5f * width);

			V3 room = new V3(width, depth, height);

			// Camera
			V3 cameraPosition = new V3(width / 2, -width, 1.5f * height / 2);

			Scene scene = new Scene(cameraPosition)
			{
				BackgroundColor = Color.BLACK
			};

			#region Walls

			float left = 0.0f;
			float right = 1.0f;

			float bottom = 0.0f;
			float top = 1.0f;

			float front = -2.001f;
			float back = 1.0f;

			V3 a = new V3(left, front, top) * room;
			V3 b = new V3(right, front, top) * room;
			V3 c = new V3(right, front, bottom) * room;
			V3 d = new V3(left, front, bottom) * room;

			V3 e = new V3(left, back, top) * room;
			V3 f = new V3(right, back, top) * room;
			V3 g = new V3(right, back, bottom) * room;
			V3 h = new V3(left, back, bottom) * room;

			Primitive leftWall = new Parallelogram(d, h, a, new Material() { Albedo = Color.GREEN, });
			scene.Primitives.Add(leftWall);

			Primitive rightWall = new Parallelogram(g, c, f, new Material() { Albedo = Color.RED, });
			scene.Primitives.Add(rightWall);

			Primitive topWall = new Parallelogram(e, f, a, new Material() { Albedo = Color.WHITE, });//Texture = new Texture("milkyway.jpg") 
			scene.Primitives.Add(topWall);

			Primitive bottomWall = new Parallelogram(d, c, h, new Material() { Albedo = Color.WHITE });
			scene.Primitives.Add(bottomWall);

			Primitive backWall = new Parallelogram(h, g, e, new Material() { Albedo = Color.WHITE });
			scene.Primitives.Add(backWall);

			Primitive frontWall = new Parallelogram(c, d, b, new Material() { Albedo = Color.WHITE });
			scene.Primitives.Add(frontWall);

			#endregion

			#region Lights 

			if(usePathtracing)
			{
				scene.Primitives.Add(new Parallelogram(
					new V3(0.8f, 0.1f, top) * room,
					new V3(0.8f, 0.9f, 0.9999f * top) * room,
					new V3(0.2f, 0.1f, 0.9999f * top) * room,
					new Material() { Albedo = Color.WHITE, Emissive = 8.0f }
				));
			}
			else
			{
				scene.Lights.Add(new PointLight(
					new V3(0.5f, 0.0f, 0.8f) * room,
					0.7f * Color.WHITE
				));
			}

			#endregion

			#region Mirrors

			scene.Primitives.Add(new Sphere(
				new V3(0.7f * width, depth + 255, 0.6f * height),
				340,
				new Material() { Roughness = 0.0f }
			));

			scene.Primitives.Add(new Sphere(
				new V3(0.5f * width, 0.3f * depth, -255),
				340,
				new Material() { Roughness = 0.0f }
			));

			#endregion

			#region Spheres

			float sphereSize = 100;

			scene.Primitives.Add(new Sphere(
				new V3(0.2f, 0.4f, 0.3f) * room,
				sphereSize,
				new Material() { Shininess = 150, Roughness = 1.0f, Bumpmap = new Texture("bump1.jpg"), Relief = 2.0f }
			));
			scene.Primitives.Add(new Sphere(
				new V3(0.8f, 0.2f, 0.3f) * room,
				sphereSize,
				new Material() { Shininess = 150, Roughness = 1.0f }
			));

			scene.Primitives.Add(new Sphere(
				new V3(0.5f, 0.7f, 0.3f) * room,
				sphereSize,
				new Material() { Roughness = 0.9f }
			));

			scene.Primitives.Add(new Sphere(
				new V3(0.5f, -1.5f, 0.3f) * room,
				90,
				new Material() { Albedo = Color.BLUE }
			));
			#endregion



			return scene;
		}


		public static Scene GetHouseScene(int width, int height, bool usePathtracing)
		{
			int depth = (int)(0.5f * width);

			V3 room = new V3(width, depth, height);

			// Camera
			V3 cameraPosition = new V3(width / 2, -width, 1.5f * height / 2);

			Scene scene = new Scene(cameraPosition)
			{
				BackgroundColor = Color.SKY_BLUE
			};

			#region Lights

			scene.Primitives.Add(new Sphere(
				new V3(-1.0f, 0.5f, 1.0f) * room,
				200,
				new Material() { Emissive = 100.0f }
			));
			//scene.Lights.Add(new DirectionalLight(new V3(1.5f, 0.0f, -1.0f), 100.0f * Color.WHITE));

			#endregion

			#region Grid

			float bottom = 0.0f;
			float top = 1.0f;

			float left = 0.0f;
			float right = 1.0f;
			float middle = 0.3f;

			float front = -1.5f;
			float backLeft = 0.5f;
			float backRight = 1.0f;

			V3 a = new V3(left, front, top) * room;
			V3 b = new V3(left, 0.2f * (backLeft - 0.0f), top) * room;
			V3 c = new V3(left, 0.8f * (backLeft - 0.0f), top) * room;
			V3 d = new V3(left, backLeft, top) * room;
			V3 e = new V3(left, backLeft, bottom) * room;
			V3 f = new V3(left, 0.8f * (backLeft - 0.0f), bottom) * room;
			V3 g = new V3(left, 0.2f * (backLeft - 0.0f), bottom) * room;
			V3 h = new V3(left, front, bottom) * room;
			V3 i = new V3(left, 0.2f * (backLeft - 0.0f), 0.8f * (top - 0.0f)) * room;
			V3 j = new V3(left, 0.8f * (backLeft - 0.0f), 0.8f * (top - 0.0f)) * room;
			V3 k = new V3(left, 0.8f * (backLeft - 0.0f), 0.2f * (top - 0.0f)) * room;
			V3 l = new V3(left, 0.2f * (backLeft - 0.0f), 0.2f * (top - 0.0f)) * room;
			V3 m = new V3(middle, front, top) * room;
			V3 n = new V3(middle, backLeft, top) * room;
			V3 o = new V3(middle, backLeft, bottom) * room;
			V3 p = new V3(middle, front, bottom) * room;
			V3 q = new V3(right, front, top) * room;
			V3 r = new V3(right, front, bottom) * room;
			V3 s = new V3(middle, backRight, top) * room;
			V3 t = new V3(right, backRight, top) * room;
			V3 u = new V3(right, backRight, bottom) * room;
			V3 v = new V3(middle, backRight, bottom) * room;

			#endregion

			#region Walls

			scene.Primitives.Add(new Parallelogram(
				e, o, d,
				new Material() { Albedo = Color.PINK }
			));

			scene.Primitives.Add(new Parallelogram(
				h, p, e,
				new Material() { Albedo = Color.PINK }
			));

			scene.Primitives.Add(new Parallelogram(
				d, n, a,
				new Material() { Albedo = Color.PINK }
			));

			scene.Primitives.Add(new Parallelogram(
				o, v, n,
				new Material() { Albedo = Color.PINK }
			));

			scene.Primitives.Add(new Parallelogram(
				u, r, t,
				new Material() { Albedo = Color.PINK }
			));

			scene.Primitives.Add(new Parallelogram(
				v, u, s,
				new Material() { Albedo = Color.PINK }
			));

			scene.Primitives.Add(new Parallelogram(
				s, t, m,
				new Material() { Albedo = Color.PINK }
			));

			scene.Primitives.Add(new Parallelogram(
				p, r, v,
				new Material() { Albedo = Color.PINK }
			));

			// -------- Window

			scene.Primitives.Add(new Parallelogram(
				h, g, a,
				new Material() { Albedo = Color.PINK }
			));

			scene.Primitives.Add(new Parallelogram(
				i, j, b,
				new Material() { Albedo = Color.PINK }
			));

			scene.Primitives.Add(new Parallelogram(
				g, f, l,
				new Material() { Albedo = Color.PINK }
			));

			scene.Primitives.Add(new Parallelogram(
				f, e, c,
				new Material() { Albedo = Color.PINK }
			));


			#endregion

			return scene;
		}
	}
}
