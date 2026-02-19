using Godot;
using System;

public partial class EnemySpawner : Node2D
{
	[Export] public PackedScene EnemyScene;    
	[Export] public PackedScene Enemy2Scene;  
	[Export] public Path2D Path;               
	[Export] public float SpawnInterval = 3f;   

	private Timer _spawnTimer;

	private int _currentWave = 0;
	private float _waveTimer = 0f;

	[Export] public float Wave1Duration = 20f;  
	[Export] public float Wave2Duration = 20f;  

	public override void _Ready()
	{
		if (EnemyScene == null || Enemy2Scene == null || Path == null)
		{
			GD.PrintErr("EnemyScene, Enemy2Scene, or Path is not assigned!");
			return;
		}

		
		_spawnTimer = new Timer
		{
			WaitTime = SpawnInterval,
			OneShot = false,
			Autostart = true
		};
		AddChild(_spawnTimer);
		_spawnTimer.Timeout += SpawnEnemy;

		StartWave1();
	}

	public override void _Process(double delta)
	{
		_waveTimer += (float)delta;

		if (_currentWave == 1 && _waveTimer >= Wave1Duration)
		{
			StartWave2();  
		}
		else if (_currentWave == 2 && _waveTimer >= Wave2Duration)
		{
			_spawnTimer.Stop();
			GD.Print("Wave 2 finished!");
		}
	}

	
	private void StartWave1()
	{
		_currentWave = 1;
		_waveTimer = 0f;
		GD.Print("Wave 1 started!");
	}

	private void StartWave2()
	{
		_currentWave = 2;
		_waveTimer = 0f;
		GD.Print("Wave 2 started!");
	}

	
	private void SpawnEnemy()
	{
		if (_currentWave == 1)
			SpawnPathEnemy();
		else if (_currentWave == 2)
			SpawnEnemy2();
	}

	
	private void SpawnPathEnemy()
	{
		PathFollow2D pathFollow = new PathFollow2D
		{
			Loop = false,
			Progress = 0
		};
		Path.AddChild(pathFollow);

		Enemy enemy = EnemyScene.Instantiate<Enemy>();
		enemy.Speed = 50f; 

		pathFollow.AddChild(enemy);
	}

	
	private void SpawnEnemy2()
	{
		Enemy2 enemy = Enemy2Scene.Instantiate<Enemy2>();
		enemy.Speed = 100f;

		
		float randomY = (float)GD.RandRange(0, GetViewportRect().Size.Y);
		enemy.GlobalPosition = new Vector2(0, randomY); 

		AddChild(enemy);
	}
}
