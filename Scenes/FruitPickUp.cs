using Godot;
using System;
using System.ComponentModel;
using System.Linq;

public partial class FruitPickUp : Area2D
{

	[Export] private int _points = 2;
	[Export] private AnimatedSprite2D _animatedSprite2D;
	[Export] private float _jump = -80.0f;
	[Export] private Timer _lifetimeTimer;
	[Export] private float _gravity = 400.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_lifetimeTimer.Timeout += OnLifetimeTimeout;
		AreaEntered += OnAreaEntered;

		PlayRandomAnimation();		
	}

	private void PlayRandomAnimation()
	{
		//_animatedSprite2D.Play(GD.Randi() % _animatedSprite2D.Frames.GetAnimationNames().Count);
		var animationNames = _animatedSprite2D.SpriteFrames.GetAnimationNames();
		if (animationNames.Count() > 0)
		{
			string randomAnimation = animationNames.ElementAt((int)(GD.Randi() % animationNames.Length));
			_animatedSprite2D.Play(randomAnimation);
		}
		else
		{
			_animatedSprite2D.Play(animationNames[0]);
		}
	}

    private void OnAreaEntered(Area2D area)
    {
        QueueFree();
		//SignalManager.Instance.Em
    }


    private void OnLifetimeTimeout()
    {
        QueueFree();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
