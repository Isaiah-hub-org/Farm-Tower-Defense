using Godot;
using System;

public partial class Bullet : Area2D
{
	public void BulletHitSomething(Node2D body) {
		if(body.GetParent() is Enemy) {
			body.GetParent().QueueFree();
		} else {
			GD.Print(body.GetParent());
		}
	}
}
