using Godot;
using System;

public partial class LevelBase : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// if(Input.IsActionJustPressed("shoot"))
		// {
		// 	//GetTree().ChangeSceneToFile("res://Scenes/Level1.tscn");
		// 	GD.Print("Create Bullet");
		// 	SignalManager.EmitOnCreateBullet(new Vector2(100,100), Vector2.Right, 20.0f, 140.0f, (int)GameObjectType.BulletPlayer);
		// }
	}
}
