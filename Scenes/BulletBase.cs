using Godot;
using System;

public partial class BulletBase : Area2D
{

	private Vector2 _direction = Vector2.Right;
	private double _lifeSpane = 20.0f;
	private double _lifeTimer = 0.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;

		//Setup(new Vector2(1,-1),20.0f,140.0f);
	}

	private void OnAreaEntered(Area2D area)
	{
		GD.Print("Bullet Hit Area: " + area.Name);
		QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CheckExpired(delta);

		// update position
		Position += _direction * (float)delta;
	}

	public void Setup(Vector2 direction, double lifeSpane,float speed)
	{
		_direction = direction.Normalized() * speed;
		_lifeSpane = lifeSpane;
	}

	public void CheckExpired(double delta)
	{
		_lifeTimer += delta;
		if (_lifeTimer > _lifeSpane)
		{
			QueueFree();
		}
	}
}
