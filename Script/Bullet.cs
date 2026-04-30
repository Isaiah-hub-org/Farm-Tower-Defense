using Godot;

public partial class Bullet : Area2D
{
	public int damage = 10;
	[Export] public float Speed = 500f;
	
	private bool hasHit = false;
	
	public override void _Process(double delta)
	{
		 Position += Transform.X * Speed * (float)delta;
	}

	private void OnEnemyEntered(Node2D body)
	{
		if (body.IsInGroup("enemies"))
		{
			hasHit = true;
			if (body is Enemy enemy)
			{
				enemy.TakeDamage(damage);
				QueueFree();
			}
			else if (body is Enemy2 enemy2)
			{
				enemy2.TakeDamage(damage);
				QueueFree();
			}
		}
	}
}
