using Godot;
using System;
public partial class GameManager : Node
{
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            TogglePause();
            
        }
    }

    private void TogglePause()
    {
        GetTree().Paused = !GetTree().Paused;
    }
}