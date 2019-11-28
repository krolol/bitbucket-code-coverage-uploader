using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CoverageUploader.Rest
{
	class Client
	{
		private string _bitbucketURL { get; set; }
		private string _username { get; set; }
		private string _password { get; set; }
		private int _timeout { get; set; }

		public Client(string bitbucketURL, string username, string password, int timeout = 120)
		{
			_bitbucketURL = bitbucketURL;
			_username = username;
			_password = password;
			_timeout = timeout;
		}

		public async Task<HttpResponseMessage> PostCoverageAsync(string commitId, string jsonData)
		{
			using (var client = new HttpClient())
			{
				var requestURL = $"{_bitbucketURL}/rest/code-coverage/1.0/commits/{commitId}";
				var data = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_username}:{_password}"));
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
				client.Timeout = TimeSpan.FromSeconds(_timeout);
				var response = await client.PostAsync(requestURL, data);
				return response;
			}
		}
	}
}
