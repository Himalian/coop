using System.CommandLine;

var listCommand = new Command("list", "list installed apps");

listCommand.SetAction(parseResult =>
{
    string scoopPath = Environment.GetEnvironmentVariable("SCOOP")!;
    string[] apps = Directory.GetDirectories(Path.Join(scoopPath, "apps"));
	Console.WriteLine("Installed apps:");
	foreach(var app in apps){
		Console.WriteLine(app.TrimStart(Path.Join(scoopPath,"apps")));
	}
});

var rootCommand = new RootCommand("coop - A fast Scoop alternative"){
    listCommand
};

return await rootCommand.Parse(args).InvokeAsync();
