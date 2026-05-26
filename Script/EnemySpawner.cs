using Godot;
using System;

public partial class EnemySpawner : Node2D
{
	// Exported variables for enemy scenes, path, spawn interval, and wave durations
	[Export] public PackedScene EnemyScene;    
	[Export] public PackedScene Enemy2Scene;
	[Export] public PackedScene Enemy4Scene;
	[Export] public PackedScene Enemy3Scene;
	[Export] public PackedScene Enemy5Scene;
	[Export] public PackedScene TowerScene;
	[Export] public PackedScene Tower1Scene;
	
	[Export] public Path2D Path;               
	[Export] public float SpawnInterval = 1.5f;

	// Private variables for timers, UI elements, tower placement, and wave management
	private Timer _spawnTimer;
	private Timer _labelTimer;
	private Label _waveLabel;
	private Node2D _towerPreview;
	public Tower1 _towerToPlace;
	public bool _isbuilding;
	private Vector2 _cellOffset;
	private bool _towerHasValidPlacement;
	public TileMapLayer _groundTileMap;
	public float _cellRound;
	private int _currentWave = 0;
	private float _waveTimer = 0f;

	[Export] public float Wave1Duration =50f;
	[Export] public float Wave2Duration = 50f;
	[Export] public float Wave3Duration = 50f;
	[Export] public float Wave4Duration = 15f;
	[Export] public float Wave5Duration = 3f;

	// Called when the node enters the scene tree for the first time
	public override void _Ready()
	{
		_towerToPlace = GetNode<Tower1>("Tower1");
		
		_groundTileMap = GetNode<TileMapLayer>("TileMapLayer");
		_cellRound = _groundTileMap.TileSet.TileSize.X; 
		_cellOffset = new Vector2(_cellRound / 2, _cellRound / 2);
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
			WaitTime =5f,
			OneShot = true,
			Autostart = false
		};
		AddChild(_labelTimer);
		_labelTimer.Timeout += () => _waveLabel.Text = "";

		
		CallDeferred(nameof(StartWave1));


		SetIsBuilding(true);	


		Button towerButton = GetNode<Button>("CanvasLayer/UI/HBoxContainer/Sprite2D/Button");
		towerButton.Pressed += OnTowerButtonPressed;
		
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
			StartWave3();
		}
		else if (_currentWave == 3 && _waveTimer >= Wave3Duration)
		{
			StartWave4();
		}
		else if (_currentWave == 4 && _waveTimer >= Wave4Duration)
		{
			StartWave5();
		}
		else if (_currentWave == 5 && _waveTimer >= Wave5Duration)
		{
			EndWaves();
		}


		if (_isbuilding && _towerToPlace != null)
		{
			Vector2 mousePos = GetGlobalMousePosition();
			_towerToPlace.Position = _RoundPositionToTileMap(mousePos);
		}
	}
	// Methods for starting waves, ending waves, showing wave text, spawning enemies, handling input, and placing towers

	private void StartWave1()
	{
		_spawnTimer.Stop(); // ensure clean start

		_currentWave = 1;
		_waveTimer = 0f;

		_spawnTimer.Start();

		ShowWaveText("Wave 1 Starting!");
		
	}

	private void StartWave2()
	{
		_spawnTimer.Stop(); // stop old wave first

		_currentWave = 2;
		_waveTimer = 0f;

		_spawnTimer.Start();

		ShowWaveText("Wave 2 Starting!");
		
	}
	
	private void StartWave3()
	{
		_spawnTimer.Stop(); // stop old wave first

		_currentWave = 3;
		_waveTimer = 0f;

		_spawnTimer.Start();

		ShowWaveText("Wave 3 Starting!");
		
	}
	private void StartWave4()
	{
		_spawnTimer.Stop(); // stop old wave first

		_currentWave = 4;
		_waveTimer = 0f;

		_spawnTimer.Start();

		ShowWaveText("Wave 4 Starting!");
		
	}
	private void StartWave5()
	{
		_spawnTimer.Stop(); // stop old wave first

		_currentWave = 5;
		_waveTimer = 0f;

		_spawnTimer.Start();

		ShowWaveText("Wave 5 Starting!");
		
	}

	private void EndWaves()
	{
		_spawnTimer.Stop();
		_currentWave = 0;

		ShowWaveText("Wave 5 Finished!");
		
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
		else if (_currentWave == 3)
			SpawnEnemy3();
		else if (_currentWave == 4)
			SpawnEnemy4();
		else if (_currentWave == 5)
			SpawnEnemy5();
		 
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
		enemy2.Speed = 55f;

		pathFollow.AddChild(enemy2);
	}
	
	private void SpawnEnemy4()
	{
		PathFollow2D pathFollow = new PathFollow2D
		{
			Loop = false,
			Progress = 0,
			Rotates = false
		};
		Path.AddChild(pathFollow);

		Enemy4 enemy4 = Enemy4Scene.Instantiate<Enemy4>();
		enemy4.Speed = 20f;

		pathFollow.AddChild(enemy4);
	}
	private void SpawnEnemy3()
	{
		PathFollow2D pathFollow = new PathFollow2D
		{
			Loop = false,
			Progress = 0,
			Rotates = false
		};
		Path.AddChild(pathFollow);

		Enemy3 enemy3 = Enemy3Scene.Instantiate<Enemy3>();
		enemy3.Speed = 40f;

		pathFollow.AddChild(enemy3);
	}
	private void SpawnEnemy5()
	{
		PathFollow2D pathFollow = new PathFollow2D
		{
			Loop = false,
			Progress = 0,
			Rotates = false
		};
		Path.AddChild(pathFollow);

		Enemy5 enemy5 = Enemy5Scene.Instantiate<Enemy5>();
		enemy5.Speed = 10f;

		pathFollow.AddChild(enemy5);
	}


	public override void _Input(InputEvent @event)
	{
		if( @event is InputEventMouseButton eventMouseButton && eventMouseButton.ButtonIndex == MouseButton.Left && !eventMouseButton.Pressed)
		{
			if (_isbuilding && _towerToPlace != null)
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
			
			
			Vector2 mousePos = GetGlobalMousePosition();
			

			
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
		Vector2 snappedPos = _RoundPositionToTileMap(GetGlobalMousePosition());
		Tower1 newTower = Tower1Scene.Instantiate<Tower1>();
		newTower.GlobalPosition = snappedPos;
		AddChild(newTower);
		SetIsBuilding(false);
	}
		
	private void OnTowerButtonPressed()
	{
		if(GameManager.instance.CanBuyTower())
		{
			SetIsBuilding(true);

		}
		
	}

	private Vector2 _RoundPositionToTileMap(Vector2 position)
	{
		float x = Mathf.Round(position.X / _cellRound) * _cellRound + _cellOffset.X;
		float y = Mathf.Round(position.Y / _cellRound) * _cellRound + _cellOffset.Y;
		return new Vector2(x, y);
	}
		
		
}
