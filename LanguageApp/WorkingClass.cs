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

    }
}
