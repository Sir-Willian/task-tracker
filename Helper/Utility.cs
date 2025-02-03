using System.Text.RegularExpressions;

namespace TaskTracker.Helper;

static class Utility
{
	public static List<string> ParseInput(string input)
	{
		var commandArgs = new List<string>();

		var regex = new Regex(@"[\""].+?[\""]|[^ ]+");
		var matches = regex.Matches(input);

		foreach(Match match in matches)
		{
			string value = match.Value.Trim('"');
			commandArgs.Add(value);
		}

		return commandArgs;
	}
}
