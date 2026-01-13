using Godot;
using System;
using System.Collections.Generic;

public partial class ObjectMaker : Node2D
{
	//[Export]
	private Dictionary<GameObjectType, PackedScene> _objectScenes = new() ;// new Dictionary<GameObjectType, PackedScene>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		//_objectScenes[GameObjectType.Explosion] = GD.Load<PackedScene>("res://Scenes/Explosion.tscn");
		//_objectScenes[GameObjectType.Pickup] = GD.Load<PackedScene>("res://Scenes/Pickup.tscn");
		_objectScenes[GameObjectType.BulletEnemy] = GD.Load<PackedScene>("res://Scenes/EnemyBullet.tscn");
		_objectScenes[GameObjectType.BulletPlayer] = GD.Load<PackedScene>("res://Scenes/PlayerBullet.tscn");

		SignalManager.Instance.OnCreateBullet += OnCreateBullet;

	}

	private void AddObject(Node node)
	{
		AddChild(node);
	}

	private void OnCreateBullet(Vector2 position, Vector2 direction, float lifeSpane, float speed, int gameObjectType)
	{
		if (_objectScenes.TryGetValue((GameObjectType)gameObjectType, out var scene))
		{
			var obj = scene.Instantiate<BulletBase>();
			obj.GlobalPosition = position;
			obj.Setup(direction, lifeSpane, speed);
			CallDeferred(MethodName.AddObject, obj);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
