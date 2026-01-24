using Godot;
using System;
using System.ComponentModel;

public partial class Player : CharacterBody2D
{

	[Export] private float GRAVITY = 600.0f;
	[Export] private float JUMP_VELOCITY = -400.0f;
	[Export] private float MAX_FALL_SPEED = 400.0f;
	[Export] private float MOVE_SPEED = 120.0f;
	[Export] private int MAX_LIVES = 3;

	[Export] private Sprite2D _sprite2D;

	[Export] private AudioStreamPlayer2D _jumpSound;

	[Export] private PlayerState _currentState = PlayerState.Idle;

	[Export] private AnimationPlayer _animationPlayer;
	[Export] private AnimationPlayer _animationPlayerInv;
	[Export] private Timer _invincibleTimer;
	[Export] private Timer _hurtTimer;
	[Export] private Area2D _hitBox;
	[Export] private Shooter _shooter;

	[Export] private bool _invincible = false;

	[Export] private int _lives = 5;


	private static readonly Vector2 HURT_UP = new Vector2(0, -200.0f);

	public const string GroupName = "Player";

	private enum PlayerState
	{
		Idle,
		Run,
		Jump,
		Fall,
		Hurt
	}

	public void Shoot()
	{
		_shooter.Shoot(_sprite2D.FlipH ? Vector2.Left : Vector2.Right);
	}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_invincibleTimer.Timeout += OnInvincibleTimerTimeout;
		_hitBox.AreaEntered += OnHitBoxAreaEntered;
		_hurtTimer.Timeout += OnHurtTimerTimeout;
	}

	private void ApplyHurtJump()
	{
		Velocity = HURT_UP;
		_animationPlayer.Play("hurt");
		_hurtTimer.Start();
		
	}

    private void OnHurtTimerTimeout()
    {
        //throw new NotImplementedException();
		SetState(PlayerState.Idle);
    }


    private void OnHitBoxAreaEntered(Area2D area)
    {
        //throw new NotImplementedException();
		ApplyHit();
    }


	private bool ReduceLives()
	{
		_lives -= 1;
		GD.Print("Player Dead",_lives);
		if(_lives <= 0)
		{
			//GD.Print("Game Over");
			//GetTree().ChangeSceneToFile("res://Scenes/GameOver.tscn");
			
			SetPhysicsProcess(false);
			return false;
		}
		return true;	
	}

    private void ApplyHit()
    {
		if(_invincible) return;

		if(!ReduceLives())		{			return;		}

		GoInvincible();
        ///throw new NotImplementedException(); 
		SetState(PlayerState.Hurt);
		SoundManager.PlayClip(_jumpSound,SoundManager.SOUND_DAMAGE);
    }

    private void GoInvincible()
    {
        //throw new NotImplementedException();
		_invincible = true;
		_animationPlayerInv.Play("invincible");
		_invincibleTimer.Start();
    }


    private void OnInvincibleTimerTimeout()
    {
        //throw new NotImplementedException();
		_invincible = false;
		_animationPlayerInv.Play("RESET");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = GetInput((float)delta);
		MoveAndSlide();
		//Shoot();
		CaculateState();
	}

    private Vector2 GetInput(float delta)
    {
        Vector2 newVelocity = Velocity;
		newVelocity.X = 0;
		newVelocity.Y += GRAVITY*delta;

		if(_currentState == PlayerState.Hurt)
		{
			return newVelocity;
		}

		
		

		// left/right movement
		// jump
		// if falling
		// limit fall speed

		if(Input.IsActionPressed("left"))
		{
			//GD.Print("Left pressed");
			newVelocity.X -= MOVE_SPEED;
			_sprite2D.FlipH = true;

		}
		if(Input.IsActionPressed("right"))
		{
			//GD.Print("right pressed");
			newVelocity.X += MOVE_SPEED;
			_sprite2D.FlipH = false;
		} 

		if(IsOnFloor() && Input.IsActionJustPressed("jump"))
		{
			newVelocity.Y = JUMP_VELOCITY;
			SoundManager.PlayClip(_jumpSound,SoundManager.SOUND_JUMP);
		}	

		newVelocity.Y = Math.Clamp(newVelocity.Y, JUMP_VELOCITY, MAX_FALL_SPEED);


		
		// Apply gravity

		return newVelocity;

    }


	private void CaculateState()
	{
		if(_currentState == PlayerState.Hurt)
		{
			return;
		}

		PlayerState state;

		if (IsOnFloor())
		{
			if(Velocity.X == 0)
			{
				state = PlayerState.Idle;
			}
			else
			{
				state = PlayerState.Run;
			}
		}
		else
		{
			// fall or jumping
			if(Velocity.Y < 0)
			{
				state = PlayerState.Jump;
			}
			else
			{
				state = PlayerState.Fall;
			}
		}

		SetState(state);
	}

	private void SetState(PlayerState newState)
	{
		if(newState == _currentState)
		{
			return;
		}
		_currentState = newState;

		switch(_currentState)
		{
			case PlayerState.Idle:
				_animationPlayer.Play("idle");
				break;
			case PlayerState.Run:
				_animationPlayer.Play("run");	
				break;
			case PlayerState.Jump:
				_animationPlayer.Play("jump");
				break;
			case PlayerState.Fall:
				_animationPlayer.Play("fall");
				break;
			case PlayerState.Hurt:
				//_animationPlayer.Play("hurt");
				ApplyHurtJump();
				break;
		}
	}
}
