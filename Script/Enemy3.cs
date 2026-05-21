using Godot;
using System;

public partial class Enemy3 : CharacterBody2D
{
	[Export] public float Speed = 40f;
	[Export] public int HP = 2;
	[Export] public int health = 50;
	[Export] public int reward = 15;
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
	public void TakeDamage(int damage)
	{
		health -= damage;
		if (health <= 0)
		{
			Die();
		}
		
		
		
	}
	private void Die()
	{
		GameManager.instance.OnEnemyDied(this);
		QueueFree();
	}
}
