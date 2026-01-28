using Godot;
using System;

public partial class CheckPoint : Area2D
{

	const string TriggerPath = "parameters/conditions/on_trigger";

	[Export] private AnimationTree _animationTree;
	[Export] private AudioStreamPlayer2D _sound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
		SignalManager.Instance.OnBossKilled += OnBossKilled;
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.OnBossKilled -= OnBossKilled;
	}

    private void OnBossKilled(int points)
    {
        //throw new NotImplementedException();
		SetDeferred(PropertyName.Monitoring,true);
		_animationTree.Set(TriggerPath, true);
    }


    private void OnAreaEntered(Area2D area)
    {
        //throw new NotImplementedException();
		SignalManager.EmitOnLevelComplete();
		SoundManager.PlayClip(_sound,SoundManager.SOUND_WIN);

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		
	}
}
