using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	[Export] public float Speed = 10f;
	[Export] public int HP = 1;
	private PathFollow2D pathFollow;

	public override void _Ready()
	{
		pathFollow = GetParent<PathFollow2D>();
	}

	public override void _PhysicsProcess(double delta)
	{
		// Move along the path
		pathFollow.Progress += Speed * (float)delta;
	}
	public override void _Process(double delta)
	{
		var pathFollow = GetParent<PathFollow2D>();
		pathFollow.Progress += Speed * (float)delta;

		if (pathFollow.ProgressRatio >= 1.0f)
		{
		_Pass(); // removes enemy + pathfollow
		}
	}

	private void _Pass()
	{
		GameManager.instance.OnEnemyPassed(this);
		QueueFree();

	}

}