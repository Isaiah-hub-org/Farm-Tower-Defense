using Godot;
using Godot.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

public partial class Tower5 : Node2D
{
	[Export] public PackedScene ArrowScene;
	[Export] public float FireRate = 0.7f;
	private bool ReadyToFire = true;
	
	[Export] int damage = 3;
	[ExportCategory("Node Connections")]
	Timer timer;
	private Area2D detectionZone;
	
	public List<Enemy> enemiesInRange;
	
 
	public override void _Ready()
	{
		enemiesInRange = new List<Enemy>();
		detectionZone = GetNode<Area2D>("DetectionZone");
		timer = GetNode<Timer>("Timer");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (HasEnemiesInRange() && ReadyToFire)
		{
			UpdateEnemies();
			var enemy = ChooseEnemy(enemiesInRange);
			Attack(enemy);
		}
	}

	public bool HasEnemiesInRange() {
		var enemies = detectionZone.GetOverlappingBodies().Where(x => x is Enemy);
		return enemies.Count() > 0;
	}

	public void TimerExpired() {
		ReadyToFire = true;
	}

	public void Attack(Enemy enemy) {
		ReadyToFire = false;
		var arrow = ArrowScene.Instantiate<Arrow>();

		timer.Start();
	}

	private void UpdateEnemies() {
		var enemies = detectionZone.GetOverlappingBodies().Where(x => x is Enemy);
		enemiesInRange = enemies.Select(x => (Enemy) x).ToList();
	}

	private Enemy ChooseEnemy(List<Enemy> enemies) {
		int index = Random.Shared.Next(enemies.Count);
		Enemy randomEnemy = enemies[index];
		return randomEnemy;
	}


}
