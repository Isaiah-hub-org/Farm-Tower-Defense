using Godot;
using System;




public partial class Enemy3 : CharacterBody2D
{
	public int MaxHealth = 50; 
	public int CurrentHealth; 
	[Export] public float Speed = 95f;
	private PathFollow2D pathFollow;
	protected ProgressBar HealthBar;
	
	[Signal]
	public delegate void HealthChangedEventHandler(int oldHealth, int newHealth);
	
	public virtual void _Ready() {
		CurrentHealth = MaxHealth;
	
		HealthBar = GetNode<ProgressBar>("enemyHealthBar");
		HealthBar.MaxValue = MaxHealth;
		HealthBar.Value = CurrentHealth;
		
		pathFollow = GetParent<PathFollow2D>();
	}

	public virtual void TakeDamage(int damage) {
		EmitSignal(SignalName.HealthChanged, CurrentHealth, CurrentHealth - damage);
		CurrentHealth -= damage;
		HealthBar.Value -= damage;
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
}
