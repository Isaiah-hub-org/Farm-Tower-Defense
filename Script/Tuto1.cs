using Godot;
using System;

public partial class Tuto1 : Node2D
{
	public void OnPressed() {
		 GetTree().ChangeSceneToFile("res://Scenes/TUTO2.tscn");
		
	}
}
