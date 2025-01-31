

namespace TaskTracker.Models;

class UserTask
{
	public int Id { get; set; }

	public string? Description { get; set; }

	public UserTaskStatus Status { get; set; }

	public DateTime CreateAt { get; set; }

	public DateTime UpdatedAt { get; set; }

	internal enum UserTaskStatus
	{
		Todo = 0,
		InProgress = 1,
		Done = 2
	}
}
