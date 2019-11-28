using System;
using System.Collections.Generic;
using System.Text;

namespace CoverageUploader.Rest
{
	class Client
	{
		private string _bitbucketURL
		{
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("Bitbucket URL cannot be empty");
				}
				_bitbucketURL = value;
			}
		}
		private string _user 
		{ 
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("User cannot be empty");
				}
				_user = value;
			}
		}
		private string _password { get; set; }
		private string _token { get; set; }
		private int? _timeout { get; set; }

		public Client(string bitbucketURL, string user, string password, string token, int? timeout = 100)
		{
			_bitbucketURL = bitbucketURL;
			_user = user;
			_password = password;
			_token = token;
			_timeout = timeout;
		}
	}
}
