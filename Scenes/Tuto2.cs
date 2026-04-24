using Godot;
using System;

public partial class Tuto2 : Node2D
{
	public void OnPressed() {
		 GetTree().ChangeSceneToFile("res://Scenes/DEBUG.tscn");
		
	}
}
