using System;

class Competitor
{
    private string LastName;
    private string Society;
    private int Jump1;
    private int Jump2;
    private int TotalJump;

    public Competitor(string lastName, string society, int jump1, int jump2)
    {
        LastName = lastName;
        Society = society;
        Jump1 = jump1;
        Jump2 = jump2;
        TotalJump = jump1 + jump2;
    }

    public static void SortCompetitorsByTotalJump(ref Competitor[] competitors)
    {
        int n = competitors.Length;
        int gap = n / 2;

        while (gap > 0)
        {
            for (int i = gap; i < n; i++)
            {
                Competitor temp = competitors[i];
                int j = i;

                while (j >= gap && competitors[j - gap].TotalJump < temp.TotalJump)
                {
                    competitors[j] = competitors[j - gap];
                    j -= gap;
                }

                competitors[j] = temp;
            }

            gap /= 2;
        }
    }
    public void DisplayCompetitor()
    {
        Console.WriteLine($"{LastName}\t{Society}\t{Jump1}\t{Jump2}\t{TotalJump}");
    }
}

class Program
{
    static void Main()
    {
        Competitor[] competitors = new Competitor[5]
        {
            new Competitor("Ivanov", "A", 5, 4),
            new Competitor("Petrov", "B", 4, 3),
            new Competitor("Cidorov", "C", 2, 3),
            new Competitor("Dorokov", "D", 3, 3),
            new Competitor("Epihov", "E", 1, 2),
        };

        Competitor.SortCompetitorsByTotalJump(ref competitors);

        Console.WriteLine("Фамилия\tОбщество\tПопытка 1\tПопытка 2\tСумма баллов");
        foreach (var competitor in competitors)
        {
            competitor.DisplayCompetitor();
        }
    }
}

