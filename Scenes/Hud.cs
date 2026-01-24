using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Hud : Control
{
	[Export] private Label _scoreLabel;
	[Export] private HBoxContainer _hbHearts;


	private List<TextureRect> _hearts = new List<TextureRect>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_hearts = _hbHearts.GetChildren().OfType<TextureRect>().ToList();
	}

	private void ConnectSignals( )
	{
		SignalManager.Instance.OnPlayerHit += OnPlayerHit;
		SignalManager.Instance.OnScoreUpdated += OnScoreUpdated;
	}

    private void OnScoreUpdated()
    {
        throw new NotImplementedException();
    }


    private void OnPlayerHit(int lives)
    {
        throw new NotImplementedException();
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
