using Godot;
using System;

public class Main : Node2D
{
#pragma warning disable 649
    [Export]
    PackedScene TailScene;
    [Signal]
    public delegate void HUD();
#pragma warning restore 649

    [Signal]
    public delegate void Score();
    Tail tail = null;
    //Apple apple;// = null;
    Timer timer = null;     // timer

    int[] cells = new int [25];
    Vector2 pos;
    int appledCount = 0;
    public int instancesCount = 0;

    int bestRecord;     // records variables
    [Signal]
    public delegate void Record();
    int recordCheckCount = 0;
    int score;
    string scoreFile = "user://score.save";
    public override void _Ready()
    {
        GD.Randomize();

        float delay = (float)GetNode<Area2D>("Player").Get("delay");
        timer = new Timer();
        AddChild(timer);
        timer.Connect("timeout", this, "OnTimerTimeOut");
        timer.WaitTime = 10;
        timer.OneShot = true;
        timer.Start();

        // Apple 
        randomNumberGenerator();
        GetNode<RigidBody2D>("Apple").Position = pos;

        LoadScore();
        EmitSignal("Record");
    }

    public override void _Process(float delta)
    {
        EmitSignal("Score");
    }

    void Instance() // instantiation of tail and giving new name to it
    {
        tail = (Tail)TailScene.Instance();
        instancesCount += 1;
        tail.Name = $"tail{instancesCount}";
        CallDeferred("add_child", tail);
    }
    void OnTimerTimeOut()
    {
        
    }

    void OnPlayerAppled()
    {
        appledCount += 1;
        if(appledCount == 1)
        {
            Instance(); Instance();
        }
        else
        Instance();
        
        randomNumberGenerator();
        GetNode<RigidBody2D>("Apple").Position = pos;
    }

    void randomNumberGenerator()
    {
        for(int i = 0; i < cells.Length; i++)
        {
            if(i == 0)
            {
                cells[i] = 20;
            }
            else
            {
                cells[i] = cells[i-1] + 40;
            }
        }

        var randomNumberGenerator = new RandomNumberGenerator();
        randomNumberGenerator.Randomize();
        int arrayX = (int)randomNumberGenerator.RandfRange(0, 24);
        int arrayY = (int)randomNumberGenerator.RandfRange(0, 24);
        pos.x = cells[arrayX];
        pos.y = cells[arrayY];
    }

    void OnPlayerTailed()
    {
        recordCheckCount += 1;
        AddRecord();

        Godot.Collections.Array forDestorying;
        forDestorying = GetTree().GetNodesInGroup("tail");
        foreach (Node forDestroy in forDestorying)
        {
            forDestroy.CallDeferred("free");
        }
        instancesCount = 0;
        EmitSignal("HUD");
    }

    void AddRecord()
    {
        score = (int)GetNode<CanvasLayer>("HUD").Get("score");
        if(bestRecord == 0 || bestRecord == null)
        {
            if(recordCheckCount == 1)
                bestRecord = score;
        }

        if(score > bestRecord)
        {
            bestRecord = score;
        }

        SaveScore();
        EmitSignal("Record");
    }

    public void SaveScore()
    {
        var file = new File();
        file.Open(scoreFile, File.ModeFlags.Write);
        file.Store32((uint)bestRecord);
        file.Close();
    }

    public void LoadScore()
    {
        var file = new File();
        if (file.FileExists(scoreFile))
        {
            file.Open(scoreFile, File.ModeFlags.Read);
            bestRecord = (int)file.Get32();
            GD.Print("not score" + bestRecord);
            file.Close();
        }
        else
            bestRecord = 0;

        GD.Print("final score " + bestRecord);
    }
}
