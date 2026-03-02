using Godot;
using System;

public partial class EnemySpawner : Node2D
{
	[Export] public PackedScene EnemyScene;    
	[Export] public PackedScene Enemy2Scene;  
	[Export] public Path2D Path;               
	[Export] public float SpawnInterval = 3f;   

	private Timer _spawnTimer;
	private Timer _labelTimer;
	private Label _waveLabel;
	
	private int _currentWave = 0;
	private float _waveTimer = 0f;

	[Export] public float Wave1Duration = 23f;  
	[Export] public float Wave2Duration = 25f;  

	public override void _Ready()
	{
		if (EnemyScene == null || Enemy2Scene == null || Path == null)
		{
			GD.PrintErr("EnemyScene, Enemy2Scene, or Path is not assigned!");
			return;
		}
		
		_waveLabel = GetNode<Label>("WaveLabel");
		_waveLabel.Text = "";

		// Spawn Timer
		_spawnTimer = new Timer
		{
			WaitTime = SpawnInterval,
			OneShot = false,
			Autostart = false
		};
		AddChild(_spawnTimer);
		_spawnTimer.Timeout += SpawnEnemy;

		// Label Timer
		_labelTimer = new Timer
		{
			WaitTime = 2f,
			OneShot = true,
			Autostart = false
		};
		AddChild(_labelTimer);
		_labelTimer.Timeout += () => _waveLabel.Text = "";

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
			EndWaves();
		}
	}

	private void StartWave1()
	{
		_spawnTimer.Stop(); // ensure clean start

		_currentWave = 1;
		_waveTimer = 0f;

		_spawnTimer.Start();

		ShowWaveText("Wave 1 Started!");
		GD.Print("Wave 1 started!");
	}

	private void StartWave2()
	{
		_spawnTimer.Stop(); // stop old wave first

		_currentWave = 2;
		_waveTimer = 0f;

		_spawnTimer.Start();

		ShowWaveText("Wave 2 Started!");
		GD.Print("Wave 2 started!");
	}

	private void EndWaves()
	{
		_spawnTimer.Stop();
		_currentWave = 0;

		ShowWaveText("Wave 2 Finished!");
		GD.Print("All waves finished!");
	}

	private void ShowWaveText(string text)
	{
		_waveLabel.Text = text;
		_labelTimer.Start();
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
		PathFollow2D pathFollow = new PathFollow2D
		{
			Loop = false,
			Progress = 0
		};
		Path.AddChild(pathFollow);

		Enemy2 enemy2 = Enemy2Scene.Instantiate<Enemy2>();
		enemy2.Speed = 50f;

		pathFollow.AddChild(enemy2);
	}
}
