using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
	class ColorWindow
	{
		Color[,] _data;
		public readonly int Width;
		public readonly int Height;

		public ColorWindow(int width, int height)
		{
			_data = new Color[width, height];
			Width = width;
			Height = height;
		}

		public void SetPixel(int x, int y, Color color)
		{
			_data[x, y] = color;
		}

		public Color GetPixel(int x, int y)
		{
			return _data[x, y];
		}
	}
}
