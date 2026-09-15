using System.CommandLine;

namespace Coop.Commands;

public static class SearchCommand{	
	public static Command GetCommand(){
		Argument<string> argument = new("query");
		Option<string> bucket = new("--bucket");
		Command command = new("search","search apps from buckets"){
			argument,
			bucket
		};

		command.SetAction(parseResult => {

				});
		return command;
	}
}
