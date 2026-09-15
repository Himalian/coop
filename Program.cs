using System.CommandLine;
using Coop.Commands;
using Coop.Core;

var scoopPath = new ScoopPath();

var rootCommand = new RootCommand("coop - A fast Scoop alternative"){
    ListCommand.GetCommand(scoopPath.AppsPath),
	SearchCommand.GetCommand(scoopPath)
};

return await rootCommand.Parse(args).InvokeAsync();
