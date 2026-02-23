using Godot;
using System;



public partial class Menu : Control
{
	public void OnPlayPressed() {
		 GetTree().ChangeSceneToFile("");
		
	}
	public void OnSettingsPressed() {
		GetTree().ChangeSceneToFile("res://Scenes/settings.tscn");
	}
	public void OnLoadPressed(){
		
	}
    public void OnExitPressed(){
		
	}
	
}