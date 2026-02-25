using Godot;
using System;

public partial class Arrow : Area2D
{
	public void BulletHitSomething(Node2D body) {
		if(body.GetParent() is Arrow) {
			body.GetParent().QueueFree();
		} else {
			GD.Print(body.GetParent());
		}
	}
}
