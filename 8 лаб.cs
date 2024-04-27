using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

abstract class Task
{
    public string _text;
    public Task(string text) { _text = text; }
    public virtual void ParseText(string text) { }
}

class Task2 : Task
{
    private string _result;

    public Task2(string text) : base(text) { }

    public override void ParseText(string text)
    {
        string[] words = text.Split(' ');
        StringBuilder encryptedText = new StringBuilder();

        foreach (string word in words)
        {
            char[] charArray = word.ToCharArray();
            Array.Reverse(charArray);
            encryptedText.Append(new string(charArray) + " ");
        }

        _result = encryptedText.ToString().Trim();
    }

    public string DecryptText(string encryptedText)
    {
        string[] words = encryptedText.Split(' ');
        StringBuilder decryptedText = new StringBuilder();

        foreach (string word in words)
        {
            char[] charArray = word.ToCharArray();
            Array.Reverse(charArray);
            decryptedText.Append(new string(charArray) + " ");
        }

        return decryptedText.ToString().Trim();
    }

    public override string ToString()
    {
        return _result;
    }
}
class Task4 : Task
{


    public Task4(string text) : base(text) { }

    public int CalculateComplexity(string text)
    {
        int wordCount = Regex.Matches(text, @"\b\w+\b").Count;
        int punctuationCount = Regex.Matches(text, @"[^\w\s]").Count;

        return wordCount + punctuationCount;
    }
}
class Task6 : Task
{
    private Dictionary<int, int> syllableCounts;
    private int complexity;

    public Task6(string text) : base(text)
    {
        syllableCounts = new Dictionary<int, int>();
        CalculateComplexity();
    }

    private int CountSyllables(string word)
    {
        int count = 0;
        bool isVowel = false;

        foreach (char c in word.ToLower())
        {
            if ("aeiouаеёиоуыэюя".Contains(c))
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

    private int CountWords()
    {
        string[] words = _text.Split(new char[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        return words.Length;
    }

    private int CountPunctuation()
    {
        int count = 0;
        foreach (char c in _text)
        {
            if (char.IsPunctuation(c))
            {
                count++;
            }
        }
        return count;
    }

    public void CalculateComplexity()
    {
        string[] words = _text.Split(new char[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string word in words)
        {
            int syllables = CountSyllables(word);

            if (syllableCounts.ContainsKey(syllables))
            {
                syllableCounts[syllables]++;
            }
            else
            {
                syllableCounts.Add(syllables, 1);
            }
        }

        complexity = CountWords() + CountPunctuation();
    }

    public void PrintAnalysis()
    {
        Console.WriteLine("Syllable Counts:");
        foreach (var pair in syllableCounts)
        {
            Console.WriteLine($"{pair.Key} syllable(s): {pair.Value}");
        }

        Console.WriteLine($"Sentence Complexity: {complexity}");
    }
}
class Task8 : Task
{

    public Task8(string text) : base(text) { }

    public void SplitTextIntoLines()
    {
        int maxLength = 50;
        string[] words = _text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        string result = "";
        int currentLength = 0;

        foreach (string word in words)
        {
            if (currentLength + word.Length <= maxLength)
            {
                result += word + " ";
                currentLength += word.Length + 1;
            }
            else
            {
                Console.WriteLine(result);
                result = word + " ";
                currentLength = word.Length + 1;
            }
        }

        if (!string.IsNullOrEmpty(result))
        {
            Console.WriteLine(result);
        }
    }
}
class Task_9 : Task
{
    private Dictionary<string, string> _codes;
    public string crypted_text;

    public Task_9(string text) : base(text)
    {
        _codes = new Dictionary<string, string>();
    }

    public override string ToString()
    {
        return _text;
    }

    public override void ParseText(string text)
    {
        var sequences = new Dictionary<string, int>();
        for (int i = 0; i < text.Length - 1; i++)
        {
            var sequence = text.Substring(i, 2);
            if (!sequences.ContainsKey(sequence))
            {
                sequences[sequence] = 0;
            }
            sequences[sequence]++;
        }
        char code = '!';
        foreach (var sequence in sequences)
        {
            if (!_codes.ContainsValue(code.ToString()))
            {
                _codes[sequence.Key] = code.ToString();
                code++;
            }
        }
    }

    public void EncodeText()
    {
        foreach (var codePair in _codes)
        {
            _text = _text.Replace(codePair.Key, codePair.Value);
        }
        crypted_text = _text;
    }

    public void DisplayCodesTable()
    {
        Console.WriteLine("Table of Codes:");
        foreach (var codePair in _codes)
        {
            Console.WriteLine($"Sequence: {codePair.Key} -> Code: {codePair.Value}");
        }
    }
}
class Task_10 : Task
{
    private string _text;
    private Dictionary<string, string> _codes;
    public Task_10(string text) : base(text)
    {
        _text = text;
        _codes = new Dictionary<string, string>();
    }
    public override string ToString()
    {
        return _text;
    }
    public override void ParseText(string text)
    {
        var sequences = new Dictionary<string, int>();
        for (int i = 0; i < text.Length - 1; i++)
        {
            var sequence = text.Substring(i, 2);
            if (!sequences.ContainsKey(sequence))
            {
                sequences[sequence] = 0;
            }
            sequences[sequence]++;
        }
        char code = '!';
        foreach (var sequence in sequences)
        {
            if (!_codes.ContainsKey(sequence.Key))
            {
                _codes[sequence.Key] = code.ToString();
                code++;
            }
        }
    }
    public void DecodeText()
    {
        foreach (var codePair in _codes)
        {
            _text = _text.Replace(codePair.Value, codePair.Value);
        }
    }
}

class Program
{
    static void Main()
    {
        string text = "Фьорды – это ущелья, формирующиеся ледниками и заполняющиеся морской водой. " +
            "Название происходит от древнескандинавского слова \"fjǫrðr\". Эти глубокие заливы, окруженные высокими горами, представляют захватывающие виды и природную красоту. " +
            "Они популярны среди туристов и известны под разными названиями: в Норвегии – \"фьорды\", в Шотландии – \"фьордс\", в Исландии – \"фьордар\". " +
            "Фьорды играют важную роль в культуре и туризме региона, продолжая вдохновлять людей со всего мира.";

        Task2 task2 = new Task2(text);
        task2.ParseText(text);

        Console.WriteLine("Encrypted Text:");
        Console.WriteLine(task2.ToString());

        string encryptedText = task2.ToString();
        string decryptedText = task2.DecryptText(encryptedText);

        Console.WriteLine("\nDecrypted Text:");
        Console.WriteLine(decryptedText);


        Task4 task4 = new Task4(text);
        int complexity = task4.CalculateComplexity(text);
        Console.WriteLine($"Complexity of the sentence: {complexity}");

        Task6 task6 = new Task6(text);
        task6.PrintAnalysis();

        Task8 task8 = new Task8(text);
        task8.SplitTextIntoLines();

        Task_9 task9 = new Task_9(text);
        task9.ParseText(text);
        task9.EncodeText();
        Console.WriteLine("Зашифрованный текст:");
        Console.WriteLine(task9.ToString());
        Console.WriteLine();
        task9.DisplayCodesTable();

        Task_10 task10 = new Task_10(text);
        task10.ParseText(text);
        task10.DecodeText();
        Console.WriteLine("Расшифрованный текст:");
        Console.WriteLine(task10.ToString());
        Console.ReadKey();
    }
}



