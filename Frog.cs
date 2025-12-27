using Godot;
using System;

public partial class Frog : EnemyBase
{
	[Export] private Timer _jumpTimer;

	private float MinJumpTime = 1.0f;
	private float MaxJumpTime = 3.0f;
	private bool _seenPlayer = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();

		_jumpTimer.Timeout += OnJumepTimerTimeout;
	}

    private void OnJumepTimerTimeout()
    {
        GD.Print("Frog Jump!");

		// continue jump
		StartJumpTimer();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

	private void StartJumpTimer()
	{
		_jumpTimer.WaitTime = GD.RandRange(MinJumpTime, MaxJumpTime);
		_jumpTimer.Start();
	}

	public void FlipMe()
	{
		_animatedSprite2D.FlipH = _player.GlobalPosition.X > GlobalPosition.X;
	}

	public override void _PhysicsProcess(double delta)
	{
		FlipMe();
	}

	protected override void OnScreenEntered()
	{	
		if(!_seenPlayer)
		{
			_seenPlayer = true;
			GD.Print("Frog has seen the player!");

			StartJumpTimer();
		}
		
		

	}

}
