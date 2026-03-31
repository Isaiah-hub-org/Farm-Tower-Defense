using Godot;
using System;

public partial class Tower3 : Node2D
{
	public override void _PhysicsProcess(double delta)
	{
		Turn();
	}
	
	private void Turn()
	{
		Vector2 enemyPosition = GetGlobalMousePosition();
		GetNode<Node2D>("$bullet").LookAt(enemyPosition);
	}
}
