using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Projet_IMA
{
	public struct Color
	{
		public float R, G, B;	// composantes R,V,B comprises entre 0 et 1

		public static readonly Color RED = new Color(1.0f, 0.0f, 0.0f);
		public static readonly Color GREEN = new Color(0.0f, 1.0f, 0.0f);
		public static readonly Color BLUE = new Color(0.0f, 0.0f, 1.0f);

		public static readonly Color MAGENTA = new Color(1.0f, 0.0f, 1.0f);
		public static readonly Color YELLOW = new Color(1.0f, 1.0f, 0.0f);
		public static readonly Color CYAN = new Color(0.0f, 1.0f, 1.0f);

		public static readonly Color BROWN = new Color(0.545f, 0.271f, 0.075f);
		public static readonly Color PINK = new Color(1.0f, 0.753f, 0.796f);
		public static readonly Color ORANGE = new Color(1.0f, 0.549f, 0.0f);
		public static readonly Color SKY_BLUE = new Color(0.149f, 0.768f, 0.925f);
		public static readonly Color VIOLET = new Color(0.576f, 0.439f, 0.859f);
		public static readonly Color MAROON = new Color(0.431f, 0.051f, 0.145f);

		public static readonly Color BLACK = new Color(0.0f, 0.0f, 0.0f);
		public static readonly Color GREY = new Color(0.827f, 0.827f, 0.827f);
		public static readonly Color DARK_GREY = new Color(0.2f, 0.2f, 0.2f);
		public static readonly Color WHITE = new Color(1.0f, 1.0f, 1.0f);

		public static readonly Color ZERO = new Color(0.0f, 0.0f, 0.0f);
		public static readonly Color ONE = new Color(1.0f, 1.0f, 1.0f);

		public void From255(byte RR, byte VV, byte BB)
		{
			R = (float)(RR / 255.0);
			G = (float)(VV / 255.0);
			B = (float)(BB / 255.0);
		}

		static public  void Transpose(ref Color cc, System.Drawing.Color c)
		{
			cc.R = (float) (c.R / 255.0);
			cc.G = (float) (c.G / 255.0);
			cc.B = (float) (c.B / 255.0);
		}

		public void Check()
		{
			if (R > 1.0) R = 1.0f;
			if (G > 1.0) G = 1.0f;
			if (B > 1.0) B = 1.0f;
		}

		public void To255(out byte RR, out byte VV, out byte BB)
		{
			RR = (byte)(R * 255);
			VV = (byte)(G * 255);
			BB = (byte)(B * 255);
		}

		public System.Drawing.Color ConvertToSystem()
		{
			Check();
			To255(out byte RR, out byte GG, out byte BB);
			return System.Drawing.Color.FromArgb(RR, GG, BB);
		}

		public Color(float R, float V, float B)
		{
			this.R = R;
			this.G = V;
			this.B = B;
		}

		public Color(Color c)
		{
			this.R = c.R;
			this.G = c.G;
			this.B = c.B;
		}

		// méthodes

		public float GreyLevel()						// utile pour le Bump Map
		{
			return (R + B + G) / 3.0f;
		}
		public float Luminance()                        // utile pour le Bump Map
		{
			return (0.3f * R + 0.59f * B + 0.11f * G) / 3.0f;
		}

		// opérateurs surchargés

		public static Color operator +(Color a, Color b)
		{
			return new Color(a.R + b.R, a.G + b.G, a.B + b.B);
		}

		public static Color operator -(Color a, Color b)
		{
			return new Color(a.R - b.R, a.G - b.G, a.B - b.B);
		}

		public static Color operator -(Color a)
		{
			return new Color(-a.R, -a.G, -a.B);
		}

		public static Color operator *(Color a, Color b)
		{
			return new Color(a.R * b.R, a.G * b.G, a.B * b.B);
		}

		public static Color operator *(float a, Color b)
		{
			return new Color(a * b.R, a * b.G, a * b.B);
		}

		public static Color operator *(Color b, float a)
		{
			return new Color(a * b.R, a * b.G, a * b.B);
		}

		public static Color operator /(Color b, float a)
		{
			return new Color(b.R / a, b.G / a, b.B / a);
		}
	}
}

	

					
