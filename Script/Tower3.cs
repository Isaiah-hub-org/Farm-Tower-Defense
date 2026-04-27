using Godot;
using System;
public partial class Tower3 : Node2D
{
	
	[Export] public PackedScene BulletPrefab;
	[Export] public float FireRate = 1.0f;
	private int attackDamage = 1;
	private float attackSpeed = 1.0f;
	private float attackDelay;
	private ShaderMaterial _spriteMaterial;
	protected Node2D targetEnemy = null;
	public override void _Ready()
	{
		_spriteMaterial = GetNode<Sprite2D>("Sprite2D").Material as ShaderMaterial;
		Area2D area = GetNode<Area2D>("EnemyDetectionArea");
		area.BodyEntered += OnEnemyEntered;
		area.BodyExited += OnEnemyExited;
		
		Timer timer = GetNode<Timer>("Timer");
		timer.WaitTime = FireRate;
		timer.Timeout += OnTimerTimeout;
		timer.Start();

		attackDelay = FireRate;
		
	}
	
	public override void _Process(double delta)
	{
		if (targetEnemy != null)
		{
			LookAt(targetEnemy.GlobalPosition);
			if (attackDelay > 0)
			{
				attackDelay -= (float)delta;
			}
			else
			{
				Shoot();
				attackDelay = FireRate;
			}
		}
	}
	
	private void OnEnemyEntered(Node body)
	{
		if (body.IsInGroup("enemies"))
		{
			GD.Print("detects");
			targetEnemy = body as Node2D;
			
			Shoot();
		}
	}
	private void OnEnemyExited(Node body)
	{
		if (body == targetEnemy)
		{
			targetEnemy = null;
		}
	}
	
	private void OnTimerTimeout()
	{
		if(targetEnemy != null)
		{
			Shoot();
		}	
	}
	private void Shoot()
	{
		if (BulletPrefab == null)
		{
			GD.Print("Bullet prefab missing");
			return;
		}
		
		Bullet bullet = BulletPrefab.Instantiate<Bullet>();
		Marker2D marker = GetNode<Marker2D>("Marker2D");
		bullet.GlobalPosition = marker.GlobalPosition;
		bullet.Rotation = Rotation;
		GetTree().CurrentScene.AddChild(bullet);
	}

	
}
