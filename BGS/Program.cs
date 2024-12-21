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

public class Board
{
    public int Size { get; set; }
    public int[] Rewards { get; set; }

    public Board(int size)
    {
        Size = size;
        Rewards = new int[size];
        GenerateRewards();
    }

    public void GenerateRewards()
    {
        Random rand = new Random();
        for (int i = 0; i < Size; i++)
        {
            Rewards[i] = rand.Next(0, 2) == 0 ? 0 : 10;
        }
    }

    public int GetReward(int position)
    {
        if (position >= 0 && position < Size)
            return Rewards[position];
        return 0;
    }
}