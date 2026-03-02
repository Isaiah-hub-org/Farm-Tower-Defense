using Godot;
using System;

public partial class Enemy2 : CharacterBody2D
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

		if (pathFollow.ProgressRatio >= 1f)
		{
			pathFollow.QueueFree(); // delete at end of path
		}
	}
}
