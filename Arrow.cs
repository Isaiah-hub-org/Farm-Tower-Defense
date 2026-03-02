using Godot;

public partial class Arrow : Node2D
{
	[Export] public float Speed = 500f;

	private Vector2 direction;

	public void SetTarget(Vector2 targetPosition)
	{
		direction = (targetPosition - GlobalPosition).Normalized();
		Rotation = direction.Angle();
	}

	public override void _Process(double delta)
	{
		GlobalPosition += direction * Speed * (float)delta;
	}
}
