using Godot;
using System;

public partial class EnemySpawner : Node2D
{
	[Export] public PackedScene EnemyScene;
	[Export] public Path2D Path;
	[Export] public float SpawnInterval = 3f;
	[Export] public float EnemySpeed = 50f;

	private Timer _spawnTimer;

	public override void _Ready()
	{
		if (EnemyScene == null || Path == null)
		{
			GD.PrintErr("EnemyScene or Path is not assigned!");
			return;
		}

		_spawnTimer = new Timer();
		_spawnTimer.WaitTime = SpawnInterval;
		_spawnTimer.OneShot = false;
		_spawnTimer.Autostart = true;

		AddChild(_spawnTimer);
		_spawnTimer.Timeout += SpawnEnemy;
	}

	private void SpawnEnemy()
	{
		GD.Print("Spawning enemy...");

		PathFollow2D pathFollow = new PathFollow2D();
		pathFollow.Loop = false;
		pathFollow.Progress = 0;

		Path.AddChild(pathFollow);

		Enemy enemy = EnemyScene.Instantiate<Enemy>();
		enemy.Speed = EnemySpeed;

		pathFollow.AddChild(enemy);
	}
}
