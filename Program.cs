using System.CommandLine;
using Coop.Commands;

var rootCommand = new RootCommand("coop - A fast Scoop alternative"){
    ListCommand.GetCommand(),
	SearchCommand.GetCommand()
};

return await rootCommand.Parse(args).InvokeAsync();
