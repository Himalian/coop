using System.CommandLine;
namespace Coop.Commands;

public static class ListCommand
{
    public static Command GetCommand()
    {
        var command = new Command("list", "list installed apps");
        command.SetAction(parseResult =>
        {
            string scoopPath = Environment.GetEnvironmentVariable("SCOOP")!;

            if (scoopPath == null)
            {
                Console.WriteLine("Warning: environment variable 'SCOOP' not found, fallback to default path");
                scoopPath = Path.Join(Environment.GetEnvironmentVariable("HOMEPATH"), "scoop");
            }

            string[] apps = Directory.GetDirectories(Path.Join(scoopPath, "apps"));
            Console.WriteLine("Installed apps:");
            foreach (var app in apps)
            {
                Console.WriteLine(app.TrimStart(Path.Join(scoopPath, "apps")));
            }
        });


        return command;

    }
}
