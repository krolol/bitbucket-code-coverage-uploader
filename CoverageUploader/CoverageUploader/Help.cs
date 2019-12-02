using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoverageUploader
{
	class Help
	{
		public static string GetRelativeFilePath(string path, string repoRoot)
		{
			string rootFolder = string.IsNullOrEmpty(repoRoot) ? path : repoRoot;
			while (!Repository.IsValid(rootFolder))
			{
				rootFolder = Directory.GetParent(rootFolder).FullName;
			}
			if (!string.IsNullOrEmpty(repoRoot)) {
				DirectoryInfo relativeFullPath = Directory.GetParent(path);
				string fileName = Path.GetFileName(path);
				string relativePath = string.Empty;
				while (!File.Exists(path))
				{
					relativePath = Path.Combine(Path.Combine(relativeFullPath.Name), relativePath);
					path = Path.Combine(rootFolder, Path.Combine(relativePath, fileName));
					relativeFullPath = Directory.GetParent(relativeFullPath.FullName);
				}
			}
			return Path.GetRelativePath(rootFolder, path).Replace("\\", "/");
		}
	}
}
