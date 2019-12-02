using CoverageUploader.Coverage;
using System.Xml;
using LibGit2Sharp;
using System.IO;
using System;

namespace CoverageUploader.Converters
{
	class CoberturaConverter
	{
		private const string _pathToClass = "//coverage/packages/package/classes/class";
		private const string _pathToLine = "lines/line";
		private const string _pathToSource = "coverage/sources/source";

		public static JSONCoverage ConvertFromCobertura(string pathToCoverageFile, string repoRoot)
		{
			JSONCoverage jsonCoverage = new JSONCoverage();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(pathToCoverageFile);
			string source = xmlDocument.SelectSingleNode(_pathToSource).InnerText;
			repoRoot = string.Equals(repoRoot, null) ? string.Empty : repoRoot;
			foreach (XmlNode xmlNode in xmlDocument.SelectNodes(_pathToClass))
			{
				string fileName = Help.GetRelativeFilePath(Path.Combine(source, xmlNode.Attributes["filename"].Value), repoRoot);
				FileCoverage fileCoverage = new FileCoverage(fileName);
				var a = xmlNode.SelectNodes(_pathToLine).Count;
				foreach (XmlNode node in xmlNode.SelectNodes(_pathToLine))
				{
					int lineNumber = int.Parse(node.Attributes["number"].Value);
					if (int.Parse(node.Attributes["hits"].Value) > 0)
					{
						if (bool.Parse(node.Attributes["branch"].Value))
						{
							if (double.Parse(node.Attributes["condition-coverage"].Value.Split('%')[0]) < 100)
							{
								fileCoverage.PartialCoveredLines.Add(lineNumber);
								continue;
							}
						}
						fileCoverage.FullyCoveredLines.Add(lineNumber);
					}
					else
					{
						fileCoverage.UncoveredLines.Add(lineNumber);
					}
				}
				jsonCoverage.FileCoverages.Add(fileCoverage);
			}
			return jsonCoverage;
		}
	}
}
