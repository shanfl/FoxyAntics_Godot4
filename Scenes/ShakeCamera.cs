using Godot;
using System;


public partial class ShakeCamera : Camera2D
{
	[Export] private Timer _shakeTimer;
	[Export] private double _shakeAmount = 5.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetProcess(false);

		SignalManager.Instance.OnPlayerHit += OnPlayerHit;
		_shakeTimer.Timeout += OnTimerOut;
	}

    public override void _ExitTree()
    {
		SignalManager.Instance.OnPlayerHit -= OnPlayerHit;
		_shakeTimer.Timeout -= OnTimerOut;
	}

	public Vector2 GetRandomOffset()
	{
		return new Vector2(
			(float)GD.RandRange(-_shakeAmount,_shakeAmount),
			(float)GD.RandRange(-_shakeAmount,_shakeAmount));
	}


    private void OnTimerOut()
    {
        SetProcess(false);
		Offset = Vector2.Zero;
    }


    private void OnPlayerHit(int lives)
    {
        _shakeTimer.Start();
		SetProcess(true);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		Offset = GetRandomOffset();
	}
}
