using System;

class Competitor
{
    private string LastName;
    private string Society;
    private int Jump1;
    private int Jump2;
    private int TotalJump;
    private bool Disqualified;

    public Competitor(string lastName, string society, int jump1, int jump2)
    {
        LastName = lastName;
        Society = society;
        Jump1 = jump1;
        Jump2 = jump2;
        TotalJump = jump1 + jump2;
        Disqualified = false;
    }

    public void DisqualifyCompetitor()
    {
        Disqualified = true;
    }

    public bool IsDisqualified()
    {
        return Disqualified;
    }

    public static void MergeSortCompetitors(ref Competitor[] competitors)
    {
        if (competitors.Length <= 1)
            return;

        int middle = competitors.Length / 2;
        Competitor[] leftArray = new Competitor[middle];
        Competitor[] rightArray = new Competitor[competitors.Length - middle];

        for (int i = 0; i < middle; i++)
        {
            leftArray[i] = competitors[i];
        }

        for (int i = middle; i < competitors.Length; i++)
        {
            rightArray[i - middle] = competitors[i];
        }

        MergeSortCompetitors(ref leftArray);
        MergeSortCompetitors(ref rightArray);

        Merge(competitors, leftArray, rightArray);
    }

    private static void Merge(Competitor[] arr, Competitor[] left, Competitor[] right)
    {
        int leftIndex = 0;
        int rightIndex = 0;
        int mergeIndex = 0;

        while (leftIndex < left.Length && rightIndex < right.Length)
        {
            if (left[leftIndex].TotalJump >= right[rightIndex].TotalJump)
            {
                arr[mergeIndex] = left[leftIndex];
                leftIndex++;
            }
            else
            {
                arr[mergeIndex] = right[rightIndex];
                rightIndex++;
            }
            mergeIndex++;
        }

        while (leftIndex < left.Length)
        {
            arr[mergeIndex] = left[leftIndex];
            leftIndex++;
            mergeIndex++;
        }

        while (rightIndex < right.Length)
        {
            arr[mergeIndex] = right[rightIndex];
            rightIndex++;
            mergeIndex++;
        }
    }

    public static void SortCompetitorsByTotalJump(ref Competitor[] competitors)
    {
        MergeSortCompetitors(ref competitors);
    }

    public void DisplayCompetitor()
    {
        if (!IsDisqualified())
        {
            Console.WriteLine($"Фамилия: {LastName}\t | Общество: {Society}\t | Попытка 1: {Jump1}\t | Попытка 2: {Jump2}\t | Результат: {TotalJump}");
        }
    }
}

class Program
{
    static void Main()
    {
        Competitor[] competitors = new Competitor[5]
        {
            new Competitor("Ivanov ", "A", 5, 4),
            new Competitor("Petrov ", "B", 4, 3),
            new Competitor("Cidorov", "C", 2, 3),
            new Competitor("Dorokov ", "D", 3, 3),
            new Competitor("Epihov ", "E", 1, 2),
        };

        competitors[2].DisqualifyCompetitor();

        Competitor.SortCompetitorsByTotalJump(ref competitors);

        foreach (var competitor in competitors)
        {
            competitor.DisplayCompetitor();
        }
    }
}
