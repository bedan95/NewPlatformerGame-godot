using Godot;
using System;

public partial class main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}

	public void OnPlayButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://world.tscn");
	}
}
