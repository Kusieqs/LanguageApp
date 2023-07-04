using System;
using System.Xml.Serialization;
namespace LanguageApp
{
	public static class Review
	{
		public static void MainReveiw(string language, string SystemOp, string unit, string slash)
		{
            
            int Mistakes = 0;
            ConsoleKeyInfo key;
            int Max = 0, Min = 0;
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

            XmlSerializer xml = new XmlSerializer(typeof(List<Word_Description>));
            StreamReader sr = new StreamReader($@"{SystemOp}{unit}{slash}{key.KeyChar}");
            List<Word_Description> MainList = xml.Deserialize(sr) as List<Word_Description>;

            if (MainList.Count != 0)
            {
                int CountingLines = MainList.Count;


                Console.ReadKey();

                if (Legendary == true)
                {
                    LegendaryLvl(SystemOp, slash, key, unit, ref Mistakes, CountingLines,MainList);
                }
                else
                {
                    Levels(SystemOp, slash, key,unit, Min, Max, ref Mistakes,CountingLines,MainList);
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
        private static void Levels(string SystemOp,string slash, ConsoleKeyInfo key, string unit, int Min, int Max, ref int Mistakes, int count, List<Word_Description> MainList)
        {
            Random random = new Random();
            int HowMany = random.Next(Min, Max + 1);

            if (count > HowMany)
            {
                for (int i = 0; i < HowMany; i++)
                {
                    int RandomWord = random.Next(0, MainList.Count);
                Backup1:
                    Console.Clear();
                    Console.Write(MainList[RandomWord].WordInYourLanguage + " - - - - - ");
                    string Attempt = Console.ReadLine();

                    if (MainList[RandomWord].Word.ToUpper() != Attempt.ToUpper())
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
                    Console.Write(MainList[RandomWord].Word + " - - - - - ");
                    Attempt = Console.ReadLine();

                    if (MainList[RandomWord].WordInYourLanguage.ToUpper() != Attempt.ToUpper()) 
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
                    MainList.RemoveAt(RandomWord);

                }
                Console.Clear();
                Console.WriteLine($"\nMistakes: {Mistakes}\nClick Enter to continue");
                Console.Read();
            }
            else
            {
                for (int i = 0; i < HowMany; i++)
                {
                    int RandomWord = random.Next(0, MainList.Count);
                Backup1:
                    Console.Clear();
                    Console.Write(MainList[RandomWord].WordInYourLanguage + " - - - - - ");
                    string Attempt = Console.ReadLine();

                    if (MainList[RandomWord].Word.ToUpper() != Attempt.ToUpper())
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
                    Console.Write(MainList[RandomWord].Word + " - - - - - ");
                    Attempt = Console.ReadLine();

                    if (MainList[RandomWord].WordInYourLanguage.ToUpper() != Attempt.ToUpper())
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
        private static void LegendaryLvl(string SystemOp, string slash, ConsoleKeyInfo key, string unit, ref int Mistakes, int count, List<Word_Description> MainList)
        {
            foreach (var Words in MainList)
            {
            Backup1:
                Console.Clear();
                Console.Write(Words.WordInYourLanguage + " - - - - - ");
                string Attempt = Console.ReadLine();

                if (Words.Word.ToUpper() != Attempt.ToUpper())
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
                Console.Write(Words.Word + " - - - - - ");
                Attempt = Console.ReadLine();

                if (Words.WordInYourLanguage.ToUpper() != Attempt.ToUpper())
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
    }
}

