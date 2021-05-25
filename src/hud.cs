using Godot;
using System;

public class hud : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    Timer timer = null;
    int score;
    public override void _Ready()
    {
        GetNode<Label>("Score").Show();
    }

    public void _on_Player_start()
    {
        GetNode<Label>("Label").Show();
    }

    public void _on_Player_end()
    {
        GetNode<Label>("Label").Hide();
    }

    public void _on_Map_die()
    {
        Timer();
        GetNode<Label>("die").Show();
    }

    void Timer()
    {
        timer = new Timer();
        AddChild(timer);
        timer.Connect("timeout", this, "OnTimerTimeout");
        timer.WaitTime = 3.5f;
        timer.OneShot = true;
        timer.Start();
    }

    void OnTimerTimeout()
    {
        GetNode<Label>("die").Hide();
    }

    public void _on_Map_Score()
    {
        score = (int)GetTree().Root.GetNode<Node2D>("Map").Get("instancesCount");
        if( (int)GetTree().Root.GetNode<Node2D>("Map").Get("instancesCount") == 2 )
            score = 1;

        if(score > 1)
            score -= 1;
            
        GetNode<Label>("Score").Text = score.ToString();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
