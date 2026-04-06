using Godot;
using System;




public partial class Enemy3 : CharacterBody2D
{
	public int MaxHealth = 50; 
	public int CurrentHealth; 
	
	[Signal]
	public delegate void HealthChangedEventHandler(int oldHealth, int newHealth);
	
	public virtual void _Ready() {
		CurrentHealth = MaxHealth;
		//enemyHealthBar = GetNode<ProgressBar>("enemyHealthBar");
		//enemyHealthBar.MaxValue = MaxHealth;
		//enemyHealthBar.Value = CurrentHealth;
		
	}

	public virtual void TakeDamage(int damage) {
		EmitSignal(SignalName.HealthChanged, CurrentHealth, CurrentHealth - damage);
		CurrentHealth -= damage;
	}
}

	public partial class Enemy3 : CharacterBody2D [Export] public float Speed = 95f;

	private PathFollow2D pathFollow;

	public override void _Ready()
	{
		pathFollow = GetParent<PathFollow2D>();
	}

	public override void _PhysicsProcess(double delta)
	{
		var oldProgressRatio = pathFollow.ProgressRatio;
		
		pathFollow.Progress += Speed * (float)delta;

		if (pathFollow.ProgressRatio < oldProgressRatio)
		{
			pathFollow.QueueFree(); 
			QueueFree();
	}
}
