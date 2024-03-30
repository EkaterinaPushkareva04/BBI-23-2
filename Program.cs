using System;
using System.Linq;

abstract class Player
{
    private string LastName;

    public Player(string lastName)
    {
        LastName = lastName;
    }

    public string GetLastName()
    {
        return LastName;
    }

    public abstract void AddPenalty();
    public abstract bool IsExcluded();
}

class HockeyPlayer : Player
{
    private int TotalPenaltyTime;

    public HockeyPlayer(string lastName, int totalPenaltyTime) : base(lastName)
    {
        TotalPenaltyTime = totalPenaltyTime;
    }

    public override void AddPenalty()
    {
        TotalPenaltyTime += 2;
    }

    public override bool IsExcluded()
    {
        return TotalPenaltyTime >= 10;
    }

    public int GetTotalPenaltyTime()
    {
        return TotalPenaltyTime;
    }

    public void DisplayPlayer()
    {
        Console.WriteLine($"{GetLastName()}\t{TotalPenaltyTime} мин");
    }
}

class BasketballPlayer : Player
{
    private int Fouls;

    public BasketballPlayer(string lastName, int fouls) : base(lastName)
    {
        Fouls = fouls;
    }

    public override void AddPenalty()
    {
        Fouls++;
    }

    public override bool IsExcluded()
    {
        return Fouls >= 4;
    }

    public int GetFouls()
    {
        return Fouls;
    }

    public void DisplayPlayer()
    {
        Console.WriteLine($"{GetLastName()}\t{Fouls} фолов");
    }
}

class Program
{
    static void Main()
    {
        Player[] players = new Player[5];
        players[0] = new HockeyPlayer("Иванов", 4);
        players[1] = new HockeyPlayer("Петров", 5);
        players[2] = new BasketballPlayer("Сидоров", 1);
        players[3] = new BasketballPlayer("Дорофеев", 4);
        players[4] = new BasketballPlayer("Федоров", 3);

        var candidates = players.Where(p => !p.IsExcluded()).OrderBy(p => p is HockeyPlayer ? ((HockeyPlayer)p).GetTotalPenaltyTime() : ((BasketballPlayer)p).GetFouls());

        foreach (var player in candidates)
        {
            if (player is HockeyPlayer)
            {
                ((HockeyPlayer)player).DisplayPlayer();
            }
            else if (player is BasketballPlayer)
            {
                ((BasketballPlayer)player).DisplayPlayer();
            }
        }
    }
}
