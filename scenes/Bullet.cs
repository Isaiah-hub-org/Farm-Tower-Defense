using Godot;

public partial class Bullet : Area2D
{
	[Export] public float Speed = 500f;

	public override void _Process(double delta)
	{
		// Move in the direction the bullet is facing
		 Position += Transform.X * Speed * (float)delta;
	}

	// Connect the "body_entered" signal in the editor to this function
	private void OnEnemyEntered(Node2D body)
	{
		if (body.IsInGroup("enemies"))
		{
			 QueueFree();
			
		}
	}
}
