using System;
using System.Collections.Generic;

public class Player
{
    public string Name { get; set; }
    public int Position { get; set; }
    public int Score { get; set; }

    public Player(string name)
    {
        Name = name;
        Position = 0;
        Score = 0;
    }

    public void Move(int steps)
    {
        Position += steps;
    }

    public void UpdateScore(int points)
    {
        Score += points;
    }
}