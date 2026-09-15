using System.CommandLine;
namespace Coop.Commands;

public static class ListCommand
{
	private static string GetScoopPath()
	{
		string? path = Environment.GetEnvironmentVariable("SCOOP");
		if (path == null)
		{
			Console.WriteLine("Warning: environment variable 'SCOOP' not found, fallback to default path");
			path = Path.Join(Environment.GetEnvironmentVariable("HOMEPATH"), "scoop");
		}
		return path;
	}

	private static string[]? GetApps(string scoopPath, string? query)
	{
		string[]? apps = Directory.GetDirectories(Path.Join(scoopPath, "apps"));
		if (!string.IsNullOrEmpty(query))
		{
			apps = [.. apps
				.Select(Path.GetFileName)
				.OfType<string>()
				.Where(x => x != null && x.Contains(query, StringComparison.OrdinalIgnoreCase))];
		}
		return apps;
	}
	public static Command GetCommand()
	{
		Argument<string?> queryArgument = new("query")
		{
			Arity = ArgumentArity.ZeroOrOne
		};
		var command = new Command("list", "list installed apps")
		{
			queryArgument
		};
		command.SetAction(parseResult =>
		{
			string? query = parseResult.GetValue(queryArgument);
			string scoopPath = GetScoopPath();


			var apps = GetApps(scoopPath, query);
			if (!string.IsNullOrEmpty(query))
			{
				Console.WriteLine($"Installed apps matching '{query}':");
			}
			else
			{
				Console.WriteLine("Installed apps:");
			}
			if (apps == null) { return; }
			foreach (var app in apps)
			{
				Console.WriteLine(app.TrimStart(Path.Join(scoopPath, "apps")));
			}

		});


		return command;

	}
}
