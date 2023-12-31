﻿using System.Xml.Serialization;
using Newtonsoft.Json;
namespace LanguageApp;
internal class Program
{

    private static void Main(string[] args)
    {
        #region Data Reading
        List<Language> Languages = new List<Language>();
        bool end = true;
        string systemData = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string systemOp = Path.Combine(systemData, "Desktop");
        string unitName;
        string[] actualData = new string[2];
        bool exit = false;
        #endregion
        Language actualLanguage = new Language();
        FirstTime(ref systemOp,ref Languages); 
        OverridingLanguages(systemOp, ref Languages);
        do
        {
            bool correctUnit = true;
            WorkingClass.WhichLanguage(systemOp, ref Languages, ref actualData);
            char.TryParse(actualData[1], out char charLanguage);
            Enum.TryParse(actualData[0], out LanguageName languageName);
            actualLanguage = new Language(languageName, charLanguage);
            WorkingClass.ChoosingUnit(systemOp, actualData[0], out unitName, ref correctUnit);
            if (!correctUnit)
                continue;
            break;

        } while (true);


        do
        {
            Console.Clear();
            Console.WriteLine($"Choose one of the options\n\n\n1.Add word\n2.Multi add word\n3.Review\n4.Check list\n5.Change language\n6.Down writing file to json\n7.Read json file\n8.Close\n\n\nLanguage: {actualData[0]}\nUnit: {unitName}"); ///menu

            ConsoleKeyInfo result = new ConsoleKeyInfo();
            result = Console.ReadKey();


            switch (result.KeyChar)
            {
                case '1':
                    WorkingClass.AddingWord(actualLanguage, unitName, systemOp, actualData[1],ref exit);
                    exit = false;
                    break;

                case '2':
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Write number of words which do you want to add or 0 to exit. (Max 20 words)");
                        Console.Write("\nNumber: ");
                        bool correctNumber = int.TryParse(Console.ReadLine(), out int numberOfWords);
                        if (correctNumber)
                        {
                            if (numberOfWords < 0 || numberOfWords > 20)
                                continue;
                            else if (numberOfWords == 0)
                                break;
                            else
                            {
                                for (int i = 0; i < numberOfWords; i++)
                                {
                                    WorkingClass.AddingWord(actualLanguage, unitName, systemOp, actualData[1],ref exit);
                                    if (exit)
                                    {
                                        exit = false;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                        else
                            continue;
                    } while (true);
                    break;
                case '3':
                    Review.MainReview(actualData[1], systemOp, unitName);
                    break;

                case '4':
                    WorkingClass.CheckList(actualData[1], systemOp, unitName);
                    break;

                case '5':
                    Console.Clear();
                    Languages.Clear();
                    OverridingLanguages(systemOp, ref Languages);
                    do
                    {
                        bool correctUnit = true;
                        WorkingClass.WhichLanguage(systemOp, ref Languages, ref actualData);
                        WorkingClass.ChoosingUnit(systemOp, actualData[0], out unitName,ref correctUnit);
                        if (!correctUnit)
                            continue;
                        break;
                    } while(true);
                    break;

                case '6':
                    WorkingClass.DownWritingFile(systemOp, actualData[1],unitName);
                    break;
                case '7':
                    WorkingClass.ReadingFile(systemOp, actualData[1], unitName);
                    break;
                case '8':
                    end = false;
                    break;


            }
        } while (end); 

    }
    public static void FirstTime(ref string systemOp, ref List<Language> languages)
    {

        if (!Directory.Exists(Path.Combine(systemOp, "LanguageApp"))) 
        {
            systemOp = Path.Combine(systemOp, "LanguageApp");
            Directory.CreateDirectory(systemOp);

            Language l1 = new Language(LanguageName.English, 'E');
            languages.Add(l1);

            string jsonLanguages = JsonConvert.SerializeObject(languages);
            File.WriteAllText(Path.Combine(systemOp, "Languages.json"), jsonLanguages);

            Directory.CreateDirectory(Path.Combine(systemOp, "E"));
            Directory.CreateDirectory(Path.Combine(systemOp, "E", "Unit1"));

            string test = null;
            string jsonFile = JsonConvert.SerializeObject(test);

            File.WriteAllText(Path.Combine(systemOp, "E", "Unit1", "W.json"), test);
            File.WriteAllText(Path.Combine(systemOp, "E", "Unit1", "E.json"), test);
            File.WriteAllText(Path.Combine(systemOp, "E", "Unit1", "D.json"), test);

        }
        else
        {
            systemOp = Path.Combine(systemOp, "LanguageApp");
        }
    } // Creator directories and files (First time using app)
    public static void OverridingLanguages(string systemOp, ref List<Language> languages)
    {
        Console.Clear();
        languages.Clear();
        string json = File.ReadAllText(Path.Combine(systemOp, "Languages.json"));
        languages = JsonConvert.DeserializeObject<List<Language>>(json);
    } // Adding main language
}