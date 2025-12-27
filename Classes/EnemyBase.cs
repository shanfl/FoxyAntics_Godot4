using Godot;
using System;
using System.Collections;
using System.ComponentModel;

[GlobalClass]
public partial class EnemyBase : CharacterBody2D
{

	[Export] protected int _points = 1;
	[Export] protected float _speed = 10;// _sprite2D;

	//[Export] 
	protected Player _player;	
	protected float _gravity = 600.0f;

	[Export] protected VisibleOnScreenNotifier2D _visibleOnScreenNotifier2D;
	[Export] protected AnimatedSprite2D _animatedSprite2D;
	[Export] protected Area2D _hitBoxArea2D;

	[Export] private float _yFallOff = 100.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		_player = GetTree().GetFirstNodeInGroup(Player.GroupName) as Player;
		_visibleOnScreenNotifier2D.ScreenExited += OnScreenExited;
		_visibleOnScreenNotifier2D.ScreenEntered += OnScreenEntered;

		_hitBoxArea2D.BodyEntered += OnHitBoxBodyEntered;

	}

	private void FallenOff()
	{
		if(GlobalPosition.Y > _yFallOff)
		{
			//SetPhysicsProcess(false);
			QueueFree();
		}
	}


	// 敌人身上既有Area2d又有hitbox,这2者不做设置也会发生碰撞,
    protected virtual void OnHitBoxBodyEntered(Node2D body)
    {
       	GD.Print("Enemy Hit by: " + body.Name + " _hitBoxArea2D: " + _hitBoxArea2D.Name);
		Die();
    }

    protected virtual void Die()
    { 
        SignalManager.EmitOnEnemyHit(_points, GlobalPosition);
		SignalManager.EmitOnCreateObject(GlobalPosition, (int)GameObjectType.Explosion);
		SignalManager.EmitOnCreateObject(GlobalPosition, (int)GameObjectType.Pickup);
		SetPhysicsProcess(false);
		QueueFree();

    }
 
    protected virtual void OnScreenEntered()
    {
 
    }


    protected virtual void OnScreenExited()
    {
       
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
	{
		//FallenOff();
	}
}
