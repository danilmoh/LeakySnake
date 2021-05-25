using Godot;
using System;

public class Tale : RigidBody2D
{
	Timer timer = null;
	Vector2 lastPosition;

	public int instancesCount;
	public int myNameCount;

	Vector2 checkRotation;

	// textures
	Texture tale = ResourceLoader.Load("res://Sprites/Snake's body.png") as Texture;
	Texture talesTale = ResourceLoader.Load("res://Sprites/Snake's tale.png") as Texture;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Setting up Timer
		timer = new Timer();
		AddChild(timer);
		timer.Connect("timeout", this, "OnTimerTimeOut");
		if(this.Name == "tale1")
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
		if(this.Name == "tale1")
		{
			Position = (Vector2)GetParent().GetNode<Area2D>("Player").Get("lastPosition");
		}
		else if(this.Name == $"tale{myNameCount}")
		{
			var newPosition = (Vector2)GetParent().GetNode<RigidBody2D>($"tale{myNameCount-1}").Get("lastPosition");
			Position = newPosition;
		}

		if(this.Name == $"tale{instancesCount}")
		{
			GetNode<Sprite>("Sprite").Texture = talesTale;
		}
		else
		{
			GetNode<Sprite>("Sprite").Texture = tale;
		}

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

	void AngleSprite()
	{
		//Vector2 nextPosition = ();	
	}

	void OnTimerTimeOut()
	{
		instancesCount = (int)GetTree().Root.GetNode<Node2D>("Map").Get("instancesCount");
		lastPosition = Position;
	}
}
