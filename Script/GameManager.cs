using Godot;
using System;

public partial class GameManager : Node
{
	public static GameManager instance;
	private Label _coinsLabel;
	private Label _livesLabel;

	private int _coins = 0;
	private int _lives = 50;
	
	private const int _Tower_Cost = 20;
	public override void _Ready()
	{
		instance = this;

		_coinsLabel = GetNode<Label>("CanvasLayer/UI/VBoxContainer/Coins/Sprite2D/Label");
		_livesLabel = GetNode<Label>("CanvasLayer/UI/VBoxContainer/Lives/Sprite2D/Label");
		_UpdateUI();
	}

	private void _UpdateUI()
	{
		_coinsLabel.Text = $"{_coins}";
		_livesLabel.Text = $"{_lives}";
	}

	public bool CanBuyTower()
	{
		return _coins >= _Tower_Cost;
	}

	public bool BuyTower()
	{
		if (_coins < _Tower_Cost)return false;
		{
			_coins -= _Tower_Cost;
			_UpdateUI();
			return true;
		}
		
	}

	// When an enemy passes through the end of the path, the player's lives are reduced by the enemy's HP and the UI is updated
	public void OnEnemyPassed(Enemy enemy)
	{
		_lives -= enemy.HP;
		_UpdateUI();
	}
	
	
	public void OnEnemyPassed(Enemy2 enemy2)
	{
		_lives -= enemy2.HP;
		_UpdateUI();
	}
	
	public void OnEnemyPassed(Enemy4 enemy4)
	{
		_lives -= enemy4.HP;
		_UpdateUI();
	}
	public void OnEnemyPassed(Enemy3 enemy3)
	{
		_lives -= enemy3.HP;
		_UpdateUI();
	}


	// The reward for killing an enemy is added to the player's coins and the UI is updated




	public void OnEnemyDied(Enemy3 enemy3)
	{
		_coins += enemy3.reward; // reward for Enemy3
		_UpdateUI();
	}

	public void OnEnemyDied(Enemy enemy)
	{
		_coins += enemy.reward; // reward for Enemy
		_UpdateUI();
	}
	public void OnEnemyDied(Enemy2 enemy2)
	{
		_coins += enemy2.reward; // reward for Enemy2
		_UpdateUI();
	}
	public void OnEnemyDied(Enemy4 enemy4)
	{
		_coins += enemy4.reward; // reward for Enemy4
		_UpdateUI();
	}
}
