using System;

class FootballTeam
{
    private string name;
    private int goalsScored;
    private int goalsConceded;
    private int points;

    public string Name => name;
    public int GoalsScored => goalsScored;
    public int GoalsConceded => goalsConceded;
    public int Points => points;

    public FootballTeam(string name, int goalsScored, int goalsConceded)
    {
        this.name = name;
        this.goalsScored = goalsScored;
        this.goalsConceded = goalsConceded;
        points = 0;
    }

    public void PlayMatch(int result)
    {
        if (result == 1)
        {
            points += 3;
        }
        else if (result == 0)
        {
            points += 0;
        }
        else
        {
            points += 1;
        }
    }

    public string PrintTeamInfo()
    {
        return $"{Name}: {Points} очков";
    }
}

class WomenFootballTeam : FootballTeam
{
    public WomenFootballTeam(string name, int goalsScored, int goalsConceded) : base(name, goalsScored, goalsConceded)
    {
    }
}

class MenFootballTeam : FootballTeam
{
    public MenFootballTeam(string name, int goalsScored, int goalsConceded) : base(name, goalsScored, goalsConceded)
    {
    }
}

class Program
{
    static void Main()
    {
        FootballTeam[] teams = new FootballTeam[6];
        teams[0] = new MenFootballTeam("Винлайн", 1, 1);
        teams[1] = new MenFootballTeam("ВанВин", 3, 1);
        teams[2] = new MenFootballTeam("Лидер", 5, 2);
        teams[3] = new WomenFootballTeam("Чемпион", 4, 3);
        teams[4] = new WomenFootballTeam("Звезда", 3, 3);
        teams[5] = new WomenFootballTeam("Герой", 1, 3);

        for (int i = 0; i < teams.Length; i++)
        {
            for (int j = i + 1; j < teams.Length; j++)
            {
                int scoreTeam1 = teams[i].GoalsScored - teams[j].GoalsConceded;
                int scoreTeam2 = teams[j].GoalsScored - teams[i].GoalsConceded;

                if (scoreTeam1 > scoreTeam2)
                {
                    teams[i].PlayMatch(3);
                    teams[j].PlayMatch(0);
                }
                else if (scoreTeam1 < scoreTeam2)
                {
                    teams[i].PlayMatch(0);
                    teams[j].PlayMatch(3);
                }
                else
                {
                    teams[i].PlayMatch(1);
                    teams[j].PlayMatch(1);
                }
            }
        }

        Array.Sort(teams, (x, y) => y.Points.CompareTo(x.Points));

        Console.WriteLine("Таблица результатов (отсортировано по очкам):");
        for (int i = 0; i < teams.Length; i++)
        {
            string teamType = teams[i] is MenFootballTeam ? "мужская команда" : "женская команда";
            Console.WriteLine($"{i + 1}. {teams[i].Name} {teamType}: {teams[i].Points} очков");
        }
    }
}
