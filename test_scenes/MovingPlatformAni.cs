using Godot;
using System;
using System.Collections.Generic;
//using System.Numerics;


class TargetDistanceTime
{
	public Vector2 PositionInfo;
	public float Time;



	public TargetDistanceTime(Vector2 positionFrom,Vector2 positionTo,float speed)
	{
		Time = positionFrom.DistanceTo(positionTo) / speed;
		PositionInfo = positionTo;
	}

	public override string ToString()
	{
		return $"Pos:{PositionInfo} Time:{Time}";
	}

}

public partial class MovingPlatformAni : AnimatableBody2D
{

	[Export] private Godot.Collections.Array<Marker2D> _points = new();
	[Export] private float _speed = 100.0f;

	private List<TargetDistanceTime> _targetPoints = [];
	private Tween _tween;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GlobalPosition = _points[0].GlobalPosition;
		
		SetupPoints();

		RunTween();
	}

	private void SetupPoints()
	{
		if(_points.Count < 2)
		{
			GD.PrintErr("MovingPlatformAni needs at least 2 points to move between");
			return;
		}
		for(int i = 0;i < _points.Count;i++)
		{
			Vector2 targetPos = _points[(i + 1) % _points.Count].GlobalPosition;
			_targetPoints.Add( new TargetDistanceTime(_points[i].GlobalPosition,targetPos,_speed) );
		}

		foreach(var tdt in _targetPoints)
		{
			GD.Print("======> ",tdt.ToString());
		}
	}

    public override void _ExitTree()
    {
        if(	_tween != null)
		{
			_tween.Kill();
		}
    }

	private void RunTween()
	{
		this._tween = GetTree().CreateTween();
		_tween.SetLoops(0);

		foreach(var tdt in _targetPoints)
		{
			_tween.TweenProperty(this,Node2D.PropertyName.Position.ToString(),tdt.PositionInfo,tdt.Time);
		}
		_tween.TweenInterval(0.02f);
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
