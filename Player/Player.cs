    using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public const float Speed = 300.0f;
    public const float JumpVelocity = -400.0f;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    Vector2 GetDirection => Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
    AnimationPlayer GetAnimation => ((AnimationPlayer)GetNode("AnimationPlayer"));
    AnimatedSprite2D GetSprite => ((AnimatedSprite2D)GetNode("AnimatedSprite2D"));


    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity.Y += gravity * (float)delta;
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        {
            GetAnimation.Play("Jump");
            velocity.Y = JumpVelocity;
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        var direction = GetDirection;

        if(direction.X == -1 && !GetSprite.FlipH)
        {
            GetSprite.FlipH = true;
        }
        else if(direction.X == 1 && GetSprite.FlipH)
        {
            GetSprite.FlipH = false;
        }

        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * Speed;

            if (velocity.Y == 0)
                GetAnimation.Play("Run");
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);

            if (velocity.Y == 0)
                GetAnimation.Play("Idle");
        }

        if (velocity.Y > 0)
            GetAnimation.Play("Fall");

        Velocity = velocity;
        MoveAndSlide();
    }

    public override void _Ready()
    {
        base._Ready();

        GetAnimation.Play("Idle");
    }
}
