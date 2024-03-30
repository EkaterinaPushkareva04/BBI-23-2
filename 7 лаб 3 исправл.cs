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
        FootballTeam[] menTeams = new FootballTeam[3];
        menTeams[0] = new MenFootballTeam("Винлайн", 1, 1);
        menTeams[1] = new MenFootballTeam("ВанВин", 3, 1);
        menTeams[2] = new MenFootballTeam("Лидер", 5, 2);

        FootballTeam[] womenTeams = new FootballTeam[3];
        womenTeams[0] = new WomenFootballTeam("Чемпион", 7, 3);
        womenTeams[1] = new WomenFootballTeam("Звезда", 3, 3);
        womenTeams[2] = new WomenFootballTeam("Герой", 5, 3);

        FootballTeam winner = null;
        int maxPoints = 0;

        foreach (var team in menTeams)
        {
            for (int i = 0; i < womenTeams.Length; i++)
            {
                int scoreMen = team.GoalsScored - womenTeams[i].GoalsConceded;
                int scoreWomen = womenTeams[i].GoalsScored - team.GoalsConceded;

                if (scoreMen > scoreWomen)
                {
                    team.PlayMatch(3);
                }
                else if (scoreMen < scoreWomen)
                {
                    womenTeams[i].PlayMatch(3);
                }
                else
                {
                    team.PlayMatch(1);
                    womenTeams[i].PlayMatch(1);
                }
            }

            if (team.Points > maxPoints)
            {
                maxPoints = team.Points;
                winner = team;
            }
        }

        foreach (var team in womenTeams)
        {
            if (team.Points > maxPoints)
            {
                maxPoints = team.Points;
                winner = team;
            }
        }

        Console.WriteLine($"Победитель: {winner.Name} с {maxPoints} очками.");
    }
}
