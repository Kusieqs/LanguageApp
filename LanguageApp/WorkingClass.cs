using System;
using Microsoft.VisualBasic;

namespace LanguageApp
{
    public static class WorkingClass
    {
        public static void which_language(string SystemOP, ref List<Language> languages  ,bool firstTime, string slash, ref string[] ActualData)
        {
            string returnlan = "";
            string returnchar = "";
        BackupToMenu:
            Console.Clear();
            Console.WriteLine("Choose one of the language (If you want to add a new language, choose 0)\n"); /// zmiana na odczytywanie z pliku tektowego
            foreach (var ChoosingLangWrite in languages)
            {
                Console.WriteLine($"{ChoosingLangWrite.CharLanguage} - {ChoosingLangWrite.Language1}");
            }

            ConsoleKeyInfo result = new ConsoleKeyInfo();
            result = Console.ReadKey();
            if (result.KeyChar == '0')
            {
                which_language_NewLanguage(returnchar ,returnlan,ref languages,SystemOP);
                goto BackupToMenu;
            }
            else
            { 
                foreach (var StringAndChar in languages)
                {
                    bool correctChar = char.TryParse(StringAndChar.CharLanguage, out char Letter);
                    if(Letter == result.KeyChar)
                    {
                        ActualData[0] = StringAndChar.Language1;
                        ActualData[1] = StringAndChar.CharLanguage;
                    }
                }

            }

        }
        private static void which_language_NewLanguage(string charlanguage, string namelanguage,ref List<Language> languages,string SystemOP)
        {
            bool end = false;
            do
            {
                Console.Clear();
                Console.Write("Choose 0 to exit or write language: ");
                namelanguage = Console.ReadLine().ToLower();
                namelanguage = char.ToUpper(namelanguage[0]) + namelanguage.Substring(1);

                if (namelanguage == "0")
                    break;

                bool correctenum = Enum.TryParse(namelanguage, out LanguageName NameLanguage);
                if (correctenum == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nWrong language in our data\nCLICK ENTER TO CONTINUE");
                    Console.ReadKey();
                    Console.ResetColor();
                    continue;
                }
                Console.Write("Index of new language (Letter): ");
                charlanguage = Console.ReadLine().ToUpper();

                if (charlanguage == "0")
                    break;

                bool correctindex = Enum.TryParse(charlanguage, out CharLanguage Namechar);
                if (correctindex == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nWrong index in our data\nCLICK ENTER TO CONTINUE");
                    Console.ReadKey();
                    Console.ResetColor();
                    continue;

                }


                foreach (var languageName in languages)
                {

                    if (languageName.Language1 == namelanguage || languageName.CharLanguage == charlanguage)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nThis language or index are already exist\nCLICK ENTER TO CONTINUE");
                        Console.ReadKey();
                        Console.ResetColor();
                        continue;
                    }
                }

                if (!Directory.Exists($@"{SystemOP}{Namechar}"))
                {
                    Directory.CreateDirectory($@"{SystemOP}{Namechar}");
                }

                Language k1 = new Language(NameLanguage, Namechar);
                languages.Add(k1);
                end = true;
            } while (end == false);

        }
        public static void ChoosingUnit(string SystemOp, string language, string Slash, out string UnitName)
        {
            UnitName = string.Empty;
            bool Unit = false;
            do
            {
                List<string> FoldersNames = new List<string>();
                Console.Clear();
                Console.WriteLine("If you want to create new Unit write number 0 or choose one of this: \n\n\nUnits:\n\n");
                string[] Folders = Directory.GetDirectories($"{SystemOp}{language[0]}");
                foreach (var direct in Folders)
                {
                    FoldersNames.Add(Path.GetFileName($"{direct}"));
                }
                for (int i = 0; i < FoldersNames.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {FoldersNames[i]}");
                }


                bool UnitChoosing = int.TryParse(Console.ReadLine(), out int CorrectUnit);
                if (UnitChoosing == false)
                    continue;
                else if (CorrectUnit > FoldersNames.Count)
                    continue;

                if (CorrectUnit == 0)
                {
                    Console.Clear();
                    Console.Write("Write a new name of Unit: ");
                    string NewUnit = Console.ReadLine();
                    if (!Directory.Exists($"{SystemOp}{language[0]}{Slash}{NewUnit}"))
                    {
                        Directory.CreateDirectory($"{SystemOp}{language[0]}{Slash}{NewUnit}");
                        StreamWriter wr = new StreamWriter($@"{SystemOp}{language[0]}{Slash}{NewUnit}{Slash}W");
                        StreamWriter wr2 = new StreamWriter($@"{SystemOp}{language[0]}{Slash}{NewUnit}{Slash}E");
                        StreamWriter wr3 = new StreamWriter($@"{SystemOp}{language[0]}{Slash}{NewUnit}{Slash}D");
                        wr.Close();
                        wr2.Close();
                        wr3.Close();
                        continue;
                    }
                    else if (NewUnit == "0")
                    {
                        Console.WriteLine("\n\nYoo can't write 0 as a name\nClick Enter to continue");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("\n\nDirectory is existing\nClick Enter to continue");
                        Console.ReadKey();
                        continue;
                    }
                }
                else
                {
                    UnitName = FoldersNames[CorrectUnit - 1];
                    Unit = true;
                    Console.Clear();
                }

            } while (Unit == false);
          
        }
        public static void AddingWord(Word_Description w1, Language language,string unit, string SystemOp, string slash,string LanChar)
        {
            string word, wordInYourLanguage, category;
            CategoryName categoryName;
            Console.Clear();
            bool correct;
            do
            {
                Console.Clear();
                correct = false;

                word = AddingWordMethods.word_writing();
                wordInYourLanguage = AddingWordMethods.word_in_your_language();
                category = AddingWordMethods.category(out categoryName);


            backup:
                Console.Clear();
                Console.WriteLine($"\nInformation:\n\n{word} - {wordInYourLanguage} - {categoryName}"); ///Information About Word/Expresion
                Console.Write("\n\nDo you accept a new word? Y/N");


                ConsoleKeyInfo answer = new ConsoleKeyInfo();
                answer = Console.ReadKey();
                if (answer.KeyChar == 'Y' || answer.KeyChar == 'y')
                {
                    correct = true;
                }
                else if (answer.KeyChar == 'N' || answer.KeyChar == 'n')
                {
                    return;
                }
                else
                    goto backup;

            } while (correct == false);

            w1 = new Word_Description(word, wordInYourLanguage, categoryName, language);
            w1.TextWrite(SystemOp, unit, slash, language, category,LanChar);

        }
        public static void review(string language,string SystemOp,string unit,string slash)
        {
            int Mistakes = 0;
            ConsoleKeyInfo key;
            int Max = 0, Min = 0, count = -1;
            bool Legendary = false;
            bool correctAnswer = false;
            do
            {

                Console.Clear();
                Console.WriteLine("Do you want review expresion or words or diffrent? W/E/D");
                key = Console.ReadKey();
                Console.Clear();

                if (key.KeyChar == 'W' || key.KeyChar == 'E' || key.KeyChar =='D')
                {
                    Console.WriteLine("Choose level: \n1.Easy (10 exercises)\n\n2.Medium (10-30 exercises)\n\n3.Hard (30-50 exercises)\n\n4.Legendary (All of words)\n");
                    ConsoleKeyInfo k1 = new ConsoleKeyInfo();
                    k1 = Console.ReadKey();
                    if (k1.KeyChar == '1')
                    {
                        Min = 10;
                        Max = 10;
                        correctAnswer = true;
                    }
                    else if (k1.KeyChar == '2')
                    {
                        Min = 10;
                        Max = 30;
                        correctAnswer = true;
                    }
                    else if (k1.KeyChar == '3')
                    {
                        Min = 30;
                        Max = 50;
                        correctAnswer = true;
                    }
                    else if (k1.KeyChar == '4')
                    {
                        Legendary = true;
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


            SystemOp += SystemOp + language + slash;

            // Do zmiany

            string word = File.ReadAllText($@"{SystemOp}{unit}{slash}{key.KeyChar}");
            string expresion = File.ReadAllText($@"{SystemOp}{unit}{slash}{key.KeyChar}");

            if ((!string.IsNullOrWhiteSpace(word) && key.KeyChar == 'W') || (!string.IsNullOrWhiteSpace(expresion) && key.KeyChar == 'E'))
            {
                StreamReader rd = new StreamReader($@"{SystemOp}{unit}{slash}{key.KeyChar}");
                string teskt = "";
                do
                {
                    teskt = rd.ReadLine();
                    count++;
                } while (teskt != null);
                rd.Close();



                if (Legendary == true)
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
                else
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
