using Godot;
using System;



public partial class Menu : Control
{
	public void OnPlayPressed() {
		 GetTree().ChangeSceneToFile("res://Scenes/Map.tscn");
		
	}
	public void OnSettingsPressed() {
		GetTree().ChangeSceneToFile("res://Scenes/settings.tscn");
	}
	public void OnLoadPressed(){
		
	}
	public void OnExitPressed(){
		 GetTree().Quit();
		
	}
	//quit by pressing Esc
	public override void _Input(InputEvent @event)
	{
	if (@event.IsActionPressed("ui_cancel"))
		{
			GetTree().Quit();
		}
	}
	
}
