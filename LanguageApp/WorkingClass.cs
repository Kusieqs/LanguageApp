using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Serialization;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LanguageApp
{
    public static class WorkingClass
    {
        public static void WhichLanguage(string systemOp, ref List<Language> languages, bool firstTime, ref string[] actualData)
        {
            Array.Clear(actualData);
            string returnLanguage = null;
            string returnChar = null;
            do
            {
                Console.Clear();
                Console.WriteLine("Choose one of the language (If you want to add a new language, choose + or 0 to exit)\n"); 
                foreach (var ChoosingLangWrite in languages)
                {
                    Console.WriteLine($"{ChoosingLangWrite.CharLanguage} - {ChoosingLangWrite.Language1}");
                }

                Console.Write("\n\nLetter: ");
                ConsoleKeyInfo result = new ConsoleKeyInfo();
                result = Console.ReadKey();

                if (result.KeyChar == '+')
                {
                    WhichLanguageNewLanguage(returnChar, returnLanguage, ref languages, systemOp);

                    string json = JsonConvert.SerializeObject(languages);
                    File.WriteAllText(Path.Combine(systemOp, "Languages.json"), json);
                }
                else if(result.KeyChar == '0')
                {
                    Environment.Exit(0);
                }

                else
                {
                    foreach (var StringAndChar in languages)
                    {
                        if(result.KeyChar.ToString().ToUpper() == StringAndChar.CharLanguage.ToString())
                        {
                            actualData[0] = StringAndChar.Language1;
                            actualData[1] = StringAndChar.CharLanguage.ToString();
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
                Console.Write("\nIndex of new language (Letter): ");
                charlanguage = Console.ReadLine().ToUpper();

                if (charlanguage == "0")
                    break;
                else if (charlanguage.Length > 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nWrong index \nCLICK ENTER TO CONTINUE");
                    Console.ReadKey();
                    Console.ResetColor();
                    continue;
                }   


                foreach (var languageName in languages)
                {

                    if (languageName.Language1 == namelanguage || languageName.CharLanguage.ToString() == charlanguage.ToString())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nThis language or index are already exist\nCLICK ENTER TO CONTINUE");
                        Console.ReadKey();
                        Console.ResetColor();
                    }
                }

                if(!Directory.Exists(Path.Combine(systemOp,charlanguage)))
                {
                    Directory.CreateDirectory(Path.Combine(systemOp, charlanguage));
                }

                char.TryParse(charlanguage, out char charlanguage1);
                Language k1 = new Language(nameLanguage, charlanguage1);
                languages.Add(k1);
                end = true;
            } while (end == false);

        }// Adding new language to list
        public static void ChoosingUnit(string systemOp, string language, out string unitName, ref bool correctUnitBool)
        {
            unitName = string.Empty; 
            bool unitBool = false;
            do
            {
                List<string> foldersNames = new List<string>();
                Console.Clear();
                Console.WriteLine("If you want to create new Unit write 0 or choose one of this: \n\n\nUnits:\n\n-1.\tExit\n 0.\tNew unit\n\n");
                string[] Folders = Directory.GetDirectories(Path.Combine(systemOp, language[0].ToString()));
                foreach (var direct in Folders)
                {
                    foldersNames.Add(Path.GetFileName($"{direct}"));
                }
                for (int i = 0; i < foldersNames.Count; i++)
                {
                    Console.WriteLine($" {i + 1}.\t{foldersNames[i]}");
                    
                }
                int maxUnit = foldersNames.Count;
                Console.Write("\n\nNumber: ");
                bool unitChoosing = int.TryParse(Console.ReadLine(), out int correctUnit);
                if (unitChoosing == false)
                    continue;
                else if (correctUnit > foldersNames.Count || correctUnit < -1)
                    continue;

                if (correctUnit == -1)
                {
                    correctUnitBool = false;
                    return;
                }
                else if (correctUnit == 0)
                {
                    maxUnit++;
                    Console.Clear();

                    if(maxUnit > 999)
                    {
                        Console.WriteLine("You reach max value of units!");
                        Console.ReadKey();
                        continue;
                    }

                    Console.Write("Write a new name of Unit: ");
                    string newUnit = Console.ReadLine();
                    string firstCharLanguage = language[0].ToString();

                    if (!Directory.Exists(Path.Combine(systemOp, firstCharLanguage,newUnit)))
                    {
                        Directory.CreateDirectory(Path.Combine(systemOp, firstCharLanguage, newUnit));
                        string fileString = "";
                        File.WriteAllText(Path.Combine(systemOp, firstCharLanguage, newUnit, "W.json"), fileString);
                        File.WriteAllText(Path.Combine(systemOp, firstCharLanguage, newUnit, "E.json"), fileString);
                        File.WriteAllText(Path.Combine(systemOp, firstCharLanguage, newUnit, "D.json"), fileString);


                    }
                    else if (newUnit == "0")
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
        public static void AddingWord(Language language, string unit, string systemOp, string lanChar) 
        {
            string word, wordInYourLanguage, category;
            CategoryType categoryName;
            Console.Clear();
            bool correct = false;
            bool exit = false;
            WordWriting(out word, out wordInYourLanguage,ref exit);
            if(exit == true)
                return;
            category = Category(out categoryName,word,wordInYourLanguage);
            if (category == "-")
                return;
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
                WordDescription w1 = new WordDescription(word, wordInYourLanguage, categoryName, language, unit, 0);
                string jsonFile;

                if (!string.IsNullOrEmpty(File.ReadAllText(Path.Combine(systemOp, lanChar, unit, category + ".json"))))
                {
                    List<WordDescription> mainList = new List<WordDescription>();
                    mainList = JsonConvert.DeserializeObject<List<WordDescription>>(File.ReadAllText(Path.Combine(systemOp, lanChar, unit, category + ".json")));
                    mainList.Add(w1);
                    jsonFile = JsonConvert.SerializeObject(mainList);
                    File.WriteAllText(Path.Combine(systemOp, lanChar, unit, category + ".json"), jsonFile);
                }
                else
                {
                    List<WordDescription> copyList = new List<WordDescription>();
                    copyList.Add(w1);
                    jsonFile = JsonConvert.SerializeObject(copyList);
                    File.WriteAllText(Path.Combine(systemOp, lanChar, unit, category + ".json"), jsonFile);
                }
                File.WriteAllText(Path.Combine(systemOp, lanChar, unit, category + ".json"), jsonFile);


            } while (correct == false);
        } // Adding new word 
        public static void CheckList(string language, string systemOp, string unit)
        {
            bool correctAnswer = false;
            string choose = null;
            Console.Clear();
            Console.WriteLine("Do you want to see words,expresions,diffrent? W/E/D");
            do
            {
                correctAnswer = true;
                ConsoleKeyInfo key1 = new ConsoleKeyInfo();
                key1 = Console.ReadKey();
                switch (key1.KeyChar.ToString().ToUpper())
                {
                    case "W":
                        choose = "W";
                        break;
                    case "E":
                        choose = "E";
                        break;
                    case "D":
                        choose = "D";
                        break;
                    default:
                        correctAnswer = false;
                        break;

                }
            } while (!correctAnswer);

            bool correctWordsList = false;
            do
            {
                Console.Clear();

                List<WordDescription> mainList = new List<WordDescription>();
                string json = File.ReadAllText(Path.Combine(systemOp, language, unit, choose + ".json"));

                if(!string.IsNullOrEmpty(json))
                    mainList = JsonConvert.DeserializeObject<List<WordDescription>>(json);

                int x = 1;
                if (mainList.Count > 0)
                {
                    Console.WriteLine("Nr  \t Mistakes\t\tWord\t\t\t\t\t\tWord in your language\n");
                    foreach (var word in mainList)
                    {
                        Console.SetCursorPosition(0, x * 2);
                        Console.Write($"{x}.");
                        Console.SetCursorPosition(9, x*2);
                        Console.Write(word.Mistakes);
                        Console.SetCursorPosition(32, x * 2);
                        Console.Write(word.Word);
                        Console.SetCursorPosition(80, x * 2);
                        Console.Write(word.WordInYourLanguage);
                        ++x;
                    }
                    Console.WriteLine("\n\n\n\nIf you want to remove any line, write a number of line (write 0 to exit)");
                    Console.Write("Number: ");
                    bool correct = int.TryParse(Console.ReadLine(), out int result);

                    if (result == 0 && correct)
                    {
                        correctWordsList = true;
                    }
                    else if (correct && result > 0 && result <= mainList.Count)
                    {
                        mainList.RemoveAt(result - 1);
                        string jsonFile = JsonConvert.SerializeObject(mainList);
                        File.WriteAllText(Path.Combine(systemOp, language, unit, choose + ".json"), jsonFile);
                    }
                    else 
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nWrong number!!\nClick enter to conitnue");
                        Console.ReadKey();
                        Console.ResetColor();
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
        public static void WordWriting(out string word, out string wordInYourLanguage,ref bool exit) /// Entering vocabulary and vocabulary data in your language
        {
            do
            {
                Console.Clear();
                Console.Write($"Write '-' to exit\n\nWrite a word in another language:    ");
                word = Console.ReadLine();
                if(word == "-")
                {
                    exit = true;
                    wordInYourLanguage = null;
                    return;
                }
            } while (word.Length == 0);
            do
            {
                Console.Clear();
                Console.Write($"Write '-' to exit\n\nWrite a word in another language:    " + word);
                Console.Write($"\n\nWrite a word in your language:    ");
                wordInYourLanguage = Console.ReadLine();
                if (wordInYourLanguage == "-")
                {
                    exit = true;
                    return;
                }
            } while (wordInYourLanguage.Length == 0);


        }// Entering data of word
        public static string Category(out CategoryType categoryName,string word, string wordInYourLanguage) // Choosing word category
        {
            do
            {
                Console.Clear();
                Console.Write($"Write '-' to exit\n\nWrite a word in another language:    " + word);
                Console.Write($"\n\nWrite a word in your language:    " + wordInYourLanguage);
                Console.WriteLine("\n\nChoose a category of your word: Word,Expression,Diffrent (W/E/D)");
                ConsoleKeyInfo choose = new ConsoleKeyInfo();
                choose = Console.ReadKey();

                switch (choose.KeyChar.ToString().ToUpper())
                {
                    case "W":
                        categoryName = CategoryType.Word;
                        return "W";
                    case "E":
                        categoryName = CategoryType.Expression;
                        return "E";
                    case "D":
                        categoryName = CategoryType.Diffrent;
                        return "D";
                    case "-":
                        categoryName = CategoryType.Nothing;
                        return "-";
                    default:
                        break;
                }
            } while (true);
        }
        public static void DownWritingFile(string systemOp,string language,string unit)
        {
            List<WordDescription> words = new List<WordDescription>();
            string[] pathsUnit = new string[] { Path.Combine(systemOp, language, unit, "W") ,Path.Combine(systemOp, language, unit, "E"), Path.Combine(systemOp, language, unit, "D") };
            do
            {
                Console.Clear();
                Console.Write("Write name of file (0 to exit): ");
                string fileName = Console.ReadLine();
                if (fileName == "0")
                    return;
                else if (fileName == "")
                    continue;
                else if (File.Exists(Path.Combine(systemOp, fileName + ".json")))
                    continue;
                else
                {
                    Console.Clear();
                    Console.Write("Choose one of category W/E/D or 0 to write down all Unit: ");
                    ConsoleKeyInfo choose = new ConsoleKeyInfo();
                    choose = Console.ReadKey();
                    switch (choose.KeyChar)
                    {
                        case 'W':
                            systemOp = pathsUnit[0];
                            break;
                        case 'E':
                            systemOp = pathsUnit[1];
                            break;
                        case 'D':
                            systemOp = pathsUnit[2];
                            break;
                        case '0':
                            foreach (var paths in pathsUnit)
                            {
                                if (!string.IsNullOrEmpty(File.ReadAllText(paths + ".json")))
                                {
                                    string jsonContent = File.ReadAllText(paths + ".json");
                                    List<WordDescription> listObject = JsonConvert.DeserializeObject<List<WordDescription>>(jsonContent);
                                    words.AddRange(listObject);
                                }
                            }
                            break;
                        default:
                            continue;
                    }

                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    if(File.Exists(Path.Combine(path, fileName + ".json")))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\nFile is existing\nClick enter to continue");
                        Console.ReadKey();
                        Console.ResetColor();
                        continue;
                    }
                    else if(choose.KeyChar == '0')
                    {
                        string jsonCreator = JsonConvert.SerializeObject(words);
                        File.WriteAllText(Path.Combine(path, fileName + ".json"), jsonCreator);
                    }
                    else
                    {
                        File.WriteAllText(Path.Combine(path, fileName + ".json"), File.ReadAllText(systemOp + ".json"));
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n\nFile has been created\nClick enter to continue");
                    Console.ReadKey();
                    Console.ResetColor();
                    break;
                }
            } while(true);
        } // Writing down file
        public static void ReadingFile(string systemOp,string language,string unit)
        {

            do
            {
                string jsonContent = "";
                Console.Clear();
                Console.Write("Write path of file to add (0 to exit): ");
                string path = Console.ReadLine();
                if (path == "0")
                    return;
                else if (path == "")
                    continue;
                else if (!File.Exists(path))
                    continue;
                else
                {
                    try
                    {
                        string jsonFile = File.ReadAllText(path);
                        List<WordDescription> listObject = JsonConvert.DeserializeObject<List<WordDescription>>(jsonFile);
                        foreach (var word in listObject)
                        {
                            switch(word.Category)
                            {
                                case CategoryType.Word:
                                    jsonContent = "W.json";
                                    break;
                                case CategoryType.Expression:
                                    jsonContent = "E.json";
                                    break;
                                case CategoryType.Diffrent:
                                    jsonContent = "D.json";
                                    break;
                                default:
                                    break;
                            }

                            string jsonRead = File.ReadAllText(Path.Combine(systemOp, language, unit, jsonContent));
                            if (string.IsNullOrEmpty(jsonRead))
                            {
                                List<WordDescription> mainlistObject = new List<WordDescription>();
                                mainlistObject.Add(word);
                                string serialized = JsonConvert.SerializeObject(mainlistObject);
                                File.WriteAllText(Path.Combine(systemOp, language, unit, jsonContent), serialized);
                            }
                            else
                            {
                                string deserialized = File.ReadAllText(Path.Combine(systemOp, language, unit, jsonContent));
                                List<WordDescription> mainlistObject = JsonConvert.DeserializeObject<List<WordDescription>>(deserialized);
                                mainlistObject.Add(word);
                                string serialized = JsonConvert.SerializeObject(mainlistObject);
                                File.WriteAllText(Path.Combine(systemOp, language, unit, jsonContent), serialized);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                        return;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\nFile has been added\nClick enter to continue");
                Console.ReadKey();
                Console.ResetColor();
                break;

            } while (true); 
        }

    }
}