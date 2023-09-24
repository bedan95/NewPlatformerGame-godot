using Godot;
using System;

public partial class BG : ParallaxBackground
{
	readonly float _scrollingSpeed = 100f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var scrollOffset = ScrollOffset;
		scrollOffset.X -= _scrollingSpeed * (float)delta;
		ScrollOffset = scrollOffset;
    }
}
