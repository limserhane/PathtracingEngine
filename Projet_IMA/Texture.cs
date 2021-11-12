using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Projet_IMA
{
	class Texture
	{
		private int _width;
		private int _height;
		Color [,] C;

		public Color GetColor(float u, float v)
		{
			return Interpol(_width * u, _height * v);
		}

		public void Bump(float u, float v, out float dhdu, out float dhdv)
		{
			float x = u * _height; // normalized
			float y = v * _width; // normalized

			float vv = Interpol(x, y).GreyLevel();
			float vx = Interpol(x + 1, y).GreyLevel(); // value for next pixel (u)
			float vy = Interpol(x, y + 1).GreyLevel(); // value for next pixel (v)

			dhdu = vx - vv;
			dhdv = vy - vv;
		}
	
		public Texture(string ff)
		{
			string path = Utils.Files.GetPath("assets\\textures", ff);

			Bitmap B = new Bitmap(path); 
			
			_height = B.Height;
			_width = B.Width;
			BitmapData data = B.LockBits(new Rectangle(0, 0, B.Width, B.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			int stride = data.Stride;
			 
			C = new Color[_width,_height];

			unsafe
			{
				byte* ptr = (byte*)data.Scan0;
				for (int x = 0; x < _width; x++)
					for (int yp = 0; yp < _height; yp++)
					{
						//int y = _height - yp - 1;
						int y = yp;
						byte RR, VV, BB;
						BB = ptr[(x * 3) + y * stride];
						VV = ptr[(x * 3) + y * stride + 1];
						RR = ptr[(x * 3) + y * stride + 2];
						C[x, y].From255(RR, VV, BB);
					}
			}
			B.UnlockBits(data);
			B.Dispose();
		}

		private Color Interpol(float Lu, float Hv)
		{
			int x = (int)(Lu);
			//int y = (int)(Hv);
			int y = (int)(_height - Hv);

			float cx = Lu - x;
			float cy = Hv - y;

			x = x % _width;
			y = y % _height;
			if (x < 0) x += _width;
			if (y < 0) y += _height;


			return C[x, y];

			//int xpu = (x + 1) % _width;
			//int ypu = (y + 1) % _height;

			//float ccx = cx * cx;
			//float ccy = cy * cy;

			//return
			//  C[x, y] * (1 - ccx) * (1 - ccy)
			//+ C[xpu, y] * ccx * (1 - ccy)
			//+ C[x, ypu] * (1 - ccx) * ccy
			//+ C[xpu, ypu] * ccx * ccy;
		}
	}	
}
