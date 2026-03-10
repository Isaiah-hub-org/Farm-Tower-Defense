using Godot;
using System;

public partial class Base : Node2D
{
    public int CurrentHealth;
    public int MaxHealth = 100;
    public int damage = 10;
    private ProgressBar BaseBar;
    private AnimatedSprite2D Basehit;





    public override void _Ready(){
        CurrentHealth = MaxHealth;
		BaseBar = GetNode<ProgressBar>("ProgressBar");
		BaseBar.MaxValue = MaxHealth;
		BaseBar.Value = CurrentHealth;
    }
	
	
    public void OnIn(int damage)
	{
		GD.Print("enemy hit");
		CurrentHealth -= damage;
		CurrentHealth = Mathf.Max(CurrentHealth,0);
		BaseBar.Value = CurrentHealth;
		if (CurrentHealth <= 90)
		{
			Basehit.Play("BaseHit");
		}
		if (CurrentHealth <= 0)
		{
			QueueFree();
		}
    }
}
