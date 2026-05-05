using Godot;
using System;
using UIProject.Script;

public partial class Enemy : CharacterBody2D
{
	[Export] public float Speed = 10f;
	[Export] public int HP = 1;
	[Export] public int health = 1;
	private PathFollow2D pathFollow;
	private ScoreKeeper scoreKeeper;

	public override void _Ready()
	{
		pathFollow = GetParent<PathFollow2D>();
		scoreKeeper = GetNode<ScoreKeeper>("/root/ScoreKeeper");
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
	public void TakeDamage(int damage)
	{
		health -= damage;
		if (health <= 0)
		{
			scoreKeeper.AddPoints(2);
			QueueFree();
		}
	}

}
