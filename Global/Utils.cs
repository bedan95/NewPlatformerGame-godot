using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;

public partial class Utils : Node
{
	const string _savePath = "res://savegame.bin";

	Game Game => GetNode<Game>("/root/Game");

	public void SaveGame()
	{
		var file = FileAccess.Open(_savePath, FileAccess.ModeFlags.Write);
		var data = new Dictionary<string, string>()
		{
			{"PlayerHp", Game.Health.ToString() },
			{ "PlayerGold", Game.Gold.ToString() }
		};

		var json = JsonSerializer.Serialize(data);
		file.StoreLine(json);
	}

	public void LoadGame()
    {
		if(FileAccess.FileExists(_savePath))
        {
            var file = FileAccess.Open(_savePath, FileAccess.ModeFlags.Read);
			if (!file.EofReached())
			{
                var currentLine = JsonSerializer.Deserialize<Dictionary<string, string>>(file.GetLine());
				if(currentLine.Count > 0)
				{
					Game.Init(currentLine);
				}
            }
        }
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
