using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoverageUploader.Coverage
{
	class JSONCoverage
	{
		[JsonProperty(PropertyName = "files")]
		public List<FileCoverage> FileCoverages;

		public JSONCoverage()
		{
			FileCoverages = new List<FileCoverage>();
		}

		public string ConvertToJson()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
