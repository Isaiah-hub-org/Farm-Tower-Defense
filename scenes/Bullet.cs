using Godot;

public partial class Bullet : Area2D
{
	[Export] public float Speed = 500f;
	
	private bool hasHit = false;
	
	public override void _Process(double delta)
	{
		 Position += Transform.X * Speed * (float)delta;
	}

	private void OnEnemyEntered(Node2D body)
	{
		GD.Print("hit");
		if (hasHit) return;
		if (body.IsInGroup("enemies"))
		{
			hasHit = true;
			
			QueueFree();
		}
	}
}
