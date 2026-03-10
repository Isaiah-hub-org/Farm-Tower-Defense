using Godot;
using System;

public partial class Enemy2 : CharacterBody2D
{
	[Export] public float Speed = 95f;

	private PathFollow2D pathFollow;

	public override void _Ready()
	{
		pathFollow = GetParent<PathFollow2D>();
	}

	public override void _PhysicsProcess(double delta)
	{
		var oldProgressRatio = pathFollow.ProgressRatio;
		
		pathFollow.Progress += Speed * (float)delta;

		if (pathFollow.ProgressRatio < oldProgressRatio || pathFollow.ProgressRatio == 1)
		{
			pathFollow.QueueFree(); 
			QueueFree();
		}
	}
}
