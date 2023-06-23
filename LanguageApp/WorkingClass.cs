using System;
namespace LanguageApp
{
    public static class WorkingClass
    {
        public static string which_language(string SystemOP, ref List<Language> languages  ,bool firstTime, string slash)
        {
            StreamReader rs = null;
            string returnlan = "";
            string returnchar = "";
            if (firstTime == true)
                rs = new StreamReader($@"{SystemOP.Remove(SystemOP.Length - 1)}{slash}IdLanguage");
            else
                rs = new StreamReader($@"{SystemOP}IdLanguage");



            ConsoleKeyInfo result = new ConsoleKeyInfo();
        BackupToMenu:
            Console.Clear();
            Console.WriteLine("Choose one of the language (If you want to add a new language, choose 0)\n"); /// zmiana na odczytywanie z pliku tektowego
            for (int i = 0; i < key.Count; i++)
            {
                Console.WriteLine(key[i] + " - " + languageData[i]);
            }
            rs.Close();
            result = Console.ReadKey();
            if (result.KeyChar == '0')
            {
            Backup:
                Console.Clear();
                Console.Write("Choose 0 to exit or write language: ");
                returnlan = Console.ReadLine().ToLower();

                if (returnlan == "0")
                    return "";

                bool correctenum = Enum.TryParse(returnlan, out NameLanguage name);
                if (correctenum == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nWrong language in our data\nCLICK ENTER TO CONTINUE");
                    Console.ResetColor();
                    goto Backup;
                }
                Console.Write
                    ("Index of new language (Letter): ");
                returnchar = Console.ReadLine().ToUpper();

                if (returnchar == "0")
                    return "";

                bool correctindex = Enum.TryParse(returnchar, out LanguageChar Namechar);
                if (correctindex == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nWrong index in our data\nCLICK ENTER TO CONTINUE");
                    Console.ResetColor();
                    goto Backup;

                }


                foreach (var item in languageData)
                {
                    if (item == returnlan)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nThis language is already exist\nCLICK ENTER TO CONTINUE");
                        Console.ReadKey();
                        Console.ResetColor();
                        goto Backup;
                    }
                }
                foreach (var item in key)
                {
                    if (item == returnchar)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nThis index is already exist\nCLICK ENTER TO CONTINUE");
                        Console.ReadKey();
                        Console.ResetColor();
                        goto Backup;
                    }
                }

                if (!Directory.Exists($@"{SystemOP}{returnchar}"))
                {
                    Directory.CreateDirectory($@"{SystemOP}{returnchar}");
                }
                StreamWriter wr = File.AppendText($@"{SystemOP}IdLanguage");
                wr.Write("/" + returnchar + " - " + returnlan);
                wr.Close();
                return returnchar + " " + returnlan;
            }
            else
            {
                int count = 0;
                foreach (var item in key)
                {
                    bool correctChar = char.TryParse(item, out char Letter);
                    if (Letter == result.KeyChar)
                    {
                        string copyLan = languageData[count];
                        return Letter + " " + copyLan;
                    }
                    count++;
                }
                goto BackupToMenu;
            }

        }
    }
}
