using System.Text.Json;
using TaskTracker.Models;

namespace TaskTracker.Services;

static class TaskService
{
	private static readonly string fileName = "task_data.json";
	private static readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

	public static void HelpAction(List<string> parameters)
	{
		if (parameters.Count > 1) { Console.WriteLine("Invalid input for 'help' command!"); return; }

		Console.WriteLine("These are the available commands:" + "\n" + "\n" +
			"Commands to help you:" + "\n" +
			"   help                               -To display all of commands" + "\n" +
			"   clear                              -To clear the display" + "\n" +
			"   exit                               -To close the task tracker" + "\n" + "\n" +
			"Commands to manipulate tasks:" + "\n" +
			"   add <description>                  -To add a task with a specific descrition" + "\n" +
			"   update <id> <description>          -To update a task with specific id" + "\n" +
			"   delete <id>                        -To delete a task with specific id" + "\n" +
			"   mark-in-progress <id>              -To mark in progress a task with specific id" + "\n" +
			"   mark-done <id>                     -To mark as done a task with specific id" + "\n" +
			"   list [todo | in-progress | done]   -To list all of tasks (if you specify one of three status, only tasks with the specified status will be listed)" + "\n");
	}

	public static void AddAction(List<string> parameters)
	{
		if (parameters.Count <= 1 || parameters.Count >= 3) { Console.WriteLine("Invalid input for 'add' command!"); return; }

		var taskList = new List<UserTask>();
		var task = new UserTask
		{
			Id = GetTaskId(),
			Description = parameters[1],
			Status = Enums.UserTaskStatus.Todo,
			CreatedAt = DateTime.UtcNow,
			UpdatedAt = DateTime.UtcNow
		};

		if (!File.Exists(filePath))
		{
			using(FileStream fs = File.Create(filePath)) { Console.WriteLine($"The file {fileName} was created."); }
		}

		taskList = GetTasksFromJson();
		taskList.Add(task);

		var updatedTaskList = JsonSerializer.Serialize<List<UserTask>>(taskList);
		File.WriteAllText(filePath, updatedTaskList);

		Console.WriteLine($"Task added successfully (Id: {task.Id}).");
	}

	private static int GetTaskId()
	{
		if (File.Exists(filePath)) 
		{
			var taskList = GetTasksFromJson();
			if(taskList != null && taskList.Count > 0) { return taskList.OrderBy(t => t.Id).Last().Id + 1; }
		}
		return 1;
	}

	public static void UpdateAction(List<string> parameters)
	{
		if(parameters.Count != 3) { Console.WriteLine("Invalid input for 'update' command!"); return; }

		int taskId = int.TryParse(parameters[1], out int result) ? result : -1;
		if(taskId == -1) { Console.WriteLine("Invalid input for <id>!"); return; }

		var taskList = GetTasksFromJson();
		int taskIndex = taskList.FindIndex(t => t.Id == taskId);
		var task = taskIndex != -1 ? taskList[taskIndex] : null;

		if(task == null) { Console.WriteLine($"This task with id {taskId} doen't exist!"); return; }

		task.Description = parameters[2];
		task.UpdatedAt = DateTime.UtcNow;

		taskList[taskIndex] = task;

		var updatedTaskList = JsonSerializer.Serialize<List<UserTask>>(taskList);
		File.WriteAllText(filePath, updatedTaskList);

		Console.WriteLine("Task updated successfully.");
	}

	public static void DeleteAction(List<string> parameters)
	{
		if (parameters.Count != 2) { Console.WriteLine("Invalid input for 'delete' command!"); return; }

		int taskId = int.TryParse(parameters[1], out int result) ? result : -1;
		if (taskId == -1) { Console.WriteLine("Invalid input for <id>!"); return; }

		var taskList = GetTasksFromJson();
		int taskIndex = taskList.FindIndex(t => t.Id == taskId);
		
		if(taskIndex == -1) { Console.WriteLine($"This task with id {taskId} doen't exist!"); return; }
		
		taskList.RemoveAt(taskIndex);

		var updatedTaskList = JsonSerializer.Serialize<List<UserTask>>(taskList);
		File.WriteAllText(filePath, updatedTaskList);

		Console.WriteLine("Task deleted successfully.");
	}

	public static void MarkInProgressOrDone(List<string> parameters)
	{
		if(parameters.Count != 2) { Console.WriteLine($"Invalid input for '{parameters[0]}' command!"); return; }

		int taskId = int.TryParse(parameters[1], out int result) ? result : -1;
		if (taskId == -1) { Console.WriteLine("Invalid input for <id>!"); return; }

		var taskList = GetTasksFromJson();
		int taskIndex = taskList.FindIndex(t => t.Id == taskId);
		var task = taskIndex != -1 ? taskList[taskIndex] : null;

		if (task == null) { Console.WriteLine($"This task with id {taskId} doen't exist!"); return; }

		task.Status = parameters[0] == "mark-in-progress" ? Enums.UserTaskStatus.InProgress : Enums.UserTaskStatus.Done;

		taskList[taskIndex] = task;

		var updatedTaskList = JsonSerializer.Serialize<List<UserTask>>(taskList);
		File.WriteAllText(filePath, updatedTaskList);

		Console.WriteLine("Task updated successfully.");
	}

	private static List<UserTask> GetTasksFromJson()
	{
		var taskFromJsonFile = File.ReadAllText(filePath);
		if (string.IsNullOrEmpty(taskFromJsonFile)) { return []; }

		return JsonSerializer.Deserialize<List<UserTask>>(taskFromJsonFile) ?? [];
	}
}
