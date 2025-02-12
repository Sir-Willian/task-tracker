using TaskTracker.Helper;
using TaskTracker.Services;

DisplayWelcomeMessage();
List<string> commands = [];
while (true)
{
	Console.Write("Type command: ");
	var input = Console.ReadLine();

	if(string.IsNullOrEmpty(input)) { continue; }

	commands = Utility.ParseInput(input);

	var exit = false;

	switch (commands[0].ToLower())
	{
		case "help":
			TaskService.HelpAction(commands);
			break;
		case "add":
			TaskService.AddAction(commands);
			break;
		case "update":
			TaskService.UpdateAction(commands);
			break;
		case "delete":
			TaskService.DeleteAction(commands);
			break;
		case "mark-in-progress" or "mark-done":
			TaskService.MarkInProgressOrDone(commands);
			break;
		case "list":
			TaskService.ListTasks(commands);
			break;
		case "clear":
			Console.Clear();
			DisplayWelcomeMessage();
			break;
		case "exit":
			exit = true;
			break;
		default:
			Console.WriteLine("Invalid input! Try again.");
			break;
	}

	if (exit) { break; }
}

Console.WriteLine("Thanks for using the program. ( ^o^)´ Bye");

void DisplayWelcomeMessage()
{
	Console.WriteLine("Welcome to Task-Tracker-Cli!");
	Console.WriteLine("Type 'help' to display the set of commands");
}