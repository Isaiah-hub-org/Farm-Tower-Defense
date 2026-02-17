using Godot;
using System;

public partial class EnemySpawner : Node2D
{
	[Export] public PackedScene EnemyScene; // The enemy to spawn
	[Export] public Path2D Path;             // The path enemies will follow
	[Export] public float SpawnInterval = 2f;
	[Export] public float EnemySpeed = 50f;

	private Timer _spawnTimer;

	public override void _Ready()
	{
		// Create a repeating timer
		_spawnTimer = new Timer();
		_spawnTimer.WaitTime = SpawnInterval;
		_spawnTimer.OneShot = false;
		AddChild(_spawnTimer);

		// Call SpawnEnemy() every time the timer finishes
		_spawnTimer.Timeout += SpawnEnemy;
		_spawnTimer.Start();
	}

	private void SpawnEnemy()
	{
		

		// 1) Create a PathFollow2D node
		PathFollow2D pathFollow = new PathFollow2D();
		pathFollow.Loop = false;
		pathFollow.Progress = 0; // Start at beginning of path

		// 2) Attach PathFollow2D to Path2D
		Path.AddChild(pathFollow);

		// 3) Create the enemy
		Enemy enemy = EnemyScene.Instantiate<Enemy>();
		enemy.Speed = EnemySpeed;

		// 4) Put the enemy on the path
		pathFollow.AddChild(enemy);
	}
}
