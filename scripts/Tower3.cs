using Godot;
using System;



public partial class Tower3 : Node2D
{
	
	[Export] public PackedScene BulletPrefab;
	[Export] public float FireRate = 1.0f;

	protected Node2D targetEnemy = null;
	public override void _Ready()
	{
		Area2D area = GetNode<Area2D>("EnemyDetectionArea");
		area.BodyEntered += OnEnemyEntered;
		area.BodyExited += OnEnemyExited;
		
	}
	
	public override void _Process(double delta)
	{
		if (targetEnemy != null)
		{
			LookAt(targetEnemy.GlobalPosition);
		}
	}
	
	private void OnEnemyEntered(Node body)
	{
		if (body.IsInGroup("enemies"))
		{
			GD.Print("detects");
			targetEnemy = body as Node2D;
		}
	}
	private void OnEnemyExited(Node body)
	{
		if (body == targetEnemy)
		{
			targetEnemy = null;
		}
	
	}
}
