using Godot;
using System;

public partial class Main : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("shoot"))
		{
			//GetTree().Quit();
			GD.Print("Next Level");
			ScoreManager.ResetScore();
			GameManager.LoadNextLevelScene();
		}

		if(Input.IsActionJustPressed("quit"))
		{
			GetTree().Quit();
		}
	}
}
