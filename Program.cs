using System.CommandLine;

var listCommand = new Command("list", "list installed apps");

listCommand.SetAction(parseResult => {
		Console.WriteLine("Example command");
		});

var rootCommand = new RootCommand("coop - A fast Scoop alternative"){
	listCommand
};

return await rootCommand.Parse(args).InvokeAsync();
