using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Projet_IMA
{
	class Mesh
	{
		public static List<Triangle> LoadMesh(string filename, V3 position, float scale, Material material)
		{
			ParseObjFile(filename, out List<V3> vertices, out List<int> indices);

			List<Triangle> triangles = new List<Triangle>();

			for (int i = 0; i < indices.Count; i += 3)
			{
				triangles.Add(new Triangle( // should use xzy instead of xyz ?
					position + scale * vertices[indices[i]],
					position + scale * vertices[indices[i + 1]],
					position + scale * vertices[indices[i + 2]],
					material
				));
			}

			return triangles;
		}

		private static void ParseObjFile(string filename, out List<V3> vertices, out List<int> indices)
		{
			vertices = new List<V3>();
			indices = new List<int>();

			string[] tokens;

			foreach (string line in System.IO.File.ReadLines(Utils.Files.GetPath("assets\\meshes\\", filename)))
			{
				if (line.Trim() == "")
					continue;

				tokens = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

				switch (tokens[0])
				{
					case "v":
						vertices.Add(new V3(
							Math.Parsef(tokens[1]),
							Math.Parsef(tokens[2]),
							Math.Parsef(tokens[3])
						));
						break;

					case "f":
						for (int i = 1; i <= 3; i++)
						{
							indices.Add(int.Parse(tokens[i].Split('/')[0]) - 1);
						}
						break;
				}
			}
		}



	}
}
