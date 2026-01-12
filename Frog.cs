using Godot;
using System;

public partial class Frog : EnemyBase
{
	[Export] private Timer _jumpTimer;

	private float MinJumpTime = 1.0f;
	private float MaxJumpTime = 3.0f;


	private const float JUMP_VELOCITY_X = 100.0f;
	private const float Jump_VELOCITY_Y = 150.0f;


	private static readonly Vector2 JUMP_VELOCITY_R = new Vector2(JUMP_VELOCITY_X, -Jump_VELOCITY_Y);
	private static readonly Vector2 JUMP_VELOCITY_L = new Vector2(-JUMP_VELOCITY_X, -Jump_VELOCITY_Y);


	private bool _seenPlayer = false;
	private bool _jump = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();

		_jumpTimer.Timeout += OnJumepTimerTimeout;
	}

    private void OnJumepTimerTimeout()
    {
        GD.Print("Frog Jump!");

		_jump = true;
		// continue jump
		//StartJumpTimer();
    }


	private void ApplyJump()
	{
		if(IsOnFloor() && _seenPlayer && _jump)
		{
			_jump = false;
			_animatedSprite2D.Play("jump");
			Velocity = _animatedSprite2D.FlipH ? JUMP_VELOCITY_R : JUMP_VELOCITY_L;
			StartJumpTimer();
		}
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
		if(!IsOnFloor())
		{
			Vector2 velocity = Velocity;
			velocity.Y += _gravity * (float)delta;
			Velocity = velocity;
		}
		else
		{
			Velocity = new Vector2(0, 0);
			_animatedSprite2D.Play("idle");
		}

		ApplyJump();
		MoveAndSlide();
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
