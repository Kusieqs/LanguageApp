﻿using System;
using System.Xml.Serialization;
using Newtonsoft.Json;
namespace LanguageApp
{
    public static class Review
    {
        public static void MainReview(string language, string systemOp, string unit)
        {
            string category = "";
            ConsoleKeyInfo key;
            int max = 0, min = 0;
            bool legendary = false, correctAnswer = false, mistakeLvl = false;

            do
            {
                correctAnswer = true;
                Console.Clear();
                Console.WriteLine("Do you want review expresion or words or diffrent? W/E/D (Write 0 to exit)");
                key = Console.ReadKey();
                category = key.KeyChar.ToString().ToUpper();
                Console.Clear();

                if (category == "W" || category == "E" || category == "D")
                {
                    Console.Write("Choose level: \n1.Easy (10 exercises)\n\n2.Medium (10-30 exercises)\n\n3.Hard (30-50 exercises)\n\n4.Legendary (All of words)\n\n5.Mistakes Level\n\n0.Exit\n\n\nNumber: ");

                    ConsoleKeyInfo k1 = new ConsoleKeyInfo();
                    k1 = Console.ReadKey();


                    switch(k1.KeyChar)
                    {
                        case '1':
                        case '2':
                        case '3':
                            HowManyWords(ref max, ref min, k1);
                            break;
                        case '4':
                            legendary = true;
                            break;
                        case '5':
                            mistakeLvl = true;
                            break;
                        case '0':
                            return;
                        default:
                            correctAnswer = false;
                            break;

                    }
                }
                else if (key.KeyChar == '0')
                    return;
                else
                    continue;

            } while (!correctAnswer);

            List<WordDescription> mainList = new List<WordDescription>();
            string json = File.ReadAllText(Path.Combine(systemOp, language, unit, category+".json"));

            if (!string.IsNullOrEmpty(json))
                mainList = JsonConvert.DeserializeObject<List<WordDescription>>(json);

            if (mainList.Count > 0)
            {

                if (legendary)
                    LegendaryLvl(ref mainList);
                else if (mistakeLvl)
                    MistakeLevel(ref mainList);
                else
                    Levels(min, max, ref mainList);

                string jsonWriter = JsonConvert.SerializeObject(mainList);
                File.WriteAllText(Path.Combine(systemOp, language, unit, category+".json"), jsonWriter);
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error\nFile with words or expresions is empty\nPress Enter to continue");
                Console.ReadKey();
                Console.ResetColor();
            }

        }// choosing which range of words and lvl
        private static void MistakeLevel(ref List<WordDescription> mainList)
        {
            int maxMistake = mainList.Max(x => x.Mistakes);
            int minMistake = 0;
            do
            {
                Console.Clear();
                Console.Write($"Number of maximum value of mistake: {maxMistake}\n\nWrite minimal number of mistakes (Write 0 to exit): ");
                minMistake = int.Parse(Console.ReadLine());
                Console.Clear();

                if (minMistake < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error\nMinimal number of mistakes can't be less than 0\nPress Enter to continue");
                    Console.ReadKey();
                    Console.ResetColor();
                }
                else if (minMistake == 0)
                    return;
                else
                    break;

            } while (true);

            string attempt;
            int mistakes = 0;
            bool correctAnswer = false;
            Random random = new Random();
            List<WordDescription> copyList = mainList.Where(x => x.Mistakes >= minMistake).ToList();

            bool goingToExit;
            bool wordInYourLanguage = false, wordInAnotherLanguage = false, mix = false, exit = false;
            QuestionAboutMode(ref wordInYourLanguage, ref wordInAnotherLanguage, ref mix, ref exit);

            if (exit)
                return;

            foreach (var words in copyList)
            {
                int whichOne = -1;
                if (mix)
                    whichOne = random.Next(0, 2);
                int howManyAttempts = 3;
                if (whichOne == 1 || wordInAnotherLanguage)
                {
                    do
                    {
                        goingToExit = false;
                        attempt = AnswerWord(ref goingToExit, words.WordInYourLanguage, howManyAttempts);

                        if (goingToExit)
                            return;

                        if (words.Word.ToUpper().Trim() != attempt.ToUpper().Trim())
                        {
                            howManyAttempts -= 1;
                            UnCorrectAnswer(howManyAttempts, ref correctAnswer, words.Word);
                            words.Mistakes += 1;
                            mistakes++;
                        }
                        else
                            CorrectAnswer(ref correctAnswer);

                    } while (!correctAnswer);
                }
                else if (whichOne == 0 || wordInYourLanguage)
                {
                    do
                    {
                        goingToExit = false;
                        attempt = AnswerWord(ref goingToExit, words.Word, howManyAttempts);
                        if (goingToExit)
                            return;

                        if (words.WordInYourLanguage.ToUpper().Trim() != attempt.ToUpper().Trim())
                        {
                            howManyAttempts -= 1;
                            UnCorrectAnswer(howManyAttempts, ref correctAnswer, words.WordInYourLanguage);
                            words.Mistakes += 1;
                            mistakes++;
                        }
                        else
                            CorrectAnswer(ref correctAnswer);
                    } while (!correctAnswer);
                    correctAnswer = false;
                }
            }
            Console.Clear();
            Console.WriteLine($"\nMistakes: {mistakes}\nClick Enter to continue");
            Console.Read();
        }// Learning words with the most numbers of mistakes
        private static void Levels(int min, int max, ref List<WordDescription> mainList)
        {
            bool wordInYourLanguage = false, wordInAnotherLanguage = false, mix = false, exit = false;
            QuestionAboutMode(ref wordInYourLanguage, ref wordInAnotherLanguage, ref mix, ref exit);

            if (exit)
                return;

            bool goingToExit;
            string attempt;
            bool correctAnswer = false;
            int  mistakes = 0;
            Random random = new Random();
            int howMany = random.Next(min, max + 1);

            for (int i = 0; i < howMany; i++)
            {
                int whichOne = -1;  
                if (mix)
                    whichOne = random.Next(0, 2);

                int howManyAttempts = 3;
                int randomWord = random.Next(0, mainList.Count);

                if (whichOne == 1 || wordInAnotherLanguage)
                {
                    do
                    {
                        goingToExit = false;
                        attempt = AnswerWord(ref goingToExit, mainList[randomWord].WordInYourLanguage, howManyAttempts);
                        if(goingToExit) 
                            return;

                        if (mainList[randomWord].Word.ToUpper().Trim() != attempt.ToUpper().Trim())
                        {

                            howManyAttempts -= 1;
                            UnCorrectAnswer(howManyAttempts, ref correctAnswer, mainList[randomWord].Word);
                            mistakes++;
                            mainList[randomWord].Mistakes += 1;
                        }
                        else
                            CorrectAnswer(ref correctAnswer);
                    } while (!correctAnswer);
                }
                else if(wordInYourLanguage || whichOne == 0)
                {
                    do
                    {
                        goingToExit = false;
                        attempt = AnswerWord(ref goingToExit, mainList[randomWord].Word, howManyAttempts);
                        if (goingToExit)
                            return;

                        if (mainList[randomWord].WordInYourLanguage.ToUpper().Trim() != attempt.ToUpper().Trim())
                        {
                            howManyAttempts -= 1;
                            UnCorrectAnswer(howManyAttempts, ref correctAnswer, mainList[randomWord].WordInYourLanguage);
                            mistakes++;
                            mainList[randomWord].Mistakes += 1;
                        }
                        else
                            CorrectAnswer(ref correctAnswer);
                    } while (!correctAnswer);

                }
                correctAnswer = false;
            }
            Console.Clear();
            Console.WriteLine($"\nMistakes: {mistakes}\nClick Enter to continue");
            Console.Read();
        }    // Learning random words from the list
        private static void LegendaryLvl(ref List<WordDescription> mainList)
        {
            bool wordInYourLanguage = false, wordInAnotherLanguage = false, mix = false, exit = false;
            QuestionAboutMode(ref wordInYourLanguage, ref wordInAnotherLanguage, ref mix, ref exit);

            if (exit)
                return;

            bool goingToExit;
            string attempt;
            int mistakes = 0;
            bool correctAnswer = false;

            foreach (var words in mainList)
            {
                int whichOne = -1;   
                Random random = new Random();
                int howManyAttempts = 3;

                if(mix)
                    whichOne = random.Next(0,2);
                correctAnswer = false;

                if (wordInAnotherLanguage || whichOne == 0)
                {
                    do
                    {
                        goingToExit = false;
                        attempt = AnswerWord(ref goingToExit, words.WordInYourLanguage, howManyAttempts);

                        if (goingToExit)
                            return;

                        if (words.Word.ToUpper().Trim() != attempt.ToUpper().Trim())
                        {
                            howManyAttempts -= 1;
                            UnCorrectAnswer(howManyAttempts, ref correctAnswer, words.Word);
                            mistakes++;
                            words.Mistakes += 1;
                        }
                        else
                            CorrectAnswer(ref correctAnswer);
                    } while (!correctAnswer);
                }
                else if(wordInYourLanguage || whichOne == 1)
                {
                    do
                    {
                        goingToExit = false;
                        attempt = AnswerWord(ref goingToExit, words.Word, howManyAttempts);

                        if (goingToExit)
                            return;

                        if (words.WordInYourLanguage.ToUpper().Trim() != attempt.ToUpper().Trim())
                        {
                            howManyAttempts -= 1;
                            UnCorrectAnswer(howManyAttempts, ref correctAnswer, words.WordInYourLanguage);
                            mistakes++;
                            words.Mistakes += 1;
                        }
                        else
                            CorrectAnswer(ref correctAnswer);
                    } while (!correctAnswer);

                }
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
        } // method to show correct answer
        private static void QuestionAboutMode(ref bool wordInYourLanguage, ref bool wordInAnotherLanguage, ref bool mix,ref bool exit)
        {
            string key = Console.ReadLine();
            do
            {
                Console.Clear();
                Console.WriteLine("Choose one of the options:\n\n\n");
                Console.Write($"1. You have to write word in your language\n\n2. You have to write word in another language\n\n3. Mix with words\n\n0. Exit\n\nNumber: ");
                key = Console.ReadLine();
                switch (key)
                {
                    case "1":
                        wordInYourLanguage = true;
                        return;
                    case "2":
                        wordInAnotherLanguage = true;
                        return;
                    case "3":
                        mix = true;
                        return;
                    case "0":
                        exit = true;
                        return;
                }
            } while (true);
        }// choosing mode of review
        private static void UnCorrectAnswer(int howManyAttempts, ref bool correctAnswer, string word)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (howManyAttempts == 0)
            {
                Console.WriteLine($"\n\nWRONG ANSWER\nCorrect answer: {word}\nClick Enter to continue");
                correctAnswer = true;
            }
            else
            {
                Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
            }
            Console.ResetColor();
            Console.ReadKey();
        }// method to show uncorrect answer
        private static string AnswerWord(ref bool exit, string word, int howManyAttempts)
        {
            Console.Clear();
            Console.WriteLine($"Attempts: {howManyAttempts}\nIf you want to exit write +\n");
            Console.Write(word + ": ".PadLeft(5));
            string attempt = Console.ReadLine();    
            if (attempt == "+")
            {
                exit = true;
                return "";
            }
            return attempt; 
        } // method to write answer

    }
}


