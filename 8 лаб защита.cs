using System;
using System.Collections.Generic;

abstract class Task
{
    public string _text;
    public Task(string text) { _text = text; }
    public virtual void ParseText(string text) { }
}

struct SyllableCount
{
    public int syllables;
    public int wordCount;

    public SyllableCount(int s, int w)
    {
        syllables = s;
        wordCount = w;
    }

    public void IncreaseSyllables()
    {
        syllables++;
    }
}

class Task6 : Task
{
    private List<SyllableCount> syllableCounts;

    public Task6(string text) : base(text)
    {
        syllableCounts = new List<SyllableCount>();
        CalculateSyllableCounts();
    }

    private int CountSyllables(string word)
    {
        int count = 0;
        bool isVowel = false;

        foreach (char c in word.ToLower())
        {
            if ("аеёиоуыэюя".Contains(c))
            {
                if (!isVowel)
                {
                    count++;
                    isVowel = true;
                }
            }
            else
            {
                isVowel = false;
            }
        }

        return count;
    }

    private void UpdateSyllableCount(int syllables)
    {
        int wordCount = 0;
        bool found = false;
        foreach (SyllableCount sc in syllableCounts)
        {
            if (sc.syllables == syllables)
            {
                wordCount++;
                found = true;
                break; 
            }
        }

        if (!found)
        {
            syllableCounts.Add(new SyllableCount(syllables, 1)); 
        }
    }





    public void CalculateSyllableCounts()
    {
        string[] words = _text.Split(new char[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string word in words)
        {
            int syllables = CountSyllables(word);
            bool found = false;

            foreach (SyllableCount sc in syllableCounts)
            {
                if (sc.syllables == syllables)
                {
                    sc.IncreaseSyllables();
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                syllableCounts.Add(new SyllableCount(syllables, 1));
            }
        }
    }

    public void PrintAnalysis()
    {
        Console.WriteLine("Syllable Counts:");
        foreach (SyllableCount sc in syllableCounts)
        {
            Console.WriteLine($"{sc.syllables} syllable(s): {sc.wordCount}");
        }
    }


    class Program
    {
        static void Main()
        {
            string text = "Фьорды – это ущелья, формирующиеся ледниками и заполняющиеся морской водой. " +
                "Название происходит от древнескандинавского слова \"fjǫrðr\". Эти глубокие заливы, окруженные высокими горами, представляют захватывающие виды и природную красоту. " +
                "Они популярны среди туристов и известны под разными названиями: в Норвегии – \"фьорды\", в Шотландии – \"фьордс\", в Исландии – \"фьордар\". " + "Фьорды играют важную роль в культуре и туризме региона, продолжая вдохновлять людей со всего мира.";

            Task6 task6 = new Task6(text);
            task6.PrintAnalysis();
        }
    }
}
