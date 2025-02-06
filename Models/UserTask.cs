namespace TaskTracker.Models;

class UserTask
{
	public int Id { get; set; }

	public string? Description { get; set; }

	public TaskTracker.Enums.UserTaskStatus Status { get; set; }

	public DateTime CreatedAt { get; set; }

	public DateTime UpdatedAt { get; set; }
}
