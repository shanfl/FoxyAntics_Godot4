using Godot;
using System;

public partial class BallSpikes : PathFollow2D
{
	[Export] private float _speed = 50.0f;
	[Export] private float _spinSpeed = 445.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Progress += _speed * (float)delta;
		RotationDegrees += _spinSpeed * (float)delta;
	}
}
