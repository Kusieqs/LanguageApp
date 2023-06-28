using System;
namespace LanguageApp
{
    public static class WorkingClass
    {
        public static void which_language(string SystemOP, ref List<Language> languages  ,bool firstTime, string slash, ref string[] ActualLanguage)
        {
            StreamReader rs = null;
            string returnlan = "";
            string returnchar = "";
            if (firstTime == true)
                rs = new StreamReader($@"{SystemOP.Remove(SystemOP.Length - 1)}{slash}IdLanguage");
            else
                rs = new StreamReader($@"{SystemOP}IdLanguage");

        BackupToMenu:
            Console.Clear();
            Console.WriteLine("Choose one of the language (If you want to add a new language, choose 0)\n"); /// zmiana na odczytywanie z pliku tektowego
            foreach (var ChoosingLangWrite in languages)
            {
                Console.WriteLine($"{ChoosingLangWrite.charLanguage} - {ChoosingLangWrite.language}");
            }
            rs.Close();

            ConsoleKeyInfo result = new ConsoleKeyInfo();
            result = Console.ReadKey();
            if (result.KeyChar == '0')
            {
                which_language_NewLanguage(returnchar ,returnlan,ref languages,SystemOP);
                goto BackupToMenu;
            }
            else
            {
                bool ArrayIsCorrect = false;
                string CopyofResult = result.ToString().ToUpper();
                foreach (var CheckingCorrectAnswer in languages)
                {
                    if (CheckingCorrectAnswer.charLanguage == CopyofResult)
                    {
                        ActualLanguage[1] = CheckingCorrectAnswer.charLanguage;
                        ActualLanguage[0] = CheckingCorrectAnswer.language;
                        ArrayIsCorrect = true;
                    }
                }
                if (ArrayIsCorrect = false)
                    goto BackupToMenu;
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

                    if (languageName.language == NameLanguage || languageName.charLanguage == Namechar)
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

    }
}
