using Godot;
using System;
using System.ComponentModel.DataAnnotations;

public partial class Shooter : Node2D
{
	[Export] private float _speed = 200.0f;
	[Export] private float _lifeSpan = 10.0f;
	[Export] private GameObjectType _bulletKey;

	[Export] private AudioStreamPlayer2D _sound;
	[Export] private Timer _shootTimer;

	[Export] private float _shootDelay = 0.7f;

	private bool _canShoot = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_shootTimer.WaitTime = _shootDelay;
		_shootTimer.Timeout += OnShootTimerTimeout;
	}

    private void OnShootTimerTimeout()
    {
        _canShoot = true;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}


	public void Shoot(Vector2 dir)
	{
		if (_canShoot)
		{
			_canShoot = false;
			SignalManager.EmitOnCreateBullet(GlobalPosition, dir, _lifeSpan, _speed, (int)_bulletKey);
			SoundManager.PlayClip(_sound,SoundManager.SOUND_LASER);			
			_shootTimer.Start();
		}
	}
}
