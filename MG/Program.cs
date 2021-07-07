using System;
using System.Text;
using System.Media;

namespace Millionaire
{
    class Program
    {
        const byte QuestionCount = 10;
        const byte ChoiceCount = 3;

        static void RandomizeChoices(int[] numbers)
        {
            var random = new Random();
            for (int i = 0; i < ChoiceCount; i++)
            {
                numbers[i] = random.Next(0, ChoiceCount);

                for (int j = 0; j < i + 1 && i > 0; j++)
                {
                    if (numbers[j] == numbers[i] && i != j)
                    {
                        numbers[i] = random.Next(0, ChoiceCount);
                        j = -1;
                    }
                }
            }
        }
        static string[] GetAnswers()
        {
            string[] answers = {"AZERBAIJAN ARMY", "ILHAM ALIYEV", "Tamer Shahin", "Paradise, Nevada", "Adile Nashit", "Kamran Aliyev",
            "ELVIN CAMALZADE", "Jumping over an office chair", "Scarface", "Golf"};
            return answers;
        }
        static void ConfirmOption(ref char option, char[] symbols)
        {
            while (option != symbols[0] && option != symbols[1] && option != symbols[2])
            {
                Console.Write("Only --> (A, B, C): ");
                option = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();
            }
        }
        static bool IsTrueAnswer(in string choice, in string answer)
        {
            return choice.EndsWith(answer);
        }
        static string ChoosedOption(in char option, StringBuilder[] text)
        {
            for (int i = 0; i < ChoiceCount; i++)
                if (text[i][0] == option)
                    return text[i].ToString();

            return null;
        }
        static void WhoWantsToBeMillionaire(in string[] questions, in string[][] choices)
        {
            string[] answers = GetAnswers();
            int[] prizes = GetPrizes();

            byte currentQuestionNumber = 0;
            byte choiceCount = 0;
            int point = 0;
            const string GameName = " MILLIONAIRE ";
            while (currentQuestionNumber != QuestionCount)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine();
                Console.Write(new string(' ', (Console.WindowWidth - GameName.Length) / 2));
                Console.WriteLine(GameName);
                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;

                char[] symbols = { 'A', 'B', 'C' };
                int[] numbers = { -1, -1, -1 };

                StringBuilder[] text = new StringBuilder[ChoiceCount];
                for (int i = 0; i < text.Length; i++)
                    text[i] = new StringBuilder();

                RandomizeChoices(numbers);
                Console.WriteLine($"{currentQuestionNumber + 1}.{questions[currentQuestionNumber]}");

                for (int j = choiceCount; j < choiceCount + ChoiceCount; j++)
                {
                    int currentChoice = j - choiceCount; int index = numbers[currentChoice];
                    text[currentChoice].Append(symbols[currentChoice] + ")" + choices[currentQuestionNumber][index]);
                    Console.Write($"{text[currentChoice]}    ");
                }

                Console.WriteLine();
                Console.Write("Pick : ");
                char option = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();

                ConfirmOption(ref option, symbols);
                string choosedOption = ChoosedOption(option, text);

                if (IsTrueAnswer(choosedOption, answers[currentQuestionNumber]))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("It is true answer, Congratulations!");
                    point += 10;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"It is Wrong answer! True answer is --> {answers[currentQuestionNumber]} !");
                    if (point != 0)
                        point -= 10;
                }

                Console.ForegroundColor = ConsoleColor.White;

                if (currentQuestionNumber != 9)
                {
                    StringBuilder nextQuestionEntry = new StringBuilder(" NEXT QUESTION, " + " its value is " + prizes[currentQuestionNumber].ToString("C") + " $");
                    Console.Write(new string(' ', (Console.WindowWidth - nextQuestionEntry.Length) / 2));
                    Console.WriteLine(nextQuestionEntry);
                }

                System.Threading.Thread.Sleep(777);
                Console.Clear();

                ++currentQuestionNumber;
                choiceCount += 3;
            }
            Console.Clear();
            Console.WriteLine($"Your point is --> {point} of 100!");

            int money = (point != 0) ? prizes[(point / 10) - 1] : 0;
            Console.WriteLine($" Your prize is {money.ToString("C")}");
        }
        static string[] GetQuestions()
        {
            string[] questions = { "Which army is the best  ?", "Who is the best President in the world ?",
            "Who was the first hacker arrested in Turkey ?", "Where is the Luxor Hotel & Casino located ?", "Which celebrity doesn't have twins ?",
            "Which famous university graduate ?",
            "Who is the best teacher in the STEP ACADEMY ?",
            "In a 1994 CBS interview, Microsoft co-founder Bill Gates performed what unusual trick on camera ?",
            "Which movie contains the quote, \"Say hello to my little friend!\"?",
            "What was the first sport to have been played on the moon?"};

            return questions;
        }
        static string[][] GetChoices()
        {
            string[][] choices = new string[][]
           {
                new string[ChoiceCount] {"AZERBAIJAN ARMY",  "armenian sheeps", "russian chickens"},
                new string[ChoiceCount] {"pashik",  "putin", "ILHAM ALIYEV"},
                new string[ChoiceCount] {"Tamer Shahin",  "Huseyn Rustamov", "Kamran Aliyev"},
                new string[ChoiceCount] { "Paradise, Nevada", "Las Vegas, Nevada", "Jackpot, Nevada"},
                new string[ChoiceCount] { "Adile Nashit", "MemmedAga", "Miraga"},
                new string[ChoiceCount] { "Mestan", "Toplan", "Kamran Aliyev"},
                new string[ChoiceCount] { "ELVIN CAMALZADE", "FILANKES", "FILANKES"},
                new string[ChoiceCount] { "Standing on his head", "Jumping over an office chair", "Jumping backwards over a desk"},
                new string[ChoiceCount] { "Scarface", "Reservoir Dogs", "Goodfellas"},
                new string[ChoiceCount] { "Golf", "Tennis", "Soccer" }
           };
            return choices;
        }

        static int[] GetPrizes()
        {
            int[] prizes = { 1_000, 2_000, 4_000, 16_000, 32_000, 64_000, 125_000, 250_000, 500_000, 1_000_000 };
            return prizes;
        }
        static void ConsoleSettings()
        {
            Console.Title = " MILLIONAIRE ";
            Console.CursorVisible = false;
        }
        static void Play()
        {
            ConsoleSettings();
            string[] questions = GetQuestions();
            string[][] choices = GetChoices();
            WhoWantsToBeMillionaire(questions, choices);
        }
        static void Main(string[] args)
        {
            Play();
        }
    }
}
