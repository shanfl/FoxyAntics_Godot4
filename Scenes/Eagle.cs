using Godot;
using System;

public partial class Eagle : EnemyBase
{
	[Export] private Timer _directionChangeTimer;

	private readonly Vector2 FLY_SPEED  = new Vector2(35,15);
	private Vector2 _flyDirection = Vector2.Zero;

	bool _seenPlayer = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();

        _directionChangeTimer.Timeout += OndirectionChangeTimerTimeout;
    }

	private void SetDirectionAndFlip()
	{
		int xDirection = Math.Sign(_player.GlobalPosition.X - GlobalPosition.X);
		_animatedSprite2D.FlipH = xDirection  > 0;
		_flyDirection = new Vector2(FLY_SPEED.X * xDirection, FLY_SPEED.Y);
	}

	private void FlyTowardsPlayer()
	{
		SetDirectionAndFlip();

		_directionChangeTimer.Start();
	}

    private void OndirectionChangeTimerTimeout()
    {
        FlyTowardsPlayer();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
	{
		Velocity = _flyDirection;
		MoveAndSlide();
	}

	protected override void OnScreenEntered()
	{	
		_animatedSprite2D.Play("fly");
		FlyTowardsPlayer();
	}
}
