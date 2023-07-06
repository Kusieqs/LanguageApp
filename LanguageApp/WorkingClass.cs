using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace LanguageApp
{
    public static class WorkingClass
    {
        public static void WhichLanguage(string systemOp, ref List<Language> languages, bool firstTime, string slash, ref string[] actualData)
        {
            Array.Clear(actualData);
            string returnLanguage = null;
            string returnChar = null;
            do
            {
                Console.Clear();
                Console.WriteLine("Choose one of the language (If you want to add a new language, choose 0)\n"); 
                foreach (var ChoosingLangWrite in languages)
                {
                    Console.WriteLine($"{ChoosingLangWrite.CharLanguage} - {ChoosingLangWrite.Language1}");
                }

                ConsoleKeyInfo result = new ConsoleKeyInfo();
                result = Console.ReadKey();

                if (result.KeyChar == '0')
                {
                    WhichLanguageNewLanguage(returnChar, returnLanguage, ref languages, systemOp);

                    string json = JsonConvert.SerializeObject(languages);
                    File.WriteAllText($"{systemOp}Languages.json", json);


                }
                else
                {
                    foreach (var StringAndChar in languages)
                    {
                        char.TryParse(StringAndChar.CharLanguage, out char Letter); 
                        if (Letter == result.KeyChar)
                        {
                            actualData[0] = StringAndChar.Language1;
                            actualData[1] = StringAndChar.CharLanguage;
                        }
                    }
                }


            } while (actualData[0] == null || actualData[1] == null); 
            Console.Clear();
        }// Choosing language
        private static void WhichLanguageNewLanguage(string charlanguage, string namelanguage, ref List<Language> languages, string systemOp)
        {
            bool end = false;
            do
            {
                Console.Clear();
                Console.Write("Choose 0 to exit or write language: ");
                namelanguage = Console.ReadLine().ToLower();

                if (namelanguage == "")
                    continue;

                namelanguage = char.ToUpper(namelanguage[0]) + namelanguage.Substring(1);

                if (namelanguage == "0")
                    break;

                bool correctEnum = Enum.TryParse(namelanguage, out LanguageName nameLanguage); 
                if (correctEnum == false)
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

                bool correctindex = Enum.TryParse(charlanguage, out CharLanguage nameChar); 
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
                    }
                }

                if (!Directory.Exists($@"{systemOp}{nameChar}"))
                {
                    Directory.CreateDirectory($@"{systemOp}{nameChar}");
                }

                Language k1 = new Language(nameLanguage, nameChar);
                languages.Add(k1);
                end = true;
            } while (end == false);

        }// Adding new language to list
        public static void ChoosingUnit(string systemOp, string language, string slash, out string unitName)
        {
            unitName = string.Empty; 
            bool unitBool = false;
            do
            {
                List<string> foldersNames = new List<string>();
                Console.Clear();
                Console.WriteLine("If you want to create new Unit write number 0 or choose one of this: \n\n\nUnits:\n\n");
                string[] Folders = Directory.GetDirectories($"{systemOp}{language[0]}");
                foreach (var direct in Folders)
                {
                    foldersNames.Add(Path.GetFileName($"{direct}"));
                }
                for (int i = 0; i < foldersNames.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {foldersNames[i]}");
                }


                bool unitChoosing = int.TryParse(Console.ReadLine(), out int correctUnit);
                if (unitChoosing == false)
                    continue;
                else if (correctUnit > foldersNames.Count)
                    continue;

                if (correctUnit == 0)
                {
                    Console.Clear();
                    Console.Write("Write a new name of Unit: ");
                    string NewUnit = Console.ReadLine();
                    if (!Directory.Exists($"{systemOp}{language[0]}{slash}{NewUnit}"))
                    {
                        Directory.CreateDirectory($"{systemOp}{language[0]}{slash}{NewUnit}");
                        string test = null;
                        string jsonFile = JsonConvert.SerializeObject(test);
                        File.WriteAllText($@"{systemOp}{language[0]}{slash}{NewUnit}{slash}W.json", test);
                        File.WriteAllText($@"{systemOp}{language[0]}{slash}{NewUnit}{slash}E.json", test);
                        File.WriteAllText($@"{systemOp}{language[0]}{slash}{NewUnit}{slash}D.json", test);
                    }
                    else if (NewUnit == "0")
                    {
                        Console.WriteLine("\n\nYou can't write 0 as a name\nClick Enter to continue");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("\n\nDirectory is existing\nClick Enter to continue");
                        Console.ReadKey();
                    }
                }
                else
                {
                    unitName = foldersNames[correctUnit - 1];
                    unitBool = true;
                    Console.Clear();
                }

            } while (unitBool == false);

        }// choosing unit
        public static void AddingWord(Language language, string unit, string systemOp, string slash, string lanChar) 
        {
            string word, wordInYourLanguage, category;
            CategoryType categoryName;
            Console.Clear();
            bool correct = false;

            AddingWordMethods.WordWriting(out word, out wordInYourLanguage);
            category = AddingWordMethods.category(out categoryName);
            do
            {
                Console.Clear();


                Console.Clear();
                Console.WriteLine($"\nInformation:\n\n{word} - {wordInYourLanguage}\n\nCategory: {categoryName}\nUnit: {unit}"); ///Information About Word/Expresion
                Console.Write("\n\nDo you accept a new word? Y/N");


                ConsoleKeyInfo answer = new ConsoleKeyInfo();
                answer = Console.ReadKey();

                if (answer.KeyChar.ToString().ToUpper() == "Y")
                {
                    correct = true;
                }
                else if (answer.KeyChar.ToString().ToUpper() == "N")
                {
                    return;
                }


                Console.Clear();

                WordDescription w1 = new WordDescription(word, wordInYourLanguage, categoryName, language, unit);
                List<WordDescription> mainList = new List<WordDescription>();

                string json = File.ReadAllText(@$"{systemOp}{slash}{lanChar}{slash}{unit}{slash}{category}.json");
                if (!string.IsNullOrEmpty(json))
                {
                    mainList = JsonConvert.DeserializeObject<List<WordDescription>>(json);
                }

                mainList.Add(w1);

                string jsonFile = JsonConvert.SerializeObject(mainList);
                File.WriteAllText(@$"{systemOp}{slash}{lanChar}{slash}{unit}{slash}{category}.json", jsonFile);


            } while (correct == false);
        } // Adding new word 
        public static void CheckList(string language, string systemOp, string unit, string slash)
        {
            bool correctAnswer = false;
            string choose = null;
            Console.Clear();
            Console.WriteLine("Do you want to see words,expresions,diffrent? W/E/D");
            do
            {
                ConsoleKeyInfo key1 = new ConsoleKeyInfo();
                key1 = Console.ReadKey();
                switch (key1.KeyChar.ToString().ToUpper())
                {
                    case "W":
                        choose = "W";
                        correctAnswer = true;
                        break;
                    case "E":
                        choose = "E";
                        correctAnswer = true;
                        break;
                    case "D":
                        choose = "D";
                        correctAnswer = true;
                        break;

                }
            } while (!correctAnswer);

            bool correctWordsList = false;
            do
            {
                int count = 0;
                Console.Clear();

                List<WordDescription> mainList = new List<WordDescription>();
                string json = File.ReadAllText($@"{systemOp}{language}{slash}{unit}{slash}{choose}.json");

                if(!string.IsNullOrEmpty(json))
                    mainList = JsonConvert.DeserializeObject<List<WordDescription>>(json);

                int x = 1;
                if (mainList.Count > 0)
                {
                    Console.WriteLine("Nr  \t Mistakes\t\tword\t\t\tWord\n");
                    foreach (var word in mainList)
                    {
                        Console.SetCursorPosition(0, x * 2);
                        Console.Write($"{x}.");
                        Console.SetCursorPosition(9, x*2);
                        Console.Write(word.Mistakes);
                        Console.SetCursorPosition(32, x * 2);
                        Console.Write(word.Word);
                        Console.SetCursorPosition(56, x * 2);
                        Console.Write(word.WordInYourLanguage);
                        ++x;
                    }
                    Console.WriteLine("\n\n\n\nIf you want to remove any line, write a number of line (write 0 to exit)");
                    Console.Write("Number: ");
                    bool correct = int.TryParse(Console.ReadLine(), out int result);

                    if (result == 0)
                    {
                        correctWordsList = true;
                    }
                    else if (correct == true && result > 0)
                    {
                        mainList.RemoveAt(result - 1);
                        string jsonFile = JsonConvert.SerializeObject(mainList);
                        File.WriteAllText($@"{systemOp}{language}{slash}{unit}{slash}{choose}.json", jsonFile);
                    }
                }
                else
                {
                    Console.WriteLine("List is empty\nClick enter to continue");
                    Console.ReadKey();
                    break;
                }

            } while (!correctWordsList);
        }// List of all words
    }
}