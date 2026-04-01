using Godot;
using System;
using System.Collections.Generic;

public partial class Tower3 : Node2D
{
	private List<Area2D> enemies = new List<Area2D>();
	
	public override void _Process(double delta)
	{
		if (enemies.Count > 0)
		{
			var bullet = GetNode<Node2D>("bullet");
			bullet.LookAt(enmies[0].GlobalPosition);
			bullet.Rotation -= Mathf.pi / 2;
		}
	}
	
	private void OnEnemyDetectionAreaAreaEntered(Area2D area)
	{
		enemies.Add(area);
	}
	
	private void OnEnemyDetectionAreaAreaExited(Area2D area)
	{
		if (enemies.Contains(area))
		{
			enemies.Remove(area);
		}
	}
}
