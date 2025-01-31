
using TaskTracker.Models;

DisplayWelcomeMessage();
List<string> commands = [];
while (true)
{
	Console.Write("Type command: ");
	var input = Console.ReadLine();

	if(string.IsNullOrEmpty(input)) { continue; }

	commands = input.Split(' ').ToList();

	var exit = false;
	var b = new UserTask();
	b.Status = 0;
	Console.WriteLine(b.Status);

	switch (commands[0].ToLower())
	{
		case "help":
			break;
		case "add":
			break;
		case "update":
			break;
		case "delete":
			break;
		case "mark-in-progress":
			break;
		case "mark-done":
			break;
		case "list":
			break;
		case "clear":
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