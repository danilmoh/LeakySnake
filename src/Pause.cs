using Godot;
using System;

public class Pause : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    bool pause = true;
    bool press = true;
    Texture pressed = (Texture) ResourceLoader.Load("res://Controls/pauseButtonIsPressed.png");
    Texture unpressed = (Texture) ResourceLoader.Load("res://Controls/pauseButton.png");
    CanvasLayer canvas;
    Control control;

    public override void _Ready()
    {   
        canvas = GetNode<CanvasLayer>("CanvasLayer");
        control = canvas.GetNode<Control>("Control");
    }

    void _on_TouchScreenButton_pressed()
    {
        if(press)
        {
            canvas.GetNode<Control>("Control").Show();  // show score window
            press = false;
            GetNode<TouchScreenButton>("TouchScreenButton").Normal = pressed;
        }
        else if(!press)
        {
            canvas.GetNode<Control>("Control").Hide();  // hide score window
            press = true;
            GetNode<TouchScreenButton>("TouchScreenButton").Normal = unpressed;
        }
        GamePause();
    }

    void _on_TouchScreenButton_released()
    {

    }

    void GamePause()
    {
        if (pause)
        {
            pause = false;
            GetTree().Paused = true;
        }
        else
        {
            pause = true;
            GetTree().Paused = false;
        }
    }

    void _on_Map_Record()
    {
        control.GetNode<Label>("Record").Text = GetTree().Root.GetNode<Node2D>("Map").Get("bestRecord").ToString();
    }
    

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
