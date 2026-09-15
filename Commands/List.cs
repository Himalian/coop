using System.CommandLine;
namespace Coop.Commands;

public static class ListCommand
{

	private static string[]? GetApps(string scoopAppPath, string? query)
	{
		string[]? apps = Directory.GetDirectories(scoopAppPath);
		if (!string.IsNullOrEmpty(query))
		{
			apps = [.. apps
				.Select(Path.GetFileName)
				.OfType<string>()
				.Where(x => x != null && x.Contains(query, StringComparison.OrdinalIgnoreCase))];
		}
		return apps;
	}
	public static Command GetCommand(string path)
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

			var apps = GetApps(path, query);
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
				Console.WriteLine(app);
			}

		});


		return command;

	}
}
