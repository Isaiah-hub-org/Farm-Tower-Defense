using Godot;
using System;

public partial class GameManager : Node
{
	public static GameManager instance;
	private Label _coinsLabel;
	private Label _livesLabel;

	private int _coins = 60;
	private int _lives = 10;
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

}
