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

	private static List<UserTask> GetTasksFromJson()
	{
		var taskFromJsonFile = File.ReadAllText(filePath);
		if (string.IsNullOrEmpty(taskFromJsonFile)) { return []; }

		return JsonSerializer.Deserialize<List<UserTask>>(taskFromJsonFile) ?? [];
	}
}
