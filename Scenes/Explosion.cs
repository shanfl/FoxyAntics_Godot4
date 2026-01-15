using Godot;
using System;
using System.Collections;

public partial class Explosion : AnimatedSprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AnimationFinished += OnAnimationFinished;
	}

    private void OnAnimationFinished()
    {
        QueueFree();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
