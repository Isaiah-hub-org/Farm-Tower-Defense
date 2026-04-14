using Godot;
using System;

public partial class TransitionScene : Control
{
    private AnimationPlayer _anim;
	private AnimationPlayer _anim2;
    private Timer _timer;

    public override void _Ready()
    {
        _anim = GetNode<AnimationPlayer>("AnimationPlayer");
        _anim2 = GetNode<AnimationPlayer>("AnimationPlayer2");
        _timer = GetNode<Timer>("Timer");

        _timer.Timeout += OnTimerTimeout;
    }

    private void OnTimerTimeout()
    {
        
        _anim.Play("fade_out");
		_anim2.Play("fade_in");
        
        _anim.AnimationFinished += OnFadeFinished;
		_anim2.AnimationFinished += OnFadeFinished;
    }

    private void OnFadeFinished(StringName animName)
    {
        if (animName == "fade_out")
        {
            GetTree().ChangeSceneToFile("res://Map.tscn");
        }
		if (animName == "fade_in")
		{
			GetTree().ChangeSceneToFile("res://Scenes/settings.tscn");
		}
    }
}
