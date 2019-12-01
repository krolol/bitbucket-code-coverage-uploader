using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoverageUploader
{
	class Help
	{
		public static string GetRelativeFilePath(string path)
		{
			string rootFolder = path;
			do
			{
				rootFolder = Directory.GetParent(rootFolder).FullName;
			} while (!Repository.IsValid(rootFolder));
			return Path.GetRelativePath(rootFolder, path).Replace("\\","/");
		}
	}
}
