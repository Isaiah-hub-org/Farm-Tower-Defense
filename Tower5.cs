using Godot;
using System.Collections.Generic;

public partial class Tower5 : Node2D
{
	[Export] public PackedScene ArrowScene;
	[Export] public float FireRate = 0.7f;

	private Area2D range;
	private Timer fireTimer;

	private List<Node2D> enemies = new();
	private Node2D currentTarget;

	public override void _Ready()
	{
		range = GetNode<Area2D>("Range");
		fireTimer = GetNode<Timer>("FireTimer");

		fireTimer.WaitTime = FireRate;
		fireTimer.Timeout += Shoot;

		range.BodyEntered += OnBodyEntered;
		range.BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node body)
	{
		if (body is Node2D enemy && body.IsInGroup("Enemy"))
		{
			enemies.Add(enemy);
			currentTarget = enemy;
		}
	}

	private void OnBodyExited(Node body)
	{
		if (body is Node2D enemy)
		{
			enemies.Remove(enemy);

			if (currentTarget == enemy)
				currentTarget = enemies.Count > 0 ? enemies[0] : null;
		}
	}

	private void Shoot()
	{
		if (currentTarget == null)
			return;

		var arrow = ArrowScene.Instantiate<Arrow>();
		GetTree().CurrentScene.AddChild(arrow);

		arrow.GlobalPosition = GlobalPosition;
		arrow.SetTarget(currentTarget.GlobalPosition);
	}
}
