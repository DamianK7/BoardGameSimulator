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

public class Game
{
    private List<Player> players;
    private Board board;
    private Random dice;
    private Player humanPlayer;
    private Player winner;

    public Game(int boardSize)
    {
        players = new List<Player>();
        board = new Board(boardSize);
        dice = new Random();
        ChoosePlayer();
        AddComputerPlayers();
    }

    private void ChoosePlayer()
    {
        Console.WriteLine("Wybierz swoją postać:");
        Console.WriteLine("1. Wojownik");
        Console.WriteLine("2. Mag");
        Console.WriteLine("3. Healer");

        int choice;
        do
        {
            Console.Write("Podaj numer postaci: ");
        } while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3);

        string playerName = choice switch
        {
            1 => "Wojownik",
            2 => "Mag",
            3 => "Healer",
            _ => "Wojownik"
        };

        humanPlayer = new Player(playerName);
        players.Add(humanPlayer);
        Console.WriteLine($"Wybrałeś: {humanPlayer.Name}");
    }

    private void AddComputerPlayers()
    {
        if (humanPlayer.Name != "Wojownik") players.Add(new Player("Wojownik"));
        if (humanPlayer.Name != "Mag") players.Add(new Player("Mag"));
        if (humanPlayer.Name != "Healer") players.Add(new Player("Healer"));
    }

    public void Start()
    {
        Console.WriteLine("Gra rozpoczyna się!");
        bool gameFinished = false;
        while (!gameFinished)
        {
            foreach (var player in players)
            {
                Console.WriteLine($"Tura gracza: {player.Name}");

                int roll = player == humanPlayer ? RollDiceHuman() : dice.Next(1, 7);
                Console.WriteLine($"{player.Name} rzuca kostką i wyrzuca {roll}.");
                player.Move(roll);

                if (player.Position >= board.Size)
                {
                    player.Position = board.Size - 1;
                    if (winner == null) winner = player;
                    gameFinished = true;
                }

                Console.WriteLine($"{player.Name} przesuwa się na pozycję {player.Position}.");

                int reward = board.GetReward(player.Position);
                if (reward > 0)
                {
                    Console.WriteLine($"{player.Name} zbiera nagrodę: {reward} punktów!");
                    player.UpdateScore(reward);
                }
                else
                {
                    Console.WriteLine($"{player.Name} nie znajduje niczego.");
                }

                Console.WriteLine($"{player.Name} ma teraz {player.Score} punktów.");
                Console.WriteLine();
            }
        }

        DisplayResults();
    }

    private int RollDiceHuman()
    {
        Console.WriteLine("Naciśnij Enter, aby rzucić kostką.");
        Console.ReadLine();
        return dice.Next(1, 7);
    }

    private void DisplayResults()
    {
        Console.WriteLine("Koniec gry! Oto wyniki:");

        players.Sort((p1, p2) => p2.Position.CompareTo(p1.Position));

        for (int i = 0; i < players.Count; i++)
        {
            var player = players[i];
            string miejsce = i == 0 ? "Zwycięzca" : $"{i + 1} miejsce";
            Console.WriteLine($"{miejsce}: {player.Name} - {player.Score} pkt");
        }

        Console.WriteLine($"\nZwycięzcą jest {winner.Name}, który dotarł pierwszy na metę!");
    }
}
