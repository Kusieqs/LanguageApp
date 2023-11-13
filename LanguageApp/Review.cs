using System;
using System.Xml.Serialization;
using Newtonsoft.Json;
namespace LanguageApp
{
    public static class Review
    {
        public static void MainReview(string language, string systemOp, string unit)
        {
            ConsoleKeyInfo key;
            int max = 0, min = 0;
            bool legendary = false, correctAnswer = false, mistakeLvl = false;

            do
            {

                Console.Clear();
                Console.WriteLine("Do you want review expresion or words or diffrent? W/E/D (Write 0 to exit)");
                key = Console.ReadKey();
                Console.Clear();

                if (key.KeyChar == 'W' || key.KeyChar == 'E' || key.KeyChar == 'D')
                {
                    Console.Write("Choose level: \n1.Easy (10 exercises)\n\n2.Medium (10-30 exercises)\n\n3.Hard (30-50 exercises)\n\n4.Legendary (All of words)\n\n5.Mistakes Level\n\n0.Exit\n\n\nNumber: ");

                    ConsoleKeyInfo k1 = new ConsoleKeyInfo();
                    k1 = Console.ReadKey();
                    if (k1.KeyChar == '1' || k1.KeyChar == '2' || k1.KeyChar == '3')
                    {
                        HowManyWords(ref max, ref min, k1);
                        correctAnswer = true;
                    }
                    else if (k1.KeyChar == '4')
                    {
                        legendary = true;
                        correctAnswer = true;
                    }
                    else if (k1.KeyChar == '5')
                    {
                        mistakeLvl = true;
                        correctAnswer = true;
                    }
                    else if (k1.KeyChar == '0')
                        return;
                }
                else if (key.KeyChar == '0')
                    return;

            } while (!correctAnswer);


            string json = File.ReadAllText(Path.Combine(systemOp, language, unit, key.KeyChar.ToString()+".json"));
            List<WordDescription> mainList = JsonConvert.DeserializeObject<List<WordDescription>>(json);

            if (mainList.Count > 0)
            {
                Console.ReadKey();

                if (legendary)
                {
                    LegendaryLvl(ref mainList);
                }
                else if (mistakeLvl)
                {
                    MistakeLevel(ref mainList);
                }
                else
                {
                    Levels(min, max, ref mainList);
                }

                string jsonWriter = JsonConvert.SerializeObject(mainList);
                File.WriteAllText(Path.Combine(systemOp, language, unit, key.KeyChar.ToString()+".json"), jsonWriter);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error\nFile with words or expresions is empty\nPress Enter to continue");
                Console.ReadKey();
                Console.ResetColor();
            }

        }// choosing which range of words and lvl
        private static void MistakeLevel(ref List<WordDescription> mainList)
        {
            int maxMistake = mainList.Max(x => x.Mistakes);
            List<WordDescription> copyList = mainList.Where(x => x.Mistakes == maxMistake || x.Mistakes == maxMistake - 1).ToList();
            string attempt;
            int mistakes = 0;
            bool correctAnswer = false;
            foreach (var words in copyList)
            {
                int howManyAttempts = 3;
                do
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine($"Attempts: {howManyAttempts}");
                    Console.Write(words.WordInYourLanguage.Trim() + ": ".PadLeft(5));
                    attempt = Console.ReadLine();

                    if (words.Word.ToUpper().Trim() != attempt.ToUpper().Trim())
                    {
                        howManyAttempts -= 1;
                        mistakes++;
                        words.Mistakes += 1;
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (howManyAttempts == 0)
                        {
                            Console.WriteLine($"\n\nWRONG ANSWER\nCorrect answer: {words.WordInYourLanguage.Trim()}\nClick Enter to continue");
                            correctAnswer = true;
                        }
                        else
                        {
                            Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                        }
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    else
                    {
                        CorrectAnswer(ref correctAnswer);
                    }

                } while (!correctAnswer);

                howManyAttempts = 3;
                correctAnswer = false;

                do
                {
                    Console.Clear();
                    Console.WriteLine($"Attempts: {howManyAttempts}");
                    Console.Write(words.Word.Trim() + ": ".PadLeft(5));
                    attempt = Console.ReadLine();

                    if (words.WordInYourLanguage.ToUpper().Trim() != attempt.ToUpper().Trim())
                    {
                        howManyAttempts -= 1;
                        mistakes++;
                        words.Mistakes += 1;
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (howManyAttempts == 0)
                        {
                            Console.WriteLine($"\n\nWRONG ANSWER\nCorrect answer: {words.WordInYourLanguage.Trim()}\nClick Enter to continue");
                            correctAnswer = true;
                        }
                        else
                        {
                            Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                        }
                        Console.ResetColor();
                        Console.ReadKey();

                    }
                    else
                    {
                        CorrectAnswer(ref correctAnswer);
                    }
                } while (!correctAnswer);
                correctAnswer = false;

            }
            Console.Clear();
            Console.WriteLine($"\nMistakes: {mistakes}\nClick Enter to continue");
            Console.Read();
        }// Learning words with the most numbers of mistakes
        private static void Levels(int min, int max, ref List<WordDescription> mainList)
        {
            string attempt;
            bool correctAnswer = false;
            int  mistakes = 0;
            Random random = new Random();
            int howMany = random.Next(min, max + 1);

            for (int i = 0; i < howMany; i++)
            {
                int howManyAttempts = 3;

                int randomWord = random.Next(0, mainList.Count);

                do
                {
                    Console.Clear();
                    Console.WriteLine($"Attempts: {howManyAttempts}");
                    Console.Write(mainList[randomWord].WordInYourLanguage.Trim() + ": ".PadLeft(5));
                    attempt = Console.ReadLine();

                    if (mainList[randomWord].Word.ToUpper().Trim() != attempt.ToUpper().Trim())
                    {

                        mainList[randomWord].Mistakes += 1;
                        howManyAttempts -= 1;
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (howManyAttempts == 0)
                        {
                            Console.WriteLine($"\n\nWRONG ANSWER\nCorrect answer: {mainList[randomWord].Word.Trim()}\nClick Enter to continue");
                            correctAnswer = true;
                        }
                        else
                        {
                            Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                        }
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    else
                    {
                        CorrectAnswer(ref correctAnswer);
                    }


                } while (!correctAnswer);

                howManyAttempts = 3;
                correctAnswer = false;

                do
                {
                    Console.Clear();
                    Console.WriteLine($"Attempts: {howManyAttempts}");
                    Console.Write(mainList[randomWord].Word.Trim() + ": ".PadLeft(5));
                    attempt = Console.ReadLine();

                    if (mainList[randomWord].WordInYourLanguage.ToUpper().Trim() != attempt.ToUpper().Trim())
                    {
                        howManyAttempts -= 1;
                        mistakes++;
                        mainList[randomWord].Mistakes += 1;
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (howManyAttempts == 0)
                        {
                            Console.WriteLine($"\n\nWRONG ANSWER\nCorrect answer: {mainList[randomWord].Word.Trim()}\nClick Enter to continue");
                            correctAnswer = true;
                        }
                        else
                        {
                            Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                        }
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    else
                    {
                        CorrectAnswer(ref correctAnswer);
                    }

                } while (!correctAnswer);
                correctAnswer = false;
            }
            Console.Clear();
            Console.WriteLine($"\nMistakes: {mistakes}\nClick Enter to continue");
            Console.Read();
        }    // Learning random words from the list
        private static void LegendaryLvl(ref List<WordDescription> mainList)
        {
            string attempt;
            int mistakes = 0;
            bool correctAnswer = false;
            foreach (var words in mainList)
            {
                int howManyAttempts = 3;

                do
                {
                    Console.Clear();
                    Console.WriteLine($"Attempts: {howManyAttempts}");
                    Console.Write(words.WordInYourLanguage.Trim() + ": ".PadLeft(5));
                    attempt = Console.ReadLine();

                    if (words.Word.ToUpper().Trim() != attempt.ToUpper().Trim())
                    {
                        howManyAttempts -= 1;
                        mistakes++;
                        words.Mistakes += 1;
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (howManyAttempts == 0)
                        {
                            Console.WriteLine($"\n\nWRONG ANSWER\nCorrect answer: {words.WordInYourLanguage.Trim()}\nClick Enter to continue");
                            correctAnswer = true;
                        }
                        else
                        {
                            Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                        }
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    else
                    {
                        CorrectAnswer(ref correctAnswer);
                    }


                } while (!correctAnswer);

                howManyAttempts = 3;
                correctAnswer = false;

                do
                {
                    Console.Clear();
                    Console.WriteLine($"Attempts: {howManyAttempts}");
                    Console.Write(words.Word.Trim() + ": ".PadLeft(5));
                    attempt = Console.ReadLine();

                    if (words.WordInYourLanguage.ToUpper().Trim() != attempt.ToUpper().Trim())
                    {
                        howManyAttempts -= 1;
                        mistakes++;
                        words.Mistakes += 1;
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (howManyAttempts == 0)
                        {
                            Console.WriteLine($"\n\nWRONG ANSWER\nCorrect answer: {words.WordInYourLanguage.Trim()}\nClick Enter to continue");
                            correctAnswer = true;
                        }
                        else
                        {
                            Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                        }
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    else
                    {
                        CorrectAnswer(ref correctAnswer);
                    }
                } while (!correctAnswer);
                correctAnswer = false;

            }
            Console.Clear();
            Console.WriteLine($"\nMistakes: {mistakes}\nClick Enter to continue");
            Console.Read();

        } // Learning all the words
        private static void HowManyWords(ref int max, ref int min, ConsoleKeyInfo k1)
        {

            switch (k1.KeyChar)
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
            }

        } // assinging a range of words
        private static void CorrectAnswer(ref bool correctAnswer)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n\nCorrect\nClick Enter to continue");
            Console.ReadKey();
            Console.ResetColor();
            correctAnswer = true;
        }
    }
}


