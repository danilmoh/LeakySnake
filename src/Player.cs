using Godot;
using System;

public class Player : Area2D
{
    Vector2 pos;
    [Export]
    public Vector2 lastPosition;
    public Vector2 screenSize;

    Timer timer = null;
    Timer textTimer = null;
    [Export]
    float delay = 0.7f;     // snake's speed

    Vector2 position;
    bool felsa = false; // check invalid position

    [Signal]
    public delegate void Hit();
    [Signal]
    public delegate void Apple();
    [Signal]
    public delegate void start();
    [Signal]
    public delegate void end();

    // movement direction
    public string direction = null;
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var startPosition = new Vector2(GetViewportRect().Size.x/2, 500);
        Position = startPosition;
        pos = Position;

        // Setting up Timer
        timer = new Timer();
        AddChild(timer);
        timer.Connect("timeout", this, "OnTimerTimeOut");
        timer.WaitTime = delay;
        timer.OneShot = false;
        timer.Start();
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if(Input.IsActionPressed("ui_down") && direction != "up")
        {
            direction = "down";
        }
        if(Input.IsActionPressed("ui_up") && direction != "down")
        {
            direction = "up";
        }
        if(Input.IsActionPressed("ui_left") && direction != "right")
        {
            direction = "left";
        }
        if(Input.IsActionPressed("ui_right") && direction != "left")
        {
            direction = "right";
        }
    }

    public override void _Process(float delta)
    {
        if(direction == null)
            EmitSignal("start");
        else
            EmitSignal("end");
    }

    void OnTimerTimeOut()
    {
        lastPosition = Position;
        if(direction == "down")
        {
            GetNode<Sprite>("Sprite").RotationDegrees = 180;
            if(pos.y > 1000)
                pos.y = 20;
            else
                pos.y += 40;
            Position = pos;
        }
        if(direction == "up")
        {
            GetNode<Sprite>("Sprite").RotationDegrees = 0;
            if(pos.y < 0)
                pos.y = 980;
            else
                pos.y -= 40;
            Position = pos;
        }
        if(direction == "left")
        {
            GetNode<Sprite>("Sprite").RotationDegrees = -90;
            if(pos.x < 0)
                pos.x = 980;
            else
                pos.x -= 40;
            Position = pos;
        }
        if(direction == "right")
        {
            GetNode<Sprite>("Sprite").RotationDegrees = 90;
            if(pos.x > 1000)
                pos.x = 20;
            else
                pos.x += 40;
            Position = pos;
        }
    }

    public void Start(Vector2 position)
    {
        //Position = position;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }

    public void OnPlayerBodyEntered(PhysicsBody2D body)
    {
        if(body.IsInGroup("tail"))
        {
            EmitSignal(nameof(Hit));
        }
        if(body.IsInGroup("apple"))
        {
            EmitSignal(nameof(Apple));
        }
    }
}
