using Godot;
using System;

public partial class Player : CharacterBody2D
{
    const float _speed = 300.0f;
    const float _jumpVelocity = -400.0f;
    int _health = 10;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    Vector2 GetDirection => Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
    AnimationPlayer GetAnimation => ((AnimationPlayer)GetNode("AnimationPlayer"));
    AnimatedSprite2D GetAnimatedSprite2D => ((AnimatedSprite2D)GetNode("AnimatedSprite2D"));
    public int Health
    {
        get => _health;
        set
        {
            if (_health > value)
                IsDazed = true;

            if (value > 0)
            {
                _health = value;
            }
            else
            {

            }
        }
    }
    bool IsDazed { get; set; } = false;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity.Y += gravity * (float)delta;
        }

        if (IsDazed)
        {
            GetAnimation.Play("Idle");

            velocity.X *= .85f * (float)delta * (float)Engine.GetFramesPerSecond();
            Velocity = velocity;
            MoveAndSlide();

            if (Mathf.Abs(velocity.X) < 10)
                IsDazed = false;

            return;
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        {
            GetAnimation.Play("Jump");
            velocity.Y = _jumpVelocity;
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        var direction = GetDirection;

        if (direction.X == -1 && !GetAnimatedSprite2D.FlipH)
        {
            GetAnimatedSprite2D.FlipH = true;
        }
        else if (direction.X == 1 && GetAnimatedSprite2D.FlipH)
        {
            GetAnimatedSprite2D.FlipH = false;
        }

        if (direction != Vector2.Zero)
        {
            if (Mathf.Abs(velocity.X) < Mathf.Abs(direction.X * _speed))
                velocity.X += direction.X * _speed * (float)delta;
            else
                velocity.X = direction.X * _speed;

            if (velocity.Y == 0)
                GetAnimation.Play("Run");
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, _speed);

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
