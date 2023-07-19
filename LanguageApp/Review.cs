using System;
using System.Xml.Serialization;
using Newtonsoft.Json;
namespace LanguageApp
{
	public static class Review
	{
		public static void MainReview(string language, string systemOp, string unit) 
		{
            
            int mistakes = 0; 
            ConsoleKeyInfo key;
            int max = 0, min = 0;
            bool legendary = false;
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
                    if (k1.KeyChar == '1' || k1.KeyChar == '2' || k1.KeyChar == '3' || k1.KeyChar == '4')
                    {
                        HowManyWords(ref max, ref min, k1, ref legendary);
                        correctAnswer = true;
                    }
                }

            } while (!correctAnswer);


            string json = File.ReadAllText(Path.Combine(systemOp, language, unit, key.KeyChar.ToString()));
            List<WordDescription> mainList = JsonConvert.DeserializeObject<List<WordDescription>>(json);

            if (mainList.Count > 0) 
            {
                Console.ReadKey();

                if (legendary == true)
                {
                    LegendaryLvl(ref mistakes, mainList);
                }
                else
                {
                    Levels(min, max, ref mistakes,mainList);
                }

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error\nFile with words or expresions is empty\nPress Enter to continue");
                Console.ReadKey();
                Console.ResetColor();
            }

        }// choosing which range of words and lvl
        private static void Levels(int min, int max, ref int mistakes,List<WordDescription> mainList) 
        {
            string attempt;
            bool correctAnswer = false;
            int count = mainList.Count;
            Random random = new Random();
            int howMany = random.Next(min, max + 1);

            if (count > howMany)
            {
                for (int i = 0; i < howMany; i++)
                {
                    int randomWord = random.Next(0, mainList.Count);

                    do
                    {
                        Console.Clear();
                        Console.Write(mainList[randomWord].WordInYourLanguage + ": ".PadLeft(5));
                        attempt = Console.ReadLine();

                        if (mainList[randomWord].Word.ToUpper() != attempt.ToUpper()) 
                        {
                            mistakes++;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                            Console.ResetColor();
                            Console.Read();
                            correctAnswer = true;
                        }
                    } while (!correctAnswer);
                    correctAnswer = false;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Correct\nClick Enter to continue");
                    Console.ResetColor();
                    Console.ReadKey();

                    do
                    {
                        Console.Clear();
                        Console.Write(mainList[randomWord].Word + ": ".PadLeft(5));  
                        attempt = Console.ReadLine();

                        if (mainList[randomWord].WordInYourLanguage.ToUpper() != attempt.ToUpper())
                        {
                            mistakes++;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                            Console.ResetColor();
                            Console.Read();
                            correctAnswer = true;
                        }

                    } while (!correctAnswer);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nCorrect\nClick Enter to continue");
                    Console.Read();
                    Console.ResetColor();
                    Console.Clear();

                    mainList.RemoveAt(randomWord);
                }
                Console.Clear();
                Console.WriteLine($"\nMistakes: {mistakes}\nClick Enter to continue");
                Console.Read();
            }
            else
            {
                for (int i = 0; i < howMany; i++)
                {
                    int randomWord = random.Next(0, mainList.Count);
                    do
                    {
                        Console.Clear();
                        Console.Write(mainList[randomWord].WordInYourLanguage + ": ".PadLeft(5));
                        attempt = Console.ReadLine();

                        if (mainList[randomWord].Word.ToUpper() != attempt.ToUpper())
                        {
                            mistakes++;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                            Console.ResetColor();
                            Console.Read();
                            correctAnswer = true;
                        }
                    } while (!correctAnswer);
                    correctAnswer = false;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Correct\nClick Enter to continue");
                    Console.ResetColor();
                    Console.ReadKey();

                    do
                    {
                        Console.Clear();
                        Console.Write(mainList[randomWord].Word + ": ".PadLeft(5));
                        attempt = Console.ReadLine();

                        if (mainList[randomWord].WordInYourLanguage.ToUpper() != attempt.ToUpper())
                        {
                            mistakes++;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                            Console.ResetColor();
                            Console.Read();
                            correctAnswer = true;
                        }
                    } while (!correctAnswer);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nCorrect\nClick Enter to continue");
                    Console.Read();
                    Console.ResetColor();
                    Console.Clear();
                }
            }
            Console.Clear();
            Console.WriteLine($"\nMistakes: {mistakes}\nClick Enter to continue");
            Console.Read();

        }// Learning random words from the list
        private static void LegendaryLvl(ref int Mistakes, List<WordDescription> mainList) 
        {
            string attempt;
            int count = mainList.Count;
            bool correctAnswer = false;
            foreach (var words in mainList)
            {
                do
                {
                    Console.Clear();
                    Console.Write(words.WordInYourLanguage + ": ".PadLeft(5));
                    attempt = Console.ReadLine();

                    if (words.Word.ToUpper() != attempt.ToUpper())
                    {
                        Mistakes++;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                        Console.ResetColor();
                        Console.Read();
                        correctAnswer = true;
                    }
                } while (!correctAnswer);
                correctAnswer = false;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Correct\nClick Enter to continue");
                Console.ResetColor();
                Console.ReadKey();

                do
                {
                    Console.Clear();
                    Console.Write(words.Word + ": ".PadLeft(5));
                    attempt = Console.ReadLine();

                    if (words.WordInYourLanguage.ToUpper() != attempt.ToUpper())
                    {
                        Mistakes++;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                        Console.ResetColor();
                        Console.Read();
                        correctAnswer = true;
                    }
                } while (!correctAnswer);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nCorrect\nClick Enter to continue");
                Console.Read();
                Console.ResetColor();
                Console.Clear();

            }
            Console.Clear();
            Console.WriteLine($"\nMistakes: {Mistakes}\nClick Enter to continue");
            Console.Read();

        } // Learning all the words
        private static void HowManyWords(ref int max, ref int min,ConsoleKeyInfo k1, ref bool legendary)
        {

            switch(k1.KeyChar)
            {
                case '1':
                    min = 10;
                    max = 10;
                    break;
                case '2':
                    min = 10;
                    max = 30;
                    break;
                case '3':
                    min = 30;
                    max = 50;
                    break;
                case '4':
                    legendary = true;
                    break;
            }

        } // assinging a range of words
    }
}

