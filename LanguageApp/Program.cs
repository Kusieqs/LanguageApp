﻿using System.Xml.Serialization;
using Newtonsoft.Json;
namespace LanguageApp;
internal class Program
{

    private static void Main(string[] args)
    {
        List<WordDescription> ListOfWords = new List<WordDescription>();
        List<Language> Languages = new List<Language>();

        #region Data Reading
        bool firstTimeBool = false, end = true;
        string userName = System.Environment.UserName;
        bool windowsOrMac = System.Environment.OSVersion.Platform == PlatformID.Win32NT;
        string systemOp, slash, unitName;
        string[] actualData = new string[2];

        if (windowsOrMac) 
        {
            systemOp = $@"C:\Users\{userName}\Desktop\"; ///Windows
            slash = @"\";
        }
        else
        {
            systemOp = $@"/Users/{userName}/Desktop/"; ///MacIOS
            slash = @"/";
        }
        #endregion

        FirstTime(ref systemOp, slash, ref firstTimeBool, ref Languages, ListOfWords); 

        OverridingLanguages(systemOp, ref Languages);

        WorkingClass.WhichLanguage(systemOp, ref Languages, firstTimeBool, slash, ref actualData);

        Language actualLanguage = new Language(actualData[0], actualData[1]);

        if (firstTimeBool)
        {
            Console.Clear();
            Console.WriteLine("Hi! I would like you to enter the first 5 words to learn in the future!");
            Thread.Sleep(1600);
            unitName = "Unit1";
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Words:\n\n");
                WorkingClass.AddingWord(actualLanguage, unitName, systemOp, slash, actualData[1]);
            }
        }
        firstTimeBool = false;

        WorkingClass.ChoosingUnit(systemOp, actualData[0], slash, out unitName);

        do
        {
            Console.Clear();
            Console.WriteLine($"Choose on of the options\n\n\n1.Add word\n2.Multi add word\n3.Review\n4.Check list\n5.Change language\n6.Close\n\n\nLanguage: {actualData[0]}\nUnit: {unitName}"); ///menu

            ConsoleKeyInfo result = new ConsoleKeyInfo();
            result = Console.ReadKey();


            switch (result.KeyChar)
            {
                case '1':
                    WorkingClass.AddingWord(actualLanguage, unitName, systemOp, slash, actualData[1]);
                    break;

                case '2':
                    Console.Clear();
                    Console.WriteLine("Write number of words which do you want to add. (Max 20 words)");
                    Console.Write("\nNumber: ");
                    int numberOfWords = int.Parse(Console.ReadLine());
                    if (numberOfWords == 0 || numberOfWords < 0 || numberOfWords > 20)
                    {
                        break;
                    }
                    else
                    {
                        for (int i = 0; i < numberOfWords; i++)
                        {
                            WorkingClass.AddingWord(actualLanguage, unitName, systemOp, slash, actualData[1]);
                        }
                    }
                    break;

                case '3':
                    Review.MainReview(actualData[1], systemOp, unitName, slash);
                    break;

                case '4':
                    WorkingClass.CheckList(actualData[1], systemOp, unitName, slash);
                    break;

                case '5':
                    Console.Clear();
                    Languages.Clear();
                    OverridingLanguages(systemOp, ref Languages);
                    WorkingClass.WhichLanguage(systemOp, ref Languages, firstTimeBool, slash, ref actualData);
                    WorkingClass.ChoosingUnit(systemOp, actualData[0], slash, out unitName);
                    break;

                case '6':
                    end = false;
                    break;


            }
        } while (end); 

    }
    public static void FirstTime(ref string systemOp, string slash, ref bool firstTimeBool, ref List<Language> languages, List<WordDescription> xmlList)
    {
        if (!Directory.Exists($"{systemOp}LanguageApp"))
        {


            Directory.CreateDirectory($"{systemOp}LanguageApp");
            systemOp += @$"LanguageApp{slash}";

            Language l1 = new Language(LanguageName.English, CharLanguage.E);
            languages.Add(l1);

            string jsonLanguages = JsonConvert.SerializeObject(languages);
            File.WriteAllText($@"{systemOp}Languages.json", jsonLanguages);


            Directory.CreateDirectory($"{systemOp}E");
            Directory.CreateDirectory($@"{systemOp}E{slash}Unit1");

            string test = null;
            string jsonFile = JsonConvert.SerializeObject(test);
            File.WriteAllText($@"{systemOp}E{slash}Unit1{slash}W.json", test);
            File.WriteAllText($@"{systemOp}E{slash}Unit1{slash}E.json", test);
            File.WriteAllText($@"{systemOp}E{slash}Unit1{slash}D.json", test);


            firstTimeBool = true;
        }
        else
        {
            systemOp += $@"LanguageApp{slash}";
        }
    } // Creator directories and files (First time using app)
    public static void OverridingLanguages(string systemOp, ref List<Language> languages)
    {
        Console.Clear();
        languages.Clear();
        string json = File.ReadAllText($@"{systemOp}Languages.json");
        languages = JsonConvert.DeserializeObject<List<Language>>(json);
    } // Adding main language
}