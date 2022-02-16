namespace Core.Dtos;

public class TaskDto
{
    public string? Description { get; set; }

    public decimal WorkTime { get; set; } = 0.0m;
}