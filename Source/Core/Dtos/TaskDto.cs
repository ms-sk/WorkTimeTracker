﻿using Core.Models;

namespace Core.Dtos;

public class TaskDto
{
    public Guid? Id { get; set; }
    
    public string? Description { get; set; }

    public double WorkTime { get; set; }
    
    public WorkType Type { get; set; }
}