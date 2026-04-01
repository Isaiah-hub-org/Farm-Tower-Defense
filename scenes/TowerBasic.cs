using Godot;
using System;

public partial class TowerBasic : Tower3
{
	public override void _Process(double delta)
	{
		if (enemies.Count > 0)
		{
			var bullet = GetNode<Node2D>("bullet");
			bullet.LookAt(enemies[0].GlobalPosition);
			bullet.Rotation -= Mathf.pi / 2;
		}
	}
}
