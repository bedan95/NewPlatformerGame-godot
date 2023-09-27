using Godot;
using System;
using System.Collections.Generic;

public partial class Game : Node
{
    int _health = 10;
	int _gold = 0;

	public int Health
	{
		get => _health;
		set
		{
            if (value >= 0)
            {
                _health = value;
            }
        }
	}

	public int Gold
	{
		get => _gold;
		set => _gold = value;
	}

	public void Init(Dictionary<string, string> data)
	{
		Health = int.Parse(data["PlayerHp"]);
		Gold = int.Parse(data["PlayerGold"]);
	}

    Utils Utils => GetNode<Utils>("/root/Utils");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		Utils.LoadGame();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
