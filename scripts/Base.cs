using Godot;
using System;
using System.Runtime.Serialization;

public partial class Base : Node2D
{
	public AnimatedSprite2D healthBar;
	private int health = 10;
	private int damage = 1;
    public void OnIntered(Node2D body)
	{
		if (body is Enemy || body is Enemy2)
		{
			TakeDamage();
			if (health <= 9)
			{
			healthBar.Play("hp6");
			}
		}
	}
	public void TakeDamage()
	{
		health -= damage;
		
	}
}

