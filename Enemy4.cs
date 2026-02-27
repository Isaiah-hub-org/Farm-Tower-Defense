using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	[Export] public float Speed = 50f;

	private PathFollow2D pathFollow;

	public override void _Ready()
	{
		pathFollow = GetParent<PathFollow2D>();
	}

	public override void _PhysicsProcess(double delta)
	{
		pathFollow.Progress += Speed * (float)delta;
	}
	public override void _Process(double delta)
	{
		var pathFollow = GetParent<PathFollow2D>();
		pathFollow.Progress += Speed * (float)delta;

		if (pathFollow.ProgressRatio >= 3.0f)
		{
			pathFollow.QueueFree(); 
		}
		
		if (pathFollow.ProgressRatio >= 1f)
		{
			pathFollow.QueueFree();
		}
	}
}