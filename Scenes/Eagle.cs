using Godot;
using System;

public partial class Eagle : EnemyBase
{
	[Export] private Timer _directionChangeTimer;

	[Export] RayCast2D _playerDector;
	[Export] Shooter _shooter;

	private readonly Vector2 FLY_SPEED  = new Vector2(35,15);
	private Vector2 _flyDirection = Vector2.Zero;

	bool _seenPlayer = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();

        _directionChangeTimer.Timeout += OndirectionChangeTimerTimeout;
    }

	public void Shoot()
	{
		if(_playerDector.IsColliding())
		{ 
			//Vector2 dir = (_playerDector.GetColliderAs<Node2D>().GlobalPosition - GlobalPosition).Normalized();
			Vector2 dir2 = GlobalPosition.DirectionTo(_player.GlobalPosition);
			_shooter.Shoot(dir2);
		}
	}

	private void SetDirectionAndFlip()
	{
		GD.Print("Eagle is setting direction towards the player!");
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
		GD.Print("Eagle is changing direction towards the player!");
        FlyTowardsPlayer();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
	{
		Velocity = _flyDirection;
		MoveAndSlide();
		Shoot();
	}

	protected override void OnScreenEntered()
	{	
		 GD.Print("Eagle has seen the player!");
		_animatedSprite2D.Play("fly");
		FlyTowardsPlayer();
	}
}
