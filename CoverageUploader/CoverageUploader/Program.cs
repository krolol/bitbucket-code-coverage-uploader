using CommandLine;
using CoverageUploader.Rest;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoverageUploader
{
	public class Arguments
	{
		[Option('u', "bitbucket.username", Required = true, HelpText = "Username of bitbucket user")]
		public string Username { get; set; }
		[Option('p', "bitbucket.password", Required = true, HelpText = "Password of bitbucket user")]
		public string Password { get; set; }
		[Option('c', "bitbucket.commit.id", Required = true, HelpText = "Commit id")]
		public string CommidID { get; set; }
		[Option('b', "bitbucket.url", Required = true, HelpText = "Bitbucket url")]
		public string BitbucketURL { get; set; }
		[Option('f', "coverage.file", Required = true, HelpText = "Path to coverage file")]
		public string CoverageFile { get; set; }

	}
	class Program
	{
		static int Main(string[] args)
		{
			Parser.Default.ParseArguments<Arguments>(args)
				.WithParsed(arguments => RunArguments(arguments));
			return -1;
		}

		private static int RunArguments(Arguments arguments)
		{
			try
			{
				var client = new Client(arguments.BitbucketURL, arguments.Username, arguments.Password);
				var responseMessage = client.PostCoverageAsync(arguments.CommidID, "{\"file\"}");
				Console.WriteLine(responseMessage.Result.ToString());
				return 0;
			}
			catch(Exception e)
			{
				Console.WriteLine(e);
				return -1;
			}
		}
	}
}
