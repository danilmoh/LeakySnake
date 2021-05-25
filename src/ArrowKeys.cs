using Godot;
using System;

public class ArrowKeys : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    string buttonDirection;
    public override void _Ready()
    {
        
    }

    void _on_upButton_pressed()
    {
        Input.ActionPress("ui_up");
    }
    void _on_upButton_released()
    {
        Input.ActionRelease("ui_up");
    }
    void _on_downButton_pressed()
    {
        Input.ActionPress("ui_down");
    }
    void _on_downButton_released()
    {
        Input.ActionRelease("ui_down");
    }
    void _on_leftButton_pressed()
    {
        Input.ActionPress("ui_left");
    }
    void _on_leftButton_released()
    {
        Input.ActionRelease("ui_left");
    }
    void _on_rightButton_pressed()
    {
        Input.ActionPress("ui_right");
    }
    void _on_rightButton_released()
    {
        Input.ActionRelease("ui_right");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
