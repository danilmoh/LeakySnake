using Godot;
using System;

public class Tail : RigidBody2D
{
	Timer timer = null;
	Vector2 lastPosition;

	public int instancesCount;
	public int myNameCount;

	Vector2 checkRotation;

	// textures
	Texture tail = ResourceLoader.Load("res://Sprites/Snake's body.png") as Texture;
	Texture tailsTail = ResourceLoader.Load("res://Sprites/Snake's tail.png") as Texture;
	Texture tailsAngle = ResourceLoader.Load("res://Sprites/Snake's angle.png") as Texture;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Setting up Timer
		timer = new Timer();
		AddChild(timer);
		timer.Connect("timeout", this, "OnTimerTimeOut");
		if(this.Name == "tail1")
			timer.WaitTime = (float)GetParent().GetNode<Area2D>("Player").Get("delay");
		else
			timer.WaitTime = (float)GetParent().GetNode<Area2D>("Player").Get("delay");
		timer.OneShot = false;
		timer.Start();
		
		int InstancesCountMyNameCount = (int)GetTree().Root.GetNode<Node2D>("Map").Get("instancesCount");
		myNameCount = InstancesCountMyNameCount;
	}

	public override void _Process(float delta)
	{
		if(this.Name == "tail1")
		{
			Position = (Vector2)GetParent().GetNode<Area2D>("Player").Get("lastPosition");
		}
		else if(this.Name == $"tail{myNameCount}")
		{
			var newPosition = (Vector2)GetParent().GetNode<RigidBody2D>($"tail{myNameCount-1}").Get("lastPosition");
			Position = newPosition;
		}

		if(this.Name == $"tail{instancesCount}")
		{
			GetNode<Sprite>("Sprite").Texture = tailsTail;
		}
		else
		{
			GetNode<Sprite>("Sprite").Texture = tail;
		}

		Vector2 pos = new Vector2();
		pos = Position;

		// fix tail overlap over botton map sprite node by hiding
		if(pos.x > 980 || pos.x < 20 || pos.y < 20 || pos.y > 980)
			this.Hide();
		else	
			this.Show();


		RotateSprite();
	}

	void RotateSprite()
	{
		checkRotation = Position;
		if (checkRotation.x < lastPosition.x)
			GetNode<Sprite>("Sprite").RotationDegrees = -90;
		if(checkRotation.x > lastPosition.x)
			GetNode<Sprite>("Sprite").RotationDegrees = 90;
		if(checkRotation.y < lastPosition.y)
			GetNode<Sprite>("Sprite").RotationDegrees = 0;
		if(checkRotation.y > lastPosition.y)
			GetNode<Sprite>("Sprite").RotationDegrees = 180;
	}

	void OnTimerTimeOut()
	{
		instancesCount = (int)GetTree().Root.GetNode<Node2D>("Map").Get("instancesCount");
		lastPosition = Position;
	}
}
