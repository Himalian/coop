using System.CommandLine;
using Coop.Core;
namespace Coop.Commands;

public static class SearchCommand
{
	/// <summary>
	/// Get all apps in given bucket
	/// </summary>
	/// <param name="bucketRootPath" bucket name </param>
	static string[] GetAppsFromBucket(string bucketRootPath,string bucketName)
	{
		string[] apps = [.. Directory.GetFiles(Path.Join(bucketRootPath,bucketName,"bucket")).Select(p => Path.GetFileNameWithoutExtension(p))];
		return apps;
	}
	public static Command GetCommand(ScoopPath path)
	{
		Argument<string> argument = new("query");
		Option<string> bucket = new("--bucket");
		Command command = new("search", "search apps from buckets"){
			argument,
			bucket
		};

		command.SetAction(parseResult =>
		{
			var query = parseResult.GetValue(argument)!;
			var bucketName = parseResult.GetValue(bucket)!;
			var apps = GetAppsFromBucket(path.BucketsPath,bucketName);
			apps = [.. apps.Where(app => app.Contains(query,StringComparison.OrdinalIgnoreCase))];
			foreach (var app in apps)
			{
				Console.WriteLine(app);
			}
		});
		return command;
	}
}
