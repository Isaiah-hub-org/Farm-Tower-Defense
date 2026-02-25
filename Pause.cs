using Godot;
using System;


public partial class Pause : CanvasLayer
{
    private Control menu;

    public override void _Ready()
    {
        menu = GetNode<Control>("Control");
        menu.Visible = true;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("Pause"))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        bool isPaused = !GetTree().Paused;
        GetTree().Paused = isPaused;
        menu.Visible = isPaused;
    }

    
}