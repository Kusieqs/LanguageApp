using System;
using System.Xml.Serialization;
namespace LanguageApp
{
	public static class Review
	{
		public static void MainReveiw(string language, string SystemOp, string unit, string slash) //popraw nazwe drugiego parametru funkcji i literowke w nazwie.
		{
            
            int Mistakes = 0; // zmienne z malej
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
                        continue; // to continue nic nie robi. Jezeli warunek nie zostanie spelniony to i tak zostanie to pominiete.
                    }
                }
                else
                    continue; //tu tak samo.
            } while (correctAnswer == false); // mozna tez tak if (!correctAnswer)


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
        private static void Levels(string SystemOp,string slash, ConsoleKeyInfo key, string unit, int Min, int Max, ref int Mistakes, int count, List<Word_Description> MainList) //trochę za dużo, im mniej funkcja przyjmuje parametrow tym latwiej ja obsluzyc bez bledow. Np. nie musisz console key podawac do funkcji tylko mozesz przyjmowac klawisz w jej srodku.
        {
            Random random = new Random();
            int HowMany = random.Next(Min, Max + 1);

            if (count > HowMany)
            {
                for (int i = 0; i < HowMany; i++)
                {
                    int RandomWord = random.Next(0, MainList.Count);
                Backup1: //nie uzywaj goto.
                    Console.Clear();
                    Console.Write(MainList[RandomWord].WordInYourLanguage + " - - - - - ");//zamiast hardcodowania odstepow jest taka funkcja jak PadLeft/right. Ew w interpolacji mozna ustawiać odstępy.
                    string Attempt = Console.ReadLine();

                    if (MainList[RandomWord].Word.ToUpper() != Attempt.ToUpper()) //bezpieczniej by tu bylo uzywac linq albo slownikow z funkcja TryGetValue(), albo jak juz chcesz tak to najpierw sprawdzaj czy lista nie jest pusta i czy ma tyle indexow.
                    {
                        Mistakes++;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                        Console.ResetColor();
                        Console.Read();
                        goto Backup1;
                    }

                    Console.WriteLine("Correct\nClick Enter to continue");
                Backup2:// nie uzywaj goto
                    Console.Clear();
                    Console.Write(MainList[RandomWord].Word + " - - - - - "); //PadLeft/Righ 
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
                Backup1: //goto
                    Console.Clear();
                    Console.Write(MainList[RandomWord].WordInYourLanguage + " - - - - - ");//PadLeft/Right
                    string Attempt = Console.ReadLine();

                    if (MainList[RandomWord].Word.ToUpper() != Attempt.ToUpper())
                    {
                        Mistakes++;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                        Console.ResetColor();
                        Console.Read();
                        goto Backup1;// goto
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
        private static void LegendaryLvl(string SystemOp, string slash, ConsoleKeyInfo key, string unit, ref int Mistakes, int count, List<Word_Description> MainList) //za duzo parametrow. Zazwyczaj jesli funkcja przyjmuje ich wiecej niz 3 to znak ze trzeba sie zastanowic nad przerobieniem funkcji.
        {
            foreach (var Words in MainList)
            {
            Backup1: //goto
                Console.Clear();
                Console.Write(Words.WordInYourLanguage + " - - - - - ");//pad
                string Attempt = Console.ReadLine();

                if (Words.Word.ToUpper() != Attempt.ToUpper())
                {
                    Mistakes++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                    Console.ResetColor();
                    Console.Read();
                    goto Backup1;//goto
                }
                Console.WriteLine("Correct\nClick Enter to continue");
            Backup2:
                Console.Clear();
                Console.Write(Words.Word + " - - - - - ");//pad
                Attempt = Console.ReadLine();

                if (Words.WordInYourLanguage.ToUpper() != Attempt.ToUpper())
                {
                    Mistakes++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\nWRONG ANSWER\nClick Enter to continue");
                    Console.ResetColor();
                    Console.Read();
                    goto Backup2; //goto
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
            if(k1.KeyChar == '1') //switch bedzie lepsza opcja
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

