using Godot;
using System;

public partial class Hp : Label
{
	Player Player => (Player)GetNode("../../Player2/Player");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Text = "HP: " + Player.Health.ToString();
    }
}
