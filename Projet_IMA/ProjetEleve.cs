using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Collections.Concurrent;
using System.Drawing;

namespace Projet_IMA
{
	static class ProjetEleve
	{
		public static bool USE_PATHTRACING = true;
		public static int SPP = 0;
		public static int MAX_DIFFUSE_BOUNCES = 0; // nombre maximum de rebonds diffus avant la fin du rayon
		public static int MAX_SPECULAR_BOUNCES = 0; // nombre maximum de rebonds spéculaires avant la fin du rayon

		const int ThreadCount = 6;
		const int WindowSize = 50;

		// --------------------------------------------------------------------------------------

		static List<Thread> Threads = new List<Thread>();
		static int ActiveThreads = 0;

		static int Width = BitmapScreen.Width;
		static int Height = BitmapScreen.Height;

		static Scene Scene;

		public static void Go(int sceneIndex)
		{

			Scene = Scene.GetSceneByIndex(sceneIndex, Width, Height, USE_PATHTRACING);

			StartThreads();
		}

		private static void StartThreads()
		{
			ConcurrentBag<Point> jobs = new ConcurrentBag<Point>();

			for (int screenX = 0; screenX < Width; screenX += WindowSize)
			{
				for (int screenY = 0; screenY < Height; screenY += WindowSize)
				{
					jobs.Add(new Point(screenX, screenY));
				}
			}

			for (int i = 0; i < ThreadCount; i++)
			{
				Thread thread = new Thread(delegate () { ProcessRaycastThread(jobs); });
				Threads.Add(thread);
				thread.Start();
				ActiveThreads++;
			}
		}

		private static void ProcessRaycastThread(in ConcurrentBag<Point> jobs)
		{
			Math.InitRand(); // thread static

			while (jobs.TryTake(out Point pixel))
			{
				int xScreenStart = pixel.X;
				int xScreenEnd = System.Math.Min(pixel.X + WindowSize, Width);
				int yScreenStart = pixel.Y;
				int yScreenEnd = System.Math.Min(pixel.Y + WindowSize, Height);

				ColorWindow window = new ColorWindow(xScreenEnd - xScreenStart, yScreenEnd - yScreenStart); // custom data structure to store "Color" (Bitmap stores "System.Drawing.Color")

				for (int xScreen = xScreenStart; xScreen < xScreenEnd; xScreen++)
				{
					for (int yScreen = yScreenStart; yScreen < yScreenEnd; yScreen++)
					{
						Color color = USE_PATHTRACING ?
							RaycastEngine.ProcessPathTracing(xScreen, yScreen, Scene, SPP, MAX_DIFFUSE_BOUNCES, MAX_SPECULAR_BOUNCES)
							:
							RaycastEngine.Process(xScreen, yScreen, Scene);

						window.SetPixel(xScreen - xScreenStart, yScreen - yScreenStart, color);
					}
				}

				BitmapScreen.DrawWindowFromMainThread(xScreenStart, yScreenStart, window);
			}

			ActiveThreads--;

			if(ActiveThreads == 0)
			{
				BitmapScreen.SaveFromMainThread(DateTime.Now.ToString("yyyyMMddHHmmss"));
			}
		}

		public static void Interrupt()
		{
			foreach (Thread thread in Threads)
			{
				thread.Abort();
			}
		}
	}
}
