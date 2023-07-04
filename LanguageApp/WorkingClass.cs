using System;
using System.Xml.Serialization;
using Microsoft.VisualBasic;

namespace LanguageApp
{
    public static class WorkingClass
    {
        public static void which_language(string SystemOP, ref List<Language> languages, bool firstTime, string slash, ref string[] ActualData) // za duzo zmiennych i zle nazewnictwo funkcji i niektorych zmiennych
        {
            Array.Clear(ActualData);
            string returnlan = "";// nie uzywaj skrotow, lepiej bedzie returnLanguage.
            string returnchar = "";
            do
            {
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
                    which_language_NewLanguage(returnchar, returnlan, ref languages, SystemOP);
                    XmlSerializer xml = new XmlSerializer(typeof(List<Language>));
                    StreamWriter sr = new StreamWriter($"{SystemOP}Languages.xml");
                    xml.Serialize(sr, languages);
                }
                else
                {
                    foreach (var StringAndChar in languages)
                    {
                        bool correctChar = char.TryParse(StringAndChar.CharLanguage, out char Letter); //Nigdzie nie uzywasz correctChar, więc nie musisz tryparse przypisywac do zmiennej.
                        if (Letter == result.KeyChar)
                        {
                            ActualData[0] = StringAndChar.Language1;
                            ActualData[1] = StringAndChar.CharLanguage;
                        }
                    }
                }


            } while (string.IsNullOrEmpty(ActualData[1]) || string.IsNullOrEmpty(ActualData[0])); //mozesz sprawdzac cala tablice czy jest pusta zamiast poszczegolne elementy
            Console.Clear();
        }
        private static void which_language_NewLanguage(string charlanguage, string namelanguage, ref List<Language> languages, string SystemOP)//zle nazewnictwo funkcji i niektorych zmiennych
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

                bool correctenum = Enum.TryParse(namelanguage, out LanguageName NameLanguage); //correctEnum
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

                bool correctindex = Enum.TryParse(charlanguage, out CharLanguage Namechar); //zla nazwa zmiennej
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
                        continue;// to continue nic nie robi bo iteracja petli i tak sie tu konczy.
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
        public static void ChoosingUnit(string SystemOp, string language, string Slash, out string UnitName,List<Word_Description> MainList) //za duzo zmiennych i zle nazwy
        {
            UnitName = string.Empty; // z malej ma byc
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
                        XmlSerializer xml = new XmlSerializer(typeof(List<Word_Description>));
                        Directory.CreateDirectory($"{SystemOp}{language[0]}{Slash}{NewUnit}");
                        StreamWriter wr = new StreamWriter($@"{SystemOp}{language[0]}{Slash}{NewUnit}{Slash}W");
                        StreamWriter wr2 = new StreamWriter($@"{SystemOp}{language[0]}{Slash}{NewUnit}{Slash}E");
                        StreamWriter wr3 = new StreamWriter($@"{SystemOp}{language[0]}{Slash}{NewUnit}{Slash}D");

                        xml.Serialize(wr, MainList);
                        xml.Serialize(wr2, MainList);
                        xml.Serialize(wr3, MainList);
                        wr.Close();
                        wr2.Close();
                        wr3.Close();
                        continue; //to continue nic nie robi bo jest koniec warunku.
                    }
                    else if (NewUnit == "0")
                    {
                        Console.WriteLine("\n\nYoo can't write 0 as a name\nClick Enter to continue");
                        Console.ReadKey();
                        continue; //to tez
                    }
                    else
                    {
                        Console.WriteLine("\n\nDirectory is existing\nClick Enter to continue");
                        Console.ReadKey();
                        continue; //i to tez
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
        public static void AddingWord(Word_Description w1, Language language, string unit, string SystemOp, string slash, string LanChar) //za duzo zmiennych i zle nazwy.
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
                Console.WriteLine($"\nInformation:\n\n{word} - {wordInYourLanguage}\n\nCategory: {categoryName}\nUnit: {unit}"); ///Information About Word/Expresion
                Console.Write("\n\nDo you accept a new word? Y/N");


                ConsoleKeyInfo answer = new ConsoleKeyInfo();
                answer = Console.ReadKey();
                if (answer.KeyChar == 'Y' || answer.KeyChar == 'y') //ToLower i porownujesz tylko z mala albo na odwrot. nie musisz wtedy obu przypadkow sprawdzac
                {
                    correct = true;
                }
                else if (answer.KeyChar == 'N' || answer.KeyChar == 'n')
                {
                    return;
                }
                else
                    goto backup; //Nie uzywaj goto

            } while (correct == false);
            Console.Clear();

