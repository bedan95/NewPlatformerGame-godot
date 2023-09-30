using Godot;
using System;

public partial class Collectables : Node2D
{
    PackedScene _cherry;
	public PackedScene Cherry
	{
		get
		{
			if(_cherry == null)
				_cherry = GD.Load<Godot.PackedScene>("res://Collectables/Cherry.tscn");

			return _cherry;
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

	public void OnTimerTimeout()
	{
		var cherry = Cherry.Instantiate<Node2D>();

		var x = new Random().Next(0, 2000);
		var y = new Random().Next(200, 304);
		cherry.Position = new Vector2(x, y);
        AddChild(cherry);
    }
}
