using Godot ;
using System;

public partial class parallax_mover : CharacterBody2D
{
    
    [Export]
    public float Speed { get; set; } = 100.0f; 

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

       
        velocity.X = -Speed; 

       
        Velocity = velocity;
        MoveAndSlide();
    }
}