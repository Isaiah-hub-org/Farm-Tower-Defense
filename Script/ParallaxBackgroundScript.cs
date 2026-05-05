using Godot;
using System;

public partial class ParallaxBackgroundScript : ParallaxBackground
{
    public override void _Process(double delta)
    {
        ScrollBaseOffset -= new Vector2(40f * (float)delta, 0);
    }
}