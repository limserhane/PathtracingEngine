using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;


namespace Projet_IMA
{
	enum DisplayMode { SLOW_MODE, FULL_SPEED};

	static class BitmapScreen
	{
		static public int Width { get; private set; }
		static public int Height { get; private set; }

		const int refresh_every = 1000; // force l'affiche tous les xx pix
		static int nb_pix = 0;				 // comptage des pixels
		
		static private Bitmap _bitmap;
		static private DisplayMode _mode;
		static private int _stride;
		static private BitmapData _data;

		static public Bitmap Init(int width, int height)
		{
			Width = width;
			Height = height;
			_bitmap = new Bitmap(width, height);
			return _bitmap;
		}
 
		static void DrawFastPixel(int x, int y, Color c)
		{
			unsafe
			{
				byte RR, VV, BB; 
				c.Check();
				c.To255(out RR, out  VV, out  BB);
				
				byte* ptr = (byte*)_data.Scan0;
				ptr[(x * 3) + y * _stride	] = BB;
				ptr[(x * 3) + y * _stride + 1] = VV;
				ptr[(x * 3) + y * _stride + 2] = RR;
			}
		}

		static void DrawSlowPixel(int x, int y, Color c)
		{
			System.Drawing.Color cc = c.ConvertToSystem();
			_bitmap.SetPixel(x, y, cc);
			
			Program.MyForm.PictureBoxInvalidate();
			nb_pix++;
			if (nb_pix > refresh_every)  // force l'affichage à l'écran tous les 1000pix
			{
			   Program.MyForm.PictureBoxRefresh();
			   nb_pix = 0;
			}
		 }

		/// /////////////////   public methods ///////////////////////

		delegate void SaveDelegate(string filename);
		static public void SaveFromMainThread(string filename)
		{
			Program.MyForm.Invoke(new SaveDelegate(Save), new object[] { filename });
		}

		static public void Save(string filename)
		{
			try
			{
				_bitmap.Save(filename + ".jpg", ImageFormat.Jpeg);
			}
			catch (Exception)
			{
				
			}
		}


		delegate void DrawWindowDelegate(int x, int y, ColorWindow window);

		static public void DrawWindowFromMainThread(int x, int y, ColorWindow window)
		{
			Program.MyForm.Invoke(new DrawWindowDelegate(DrawWindow), new object[] { x, y, window });
		}

		public static void DrawWindow(int x, int y, ColorWindow window)
		{
			for (int bitmapX = 0; bitmapX < window.Width; bitmapX++)
			{
				for (int bitmapY = 0; bitmapY < window.Height; bitmapY++)
				{
					Color color = window.GetPixel(bitmapX, bitmapY);
					BitmapScreen.DrawPixel(x + bitmapX, y + bitmapY, color);
				}
			}
		}

		static public void RefreshScreen(Color c)
		{
			if (Program.MyForm.Checked())
			{
				_mode = DisplayMode.SLOW_MODE;
				Graphics g = Graphics.FromImage(_bitmap);
				System.Drawing.Color cc = c.ConvertToSystem();
				g.Clear(cc);
			}
			else
			{
				_mode = DisplayMode.FULL_SPEED;
				_data = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
				_stride = _data.Stride;
				for (int x = 0; x < Width; x++)
					for (int y = 0; y < Height; y++)
						DrawFastPixel(x, y, c);
			}
		}
		
		public static void DrawPixel(int x, int y, Color c)
		{
			int x_screen = x;
			int y_screen = Height - y;

			if ((x_screen >= 0) && (x_screen < Width) && (y_screen >= 0) && (y_screen < Height))
				if (_mode == DisplayMode.SLOW_MODE) DrawSlowPixel(x_screen, y_screen, c);
				else DrawFastPixel(x_screen, y_screen, c);
		}
		
		static public void Show()
		{
			if (_mode == DisplayMode.FULL_SPEED)
				_bitmap.UnlockBits(_data);

			Program.MyForm.PictureBoxInvalidate();
		}
	}
}
