using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoverageUploader.Coverage
{
	class FileCoverage
	{
		[JsonProperty(PropertyName = "path")]
		public string PathToFile { get; set; }
		[JsonIgnore]
		public List<int> FullyCoveredLines { get; set; }
		[JsonIgnore]
		public List<int> PartialCoveredLines { get; set; }
		[JsonIgnore]
		public List<int> UncoveredLines { get; set; }
		[JsonProperty(PropertyName = "coverage")]
		public string LinesCoverage
		{
			get
			{
				string fullyCoveredLines = FullyCoveredLines.Any() ? $"C:{string.Join(",", FullyCoveredLines)};" : "";
				string partialCoveredLines = PartialCoveredLines.Any() ? $"P:{string.Join(",", PartialCoveredLines)};" : "";
				string uncoveredLines = UncoveredLines.Any() ? $"U:{string.Join(",", UncoveredLines)};" : "";
				return $"{fullyCoveredLines}{partialCoveredLines}{uncoveredLines}";
			}
		}

		public FileCoverage(string pathToFile)
		{
			PathToFile = pathToFile;
			FullyCoveredLines = new List<int>();
			PartialCoveredLines = new List<int>();
			UncoveredLines = new List<int>();
		}
	}
}