            List<Word_Description> MainList = new List<Word_Description>();
            XmlSerializer xml = new XmlSerializer(typeof(List<Word_Description>));
            StreamReader sr = new StreamReader($@"{SystemOp}{slash}{LanChar}{slash}{unit}{slash}{category}.xml");
            StreamWriter sw = new StreamWriter($@"{SystemOp}{slash}{LanChar}{slash}{unit}{slash}{category}.xml"); //ulatwisz sobie znacznie zycie jak przerzucisz sie na jsona.

            w1 = new Word_Description(word, wordInYourLanguage, categoryName, language, unit);
            try
            {
                MainList = xml.Deserialize(sr) as List<Word_Description>;
                MainList.Add(w1);
                xml.Serialize(sw, MainList); //// PROBLEM!
            }
            catch // przechwytujesz blad ale nic z nim nie robisz co moze prowadzic do ukrywania bledow i nie bedziesz wiedzial czemu Ci cos nie dziala.
            { }
            finally
            {
                MainList.Add(w1);
                xml.Serialize(sw, MainList);
            }

        }
        public static void CheckList(string language, string SystemOp, string unit, string slash)// zle nazwy zmiennych
        {
            Console.Clear();
            Console.WriteLine("Do you want to see words,expresions,diffrent? W/E/D");
        Backup:
            string choose;
            ConsoleKeyInfo key1 = new ConsoleKeyInfo();
            key1 = Console.ReadKey();
            if (key1.KeyChar == 'W' || key1.KeyChar == 'w') //proponowalbym switcha
            {
                choose = "W";
            }
            else if (key1.KeyChar == 'E' || key1.KeyChar == 'e')
            {
                choose = "E";
            }
            else if(key1.KeyChar=='d'|| key1.KeyChar =='D')
            {
                choose = "D";
            }
            else
                goto Backup;//goto


        Backup1: //goto
            int count = 0;
            Console.Clear();
            StreamReader rd = new StreamReader($@"{SystemOp}{language}{slash}{unit}{slash}{choose}.xml");
            XmlSerializer xml = new XmlSerializer(typeof(List<Word_Description>));
            List<Word_Description> MainList = xml.Deserialize(rd) as List<Word_Description>;
            int x = 1;
            foreach (var Words in MainList) // nie sprawdzasz czy mainList nie jest pusta przed iteracja
            {
                Console.WriteLine($"{x}. {Words.Word} - - - - - {Words.WordInYourLanguage} - - - - - Mistakes: {Words.Mistakes}"); //PadLeft/Right
                x++;
            }


            rd.Close();
            Console.WriteLine("\n\nIf you want to remove any line, write a number of line (write 0 to exit)");
            Console.Write("Number: ");

            bool correct = int.TryParse(Console.ReadLine(), out int result);
            if (result == 0 && correct == true) // if(result ==0 && correct)
            {
                return;// to tez nie ma sensu. Odwroc warunek jak juz i w nim cos rob.
            }
            else if (correct == true)
            {
                MainList.RemoveAt(result-1);
                StreamWriter sr = new StreamWriter($@"{SystemOp}{language}{slash}{unit}{slash}{choose}.xml");
                xml.Serialize(sr, MainList);
                goto Backup1; //goto

            }
            else
                goto Backup1; //goto


        }
    }
}