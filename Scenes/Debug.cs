using Godot;
using System;

public partial class Debug : Node2D
{
	public void OnPressed() {
		 GetTree().ChangeSceneToFile("res://Scenes/Map.tscn");
		
	}
}
