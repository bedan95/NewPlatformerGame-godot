using Godot;
using System;

public partial class Cherry : Area2D
{
	Game Game => GetNode<Game>("/root/Game");
    AnimatedSprite2D GetAnimatedSprite2D => ((AnimatedSprite2D)GetNode("AnimatedSprite2D"));

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        GetAnimatedSprite2D.Play("default");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void OnBodyEntered(Node2D body)
	{
		if(body.Name == "Player")
        {
            Game.Gold += 1;

			var tween = GetTree().CreateTween();
			var tween1 = GetTree().CreateTween();
			tween.TweenProperty(this, "position", Position - new Vector2(0, 30), 0.3);
            tween1.TweenProperty(this, "modulate:a", 0, 0.3);
			tween.TweenCallback(Callable.From(QueueFree));
		}
	}
}
