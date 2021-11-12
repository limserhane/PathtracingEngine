using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA.Utils
{
	static class Files
	{
		public static string GetPath(string folder, string filename)
		{
			string s = System.IO.Path.GetFullPath(".");
			string path = System.IO.Path.Combine(s, folder, filename);
			return path;
		}
	}
}
