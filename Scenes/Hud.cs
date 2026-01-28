using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public partial class Hud : Control
{
	[Export] private Label _scoreLabel;
	[Export] private HBoxContainer _hbHearts;

	[Export] private ColorRect _colorRect;
	[Export] private VBoxContainer _vbLevelComplete;
	[Export] private VBoxContainer _vbGameOver;

	[Export] private AudioStreamPlayer _sound;
	[Export] private Timer _continueTimer;

	private bool _canContinue = false;


	private List<TextureRect> _hearts;// = new List<TextureRect>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_hearts = _hbHearts.GetChildren().OfType<TextureRect>().ToList();
		ConnectSignals();
	}

	private void ConnectSignals( )
	{
		SignalManager.Instance.OnPlayerHit 		+= OnPlayerHit;
		SignalManager.Instance.OnScoreUpdated 	+= OnScoreUpdated;
		SignalManager.Instance.OnGameOver 		+= OnGameOver;
		SignalManager.Instance.OnLevelComplete 	+= OnLevelComplete;

		_continueTimer.Timeout += OnContinueTimeout;
	}

    private void OnContinueTimeout()
    {
        _canContinue = true;
    }

    private void DisConnectSignals( )
	{
		SignalManager.Instance.OnPlayerHit 		-= OnPlayerHit;
		SignalManager.Instance.OnScoreUpdated 	-= OnScoreUpdated;
		SignalManager.Instance.OnGameOver 		-= OnGameOver;
		SignalManager.Instance.OnLevelComplete 	-= OnLevelComplete;
	}

	private void ShowHud(bool gameover)
	{
		if (gameover)
		{
			_vbGameOver.Visible = true;
			_vbLevelComplete.Visible = false;
			//_sound.Play();
		}
		else
		{
			_vbLevelComplete.Visible = true;
			_vbGameOver.Visible = false;
			//_sound.Play();
		}

		_continueTimer.Start();
		_colorRect.Show();
	}

    private void OnLevelComplete()
    {
        //throw new NotImplementedException();
		ShowHud(false);
    }


    private void OnGameOver()
    {
        //throw new NotImplementedException();
		ShowHud(true);

    }



    public override void _ExitTree()
    {
        //base._ExitTree();
		DisConnectSignals();
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
		if(_canContinue && Input.IsActionJustPressed("shoot"))
		{
			if(_vbLevelComplete.Visible)
			{
				GD.Print("Next Level");
				GameManager.LoadNextLevelScene();
			}
			else if(_vbGameOver.Visible)
			{
				GD.Print("Restart Game");
				//ScoreManager.ResetScore();
				GameManager.LoadMainScene();
			}
		}
	}
}
