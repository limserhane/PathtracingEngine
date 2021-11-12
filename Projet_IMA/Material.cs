using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
	class Material
	{
		public Color Albedo { get; set; }
		public float Shininess { get; set; }
		public float Roughness { get; set; } // what part of light is reflection is diffuse (vs specular)
		public Texture Texture { get; set; }
		public bool HasTexture { get { return Texture != null; } }
		public Texture Bumpmap { get; set; }
		public bool HasBumpmap { get { return Bumpmap != null; } }
		public float Relief { get; set; }
		public float Emissive { get; set; }

		public Material()
		{
			Albedo = Color.WHITE;
			Shininess = 50;
			Roughness = 1.0f;
			Texture = null;
			Bumpmap = null;
			Relief = 1.0f;
			Emissive = 0.0f;
		}

		public Color GetColor(float normalizedU, float normalizedV)
		{
			if(!HasTexture)
			{
				return Albedo;
			}

			return Albedo * Texture.GetColor(normalizedU, normalizedV);
		}

		public V3 GetBumpedNormal(Primitive primitive, V3 point, float u, float v, float normalizedU, float normalizedV)
		{
			V3 n = primitive.GetNormal(point);

			if(!HasBumpmap)
			{
				return n;
			}

			Bumpmap.Bump(normalizedU, normalizedV, out float dhdu, out float dhdv);

			V3 dmdu = primitive.GetDu(u, v);
			V3 dmdv = primitive.GetDv(u, v);

			V3 T2 = dhdv * (dmdu ^ n);
			V3 T3 = dhdu * (n ^ dmdv);

			V3 np = n + (Relief / 256.0f) * (T2 + T3);

			np.Normalize();

			return np;

		}

	}
}
