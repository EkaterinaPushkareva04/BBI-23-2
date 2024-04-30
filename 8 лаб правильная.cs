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
    private string[] originalWords;

    public Task2(string text) : base(text) { }

    public override void ParseText(string text)
    {
        List<int> spaceIndices = new List<int>();
        originalWords = Regex.Split(text, @"(\W)").Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == ' ')
            {
                spaceIndices.Add(i);
            }
        }

        for (int i = 0; i < originalWords.Length - 1; i++)
        {
            if (originalWords[i] != null && originalWords[i + 1] != null)
            {
                int spaceIndex = spaceIndices.FirstOrDefault(x => x > text.IndexOf(originalWords[i]) && x < text.IndexOf(originalWords[i + 1]));
                if (spaceIndex != -1)
                {
                    originalWords[i] += " ";
                }
            }
        }
    }
    public string EncryptAndDecryptText()
    {
        ParseText(_text);

        string[] words = Regex.Split(_text, @"(\W)").ToArray();

        string[] processedWords = new string[words.Length];

        for (int i = 0; i < words.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(words[i]))
            {
                char[] characters = words[i].ToCharArray();
                Array.Reverse(characters);
                processedWords[i] = new string(characters);
            }
            else
            {
                processedWords[i] = words[i];
            }
        }
        string encryptedText = string.Join("", processedWords);

        string[] decryptedWords = Regex.Split(encryptedText, @"(\W)").ToArray();

        for (int i = 0; i < decryptedWords.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(decryptedWords[i]))
            {
                char[] characters = decryptedWords[i].ToCharArray();
                Array.Reverse(characters);
                decryptedWords[i] = new string(characters);
            }
        }


        string decryptedText = string.Join("", decryptedWords);

        Console.WriteLine("Зашифрованный: " + encryptedText);
        Console.WriteLine("Расшифрованный: " + decryptedText);

        return "Зашифрованный и расшифрованный текст выведен в консоль.";
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

        public void AlignText(int width)
        {
            string[] words = _text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> lines = new List<string>();
            string currentLine = "";

            foreach (string word in words)
            {
                if ((currentLine + word).Length + 1 <= width)
                {
                    currentLine += word + " ";
                }
                else
                {
                    lines.Add(currentLine.Trim());
                    currentLine = word + " ";
                }
            }

            lines.Add(currentLine.Trim());

            foreach (string line in lines)
            {
                string[] lineWords = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int totalLength = lineWords.Sum(w => w.Length);
                int numGaps = lineWords.Length - 1;
                if (numGaps != 0)
                {
                    int totalGapsWidth = width - totalLength;
                    int baseGapWidth = totalGapsWidth / numGaps;
                    int extraSpaces = totalGapsWidth % numGaps;

                    string formattedLine = "";
                    for (int i = 0; i < lineWords.Length - 1; i++)
                    {
                        formattedLine += lineWords[i] + new string(' ', baseGapWidth);
                        if (extraSpaces > 0)
                        {
                            formattedLine += " ";
                            extraSpaces--;
                        }
                    }
                    formattedLine += lineWords[lineWords.Length - 1];
                    Console.WriteLine(formattedLine);
                }

                else
                {
                    Console.WriteLine("Ошибка: деление на ноль!");
                }
            }
        }

        public override string ToString()
        {
            return _text;
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
            return crypted_text;
        }

        public override void ParseText(string text)
        {
            var sequences = new Dictionary<string, int>();
            for (int i = 0; i < text.Length - 1; i++)
            {
                if (char.IsLetter(text[i]) && char.IsLetter(text[i + 1]))
                {
                    var sequence = text.Substring(i, 2);
                    if (!sequences.ContainsKey(sequence))
                    {
                        sequences[sequence] = 0;
                    }
                    sequences[sequence]++;
                }
            }

            var topSequences = sequences.OrderByDescending(x => x.Value).Take(5).ToList();
            char code = '!';
            foreach (var sequence in topSequences)
            {
                _codes[sequence.Key] = code.ToString();
                text = text.Replace(sequence.Key, code.ToString());
                code++;
            }

            crypted_text = text;
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
        private Dictionary<string, string> _codes;
        private string _text;

        public Task_10(string text) : base(text)
        {
            _codes = new Dictionary<string, string>();
            _text = text;
        }


        public override string ToString()
        {
            return _text;
        }

        public override void ParseText(string text)
        {
            foreach (var codePair in _codes)
            {
                _text = _text.Replace(codePair.Value, codePair.Key);
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
            task2.EncryptAndDecryptText();

            Task4 task4 = new Task4(text);
            int complexity = task4.CalculateComplexity(text);
            Console.WriteLine($"Complexity of the sentence: {complexity}");

            Task6 task6 = new Task6(text);
            task6.PrintAnalysis();

            Task8 task8 = new Task8(text);
            task8.AlignText(50);

            Task_9 task9 = new Task_9(text);
            task9.ParseText(text);
            Console.WriteLine("Закодированный текст:");
            Console.WriteLine(task9.ToString());
            Console.WriteLine();
            task9.DisplayCodesTable();

            Task_10 task10 = new Task_10(text);
            task10.ParseText(task9.ToString());
            Console.WriteLine("Раскодированный текст:");
            Console.WriteLine(task10.ToString());

            Console.ReadKey();
        }
    }
}