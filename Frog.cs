using Godot;
using System;

public partial class Frog : CharacterBody2D
{
    // Get the gravity from the project settings to be synced with RigidBody nodes.
    float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    Player Player => (Player)GetNode("/root/World/Player2/Player");
    bool IsChaseing { get; set; } = false;
    float Speed => 50f;
    AnimatedSprite2D GetAnimatedSprite2D => ((AnimatedSprite2D)GetNode("AnimatedSprite2D"));
    CollisionShape2D GetCollisionShape2D => ((CollisionShape2D)GetNode("CollisionShape2D"));
    Game Game => GetNode<Game>("/root/Game");
    Utils Utils => GetNode<Utils>("/root/Utils");
    bool IsDying { get; set; } = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetAnimatedSprite2D.Play("Idle");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void OnPlayerDetectionBodyEntered(Node2D body)
    {
        if (body.Name == "Player")
        {
            IsChaseing = true;
        }
    }

    public void OnPlayerDetectionBodyExited(Node2D body)
    {
        if (body.Name == "Player")
        {
            IsChaseing = false;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (IsDying)
            return;

        Vector2 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity.Y += gravity * (float)delta;
        }

        velocity = ChasePlayer(velocity);

        Velocity = velocity;
        MoveAndSlide();
    }

    Vector2 ChasePlayer(Vector2 velocity)
    {
        if (!IsChaseing)
        {
            if (GetAnimatedSprite2D.Animation != "Death")
                GetAnimatedSprite2D.Play("Idle");

            velocity.X = 0;
            return velocity;
        }

        if (GetAnimatedSprite2D.Animation != "Death")
            GetAnimatedSprite2D.Play("Jump");

        var direction = (Player.Position - this.Position).Normalized();

        velocity.X = direction.X * Speed;

        if (direction.X < 0 && GetAnimatedSprite2D.FlipH)
        {
            GetAnimatedSprite2D.FlipH = false;
        }
        else if (direction.X > 0 && !GetAnimatedSprite2D.FlipH)
        {
            GetAnimatedSprite2D.FlipH = true;
        }

        return velocity;
    }

    public void OnPlayerDeathBodyEntered(Node2D body)
    {
        if (body.Name == "Player")
        {
            Die();
        }
    }

    void Die()
    {
        IsDying = true;
        foreach(var child in GetChildren())
        {
            if(child is AnimatedSprite2D == false)
            {
                child.QueueFree();
            }
        }
        gravity = 0;
        Velocity = Vector2.Zero;
        IsChaseing = false;
        GetAnimatedSprite2D.Play("Death");
        GetAnimatedSprite2D.AnimationFinished += () => this.QueueFree();
        Game.Gold += 5;
        
        Utils.SaveGame();
    }

    public void OnPlayerDamageBodyEntered(Node2D body)
    {
        if (body.Name == "Player")
        {
            HitPlayer();
        }
    }

    void HitPlayer()
    {
        Game.Health--;
        Player.IsDazed = true;

        var direction = (Player.Position - this.Position).Normalized();
        direction.X *= 100;

        const float speed = 500;
        var velocity = Player.Velocity;
        if (direction.X < 0)
        {
            velocity.X = -speed;
        }
        else
        {
            velocity.X = speed;
        }
        Player.Velocity = velocity;
        Player.MoveAndSlide();
    }
}
