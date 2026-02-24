using Godot;
using System;

public partial class Enemy2 : CharacterBody2D
{
	[Export] public float Speed = 100f; // horizontal speed

	public override void _PhysicsProcess(double delta)
	{
		Velocity = new Vector2(Speed, 0);
		MoveAndSlide();
	}
}
