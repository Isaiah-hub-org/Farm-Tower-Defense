using Godot;
using System;

public partial class EnemySpawner : Node2D
{
	[Export] public PackedScene EnemyScene;    
	[Export] public PackedScene Enemy2Scene;
	[Export] public PackedScene TowerScene;
	[Export] public Path2D Path;               
	[Export] public float SpawnInterval = 2.5f;   

	private Timer _spawnTimer;
	private Timer _labelTimer;
	private Label _waveLabel;
	private Node2D _towerPreview;
	public Tower3 _towerToPlace;
	public bool _isbuilding;

	public TileMapLayer _tileMap;
	//public float _cellRound;
	private int _currentWave = 0;
	private float _waveTimer = 0f;

	[Export] public float Wave1Duration = 20f;  
	[Export] public float Wave2Duration = 20f;  

	public override void _Ready()
	{
		_towerToPlace = GetNode<Tower3>("Tower3");
		_tileMap = GetNode<TileMapLayer>("TileMapLayer");
		//_cellRound = _tileMap.TileSet.TileSize.X; 

		_waveLabel = GetNode<Label>("WaveLabel");
		_waveLabel.Text = "";
		_isbuilding = true;
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
			WaitTime = 10f,
			OneShot = true,
			Autostart = false
		};
		AddChild(_labelTimer);
		_labelTimer.Timeout += () => _waveLabel.Text = "";

		
		CallDeferred(nameof(StartWave1));


		SetIsBuilding(true);	


		Node towerButtonParent = GetNode<Node>("CanvasLayer/UI/HBoxContainer/Sprite2D/Button");
		for (int i = 0; i < towerButtonParent.GetChildCount(); i++)
		{
			Control c = (Control)towerButtonParent.GetChild(i);
			c.Connect("pressed", Callable.From(OnTowerButtonPressed));
		}


		
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


		if (_isbuilding)
		{
			Vector2 mousePos = GetGlobalMousePosition();
			
			Vector2I cell = _tileMap.LocalToMap(mousePos);
			Vector2 snappedPos = _tileMap.MapToLocal(cell);
			_towerToPlace.Position = snappedPos;

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
		_waveTimer = 10f;

		_spawnTimer.Start();

		ShowWaveText("Wave 2 Started!");
		GD.Print("Wave 2 started!");
	}

	private void EndWaves()
	{
		_spawnTimer.Stop();
		_currentWave = 0;

		ShowWaveText("Wave 2 Finished!");
		ShowWaveText("All waves finished!");
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
			Progress = 0,
			Rotates = false
		};
		Path.AddChild(pathFollow);

		Enemy enemy = EnemyScene.Instantiate<Enemy>();
		enemy.Speed = 30f;

		pathFollow.AddChild(enemy);
	}

	private void SpawnEnemy2()
	{
		PathFollow2D pathFollow = new PathFollow2D
		{
			Loop = false,
			Progress = 0,
			Rotates = false
		};
		Path.AddChild(pathFollow);

		Enemy2 enemy2 = Enemy2Scene.Instantiate<Enemy2>();
		enemy2.Speed = 40f;

		pathFollow.AddChild(enemy2);
	}


	public override void _Input(InputEvent @event)
	{
		if( @event is InputEventMouseButton eventMouseButton && eventMouseButton.ButtonIndex == MouseButton.Left && !eventMouseButton.Pressed)
		{
			if (_isbuilding)
			{
				if (GameManager.instance.BuyTower())
				{
					PlaceTower();
				}
				else
				{
					GD.Print("Not enough coins to place tower!");
				}
			}
			else
			{
				_isbuilding = true;
			}
		}
		else if (@event is InputEventMouseMotion)
		{
			if (_isbuilding)
			{
				Vector2 mousePos = GetGlobalMousePosition();
				_towerToPlace.Position = mousePos;
			}
		}
	}

	public void SetIsBuilding(bool value)
	{
		_isbuilding = value;
		if (_isbuilding)
		{
			((CanvasItem)_towerToPlace).Show();
		}
		else
		{
			((CanvasItem)_towerToPlace).Hide();
		}
	}
	void PlaceTower(){
		if (_isbuilding)
		{
			_towerToPlace = null;
		}
	}
		
	private void OnTowerButtonPressed()
	{
		if(!GameManager.instance.CanBuyTower())
		{
			SetIsBuilding(true);
		}
		
	}
		
		
}
