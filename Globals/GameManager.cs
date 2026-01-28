using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node
{
    const int TotalLevels = 3;
    
    private Dictionary<int, PackedScene> _levels = new Dictionary<int, PackedScene>();

    private PackedScene _mainScene = GD.Load<PackedScene>("res://Scenes/main.tscn");

    int _currentLevel = 0;

    public static GameManager Instance { get; private set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Instance = this;


        for(int i = 0;i < TotalLevels; i++)
        {
            _levels[i] = GD.Load<PackedScene>($"res://Scenes/levels/Level{i + 1}.tscn");
        }
	}

    private void SetNextLevel()
    {
        _currentLevel++;
        if(_currentLevel > TotalLevels)
        {
            _currentLevel = 1;
        }
    }

    public static void LoadNextLevelScene()
    {
        if (Instance._currentLevel < TotalLevels)
        {
            Instance._currentLevel++;
            Instance.GetTree().ChangeSceneToPacked(Instance._levels[Instance._currentLevel]);
        }
    }

    public static void LoadMainScene()
    {
        Instance._currentLevel = 0;
        Instance.GetTree().ChangeSceneToPacked(Instance._mainScene);
    }
}
