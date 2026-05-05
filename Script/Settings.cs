using Godot;
using System;

public partial class Settings : Control
{
    private HSlider _slider;
    private AudioStreamPlayer2D _audio;

    public override void _Ready()
    {
        _slider = GetNode<HSlider>("Audio");
        _audio = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");

        
        _slider.MinValue = -40; 
        _slider.MaxValue = 0;  
        _slider.Value = _audio.VolumeDb;

        // Connect signal
        _slider.ValueChanged += OnSliderValueChanged;
    }

    private void OnSliderValueChanged(double value)
    {
        _audio.VolumeDb = (float)value;
    }

	public void OnBackPressed() {
		 GetTree().ChangeSceneToFile("res://Scenes/parallax_background.tscn");
	}
	
	
}
