using Godot;
using System;
using System.ComponentModel;

public partial class Boss : Node2D
{
	[Export] private AnimationTree _animationTree;
	[Export] private Area2D _trigger;

	[Export] private Area2D _hitBox;
	[Export] private Node2D _visual;

	[Export] private int _lives = 2;
	[Export] private int _points = 5;

	private bool _invincible = false;

	private Tween _hitTween;

	private AnimationNodeStateMachinePlayback _stateMachine;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		_trigger.AreaEntered += OnTriggerAreaEntered;
		_hitBox.AreaEntered += OnHitBoxAreaEntered;

		_stateMachine = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");
	}

	private void SetInvincible(bool v)
	{
		_invincible = v;
	}

	private void TakeDamage()
	{
		if(_invincible)
		{
			return;
		}

		GD.Print("=====> Boss Hit");

		// _invincible = true;
		SetInvincible(true);
		_stateMachine.Travel("hit");
		TweenHit();
		ReduceLives();

	}

	private void ReduceLives()
	{
		_lives -= 1;
		if(_lives <= 0)
		{
			Die();
		}

	}

	private void Die()
	{
		GD.Print("=====> Boss Dead");
		if(_hitTween != null)
		{
			_hitTween.Kill();
		}
		SignalManager.EmitOnBossKilled(_points);//, GlobalPosition);
		QueueFree();
	}

    private void OnHitBoxAreaEntered(Area2D area)
    {
		GD.Print("=====> Boss OnHitBoxAreaEntered");
        //throw new NotImplementedException();
		TakeDamage();
    }


    private void OnTriggerAreaEntered(Area2D area)
    {
        //throw new NotImplementedException();
		_animationTree.Set("parameters/conditions/on_trigger",true);
		
		//_hitBox.SetDeferred("monitoring", true);  //_hitBox.Monitoring = true;
		_hitBox.SetDeferred(Area2D.PropertyName.Monitoring, true); //_hitBox.Monitoring = true;
    }

	private void TweenHit()
	{
		_hitTween = GetTree().CreateTween();
		_hitTween.TweenProperty(_visual,Node2D.PropertyName.Position.ToString(),Vector2.Zero,1.6f);
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
 