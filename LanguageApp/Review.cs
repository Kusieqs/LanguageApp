using System;
namespace LanguageApp
{
	public static class Review
	{
        private static void Levels(string SystemOp, string unit, string slash, ConsoleKeyInfo key, int Min, int Max, int count, ref int Mistakes)
        {
            Random random = new Random();
            List<string> WordsInLanguage = new List<string>();
            StreamReader rdl = new StreamReader($@"{SystemOp}{unit}{slash}{key.KeyChar}");
            string teskt1;
            int HowMany = random.Next(Min, Max + 1);
            do
            {
                teskt1 = "";
                teskt1 = rdl.ReadLine();
                WordsInLanguage.Add(teskt1);
            } while (teskt1 != null);

            WordsInLanguage.Remove(WordsInLanguage.Last());


            if (count > HowMany)
            {
                for (int i = 0; i < HowMany; i++)
                {
                    int RandomWord = random.Next(0, WordsInLanguage.Count);
                    string[] SplitWord = WordsInLanguage[RandomWord].Split('|');
                Backup1:
                    Console.Clear();
                    Console.WriteLine(SplitWord[1] + " - - - ");
                    Console.SetCursorPosition(SplitWord[1].Length + 7, 0);
                    string Attempt = Console.ReadLine();

                    if (SplitWord[0].ToUpper() != Attempt.ToUpper())
                    {
                        Mistakes++;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                        Console.ResetColor();
                        Console.Read();
                        goto Backup1;
                    }
                    Console.WriteLine("Correct\nClick Enter to continue");
                Backup2:
                    Console.Clear();
                    Console.Write(SplitWord[0] + " - - - ");
                    Console.SetCursorPosition(SplitWord[0].Length + 7, 0);
                    Attempt = Console.ReadLine().ToUpper();

                    if (SplitWord[1].ToUpper() != Attempt)
                    {
                        Mistakes++;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                        Console.ResetColor();
                        Console.Read();
                        goto Backup2;
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nCorrect\nClick Enter to continue");
                    Console.Read();
                    Console.ResetColor();
                    Console.Clear();
                    WordsInLanguage.RemoveAt(RandomWord);

                }
                Console.Clear();
                Console.WriteLine($"\nMistakes: {Mistakes}\nClick Enter to continue");
                Console.Read();
            }
            else
            {
                for (int i = 0; i < HowMany; i++)
                {
                    int RandomWord = random.Next(0, WordsInLanguage.Count);
                    string[] SplitWord = WordsInLanguage[RandomWord].Split('|');
                Backup1:
                    Console.Clear();
                    Console.WriteLine(SplitWord[1] + " - - - ");
                    Console.SetCursorPosition(SplitWord[1].Length + 7, 0);
                    string Attempt = Console.ReadLine();

                    if (SplitWord[0].ToUpper() != Attempt.ToUpper())
                    {
                        Mistakes++;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                        Console.ResetColor();
                        Console.Read();
                        goto Backup1;
                    }
                    Console.WriteLine("Correct\nClick Enter to continue");
                Backup2:
                    Console.Clear();
                    Console.Write(SplitWord[0] + " - - - ");
                    Console.SetCursorPosition(SplitWord[0].Length + 7, 0);
                    Attempt = Console.ReadLine().ToUpper();

                    if (SplitWord[1].ToUpper() != Attempt)
                    {
                        Mistakes++;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                        Console.ResetColor();
                        Console.Read();
                        goto Backup2;
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nCorrect\nClick Enter to continue");
                    Console.Read();
                    Console.ResetColor();
                    Console.Clear();


                }
            }
            Console.Clear();
            Console.WriteLine($"\nMistakes: {Mistakes}\nClick Enter to continue");
            Console.Read();

        }
        private static void LegendaryLvl(string SystemOp, string slash, ConsoleKeyInfo key, string unit, ref int Mistakes, int count)
        {
            string tekst1 = "";
            StreamReader rdl = new StreamReader($@"{SystemOp}{unit}{slash}{key.KeyChar}");
            for (int i = 0; i < count; i++)
            {
                tekst1 = rdl.ReadLine();
                string[] Tab = tekst1.Split('|');
            Backup1:
                Console.Clear();
                Console.WriteLine(Tab[1] + " - - - ");
                Console.SetCursorPosition(Tab[1].Length + 7, 0);
                string Attempt = Console.ReadLine();

                if (Tab[0].ToUpper() != Attempt.ToUpper())
                {
                    Mistakes++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                    Console.ResetColor();
                    Console.Read();
                    goto Backup1;
                }
                Console.WriteLine("Correct\nClick Enter to continue");
            Backup2:
                Console.Clear();
                Console.Write(Tab[0] + " - - - ");
                Console.SetCursorPosition(Tab[0].Length + 7, 0);
                Attempt = Console.ReadLine().ToUpper();

                if (Tab[1].ToUpper() != Attempt)
                {
                    Mistakes++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                    Console.ResetColor();
                    Console.Read();
                    goto Backup2;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nCorrect\nClick Enter to continue");
                Console.Read();
                Console.ResetColor();
                Console.Clear();
            }
            Console.Clear();
            Console.WriteLine($"\nMistakes: {Mistakes}\nClick Enter to continue");
            Console.Read();


        }
        private static void HowManyWords(ref int max, ref int min,ConsoleKeyInfo k1, ref bool Legendary)
        {
            if(k1.KeyChar == '1')
            {
                min = 10;
                max = 10;
            }
            else if(k1.KeyChar =='2')
            {
                min = 10;
                max = 30;
            }
            else if (k1.KeyChar =='3')
            {
                min = 30;
                max = 50;
            }
            else if (k1.KeyChar =='4')
            {
                Legendary = true;
            }
        }
		public static void MainReveiw(string language, string SystemOp, string unit, string slash)
		{
            int Mistakes = 0;
            ConsoleKeyInfo key;
            int Max=0, Min=0, count = -1;
            bool Legendary = false;
            bool correctAnswer = false;
            do
            {

                Console.Clear();
                Console.WriteLine("Do you want review expresion or words or diffrent? W/E/D");
                key = Console.ReadKey();
                Console.Clear();

                if (key.KeyChar == 'W' || key.KeyChar == 'E' || key.KeyChar == 'D')
                {
                    Console.WriteLine("Choose level: \n1.Easy (10 exercises)\n\n2.Medium (10-30 exercises)\n\n3.Hard (30-50 exercises)\n\n4.Legendary (All of words)\n");

                    ConsoleKeyInfo k1 = new ConsoleKeyInfo();
                    k1 = Console.ReadKey();
                    if (k1.KeyChar == '1' || k1.KeyChar=='2' ||k1.KeyChar =='3' || k1.KeyChar =='4')
                    {
                        HowManyWords(ref Max, ref Min, k1, ref Legendary);
                        correctAnswer = true;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                    continue;
            } while (correctAnswer == false);


            SystemOp += language + slash;


            string review = File.ReadAllText($@"{SystemOp}{unit}{slash}{key.KeyChar}");

            if ((!string.IsNullOrWhiteSpace(review)))
            {
                StreamReader rd = new StreamReader($@"{SystemOp}{unit}{slash}{key.KeyChar}");
                string teskt = "";
                do
                {
                    teskt = rd.ReadLine();
                    count++;
                } while (teskt != null);
                rd.Close();


                Console.ReadKey();

                if (Legendary == true)
                {
                    LegendaryLvl(SystemOp, slash, key, unit, ref Mistakes,count);
                }
                else
                {
                    Levels(SystemOp, unit, slash, key, Min, Max, count, ref Mistakes);
                }

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error\nFile with words or expresions is empty\nPress Enter to continue");
                Console.ReadKey();
                Console.ResetColor();
            }

        }
    }
}

