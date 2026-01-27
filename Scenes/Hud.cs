using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public partial class Hud : Control
{
	[Export] private Label _scoreLabel;
	[Export] private HBoxContainer _hbHearts;


	private List<TextureRect> _hearts;// = new List<TextureRect>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_hearts = _hbHearts.GetChildren().OfType<TextureRect>().ToList();
		ConnectSignals();
	}

	private void ConnectSignals( )
	{
		SignalManager.Instance.OnPlayerHit += OnPlayerHit;
		SignalManager.Instance.OnScoreUpdated += OnScoreUpdated;
	}

    public override void _ExitTree()
    {
        //base._ExitTree();
    }


    private void OnScoreUpdated()
    {
        
    }


    private void OnPlayerHit(int lives)
    {
        for(int i = 0; i < _hearts.Count; i++)
		{
			if(i < lives)
			{
				_hearts[i].Visible = true;
			}
			else
			{
				_hearts[i].Visible = false;
			}
		}
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
