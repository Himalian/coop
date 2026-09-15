using System.CommandLine;
using Coop.Commands;

var rootCommand = new RootCommand("coop - A fast Scoop alternative"){
    ListCommand.GetCommand()
};

return await rootCommand.Parse(args).InvokeAsync();
