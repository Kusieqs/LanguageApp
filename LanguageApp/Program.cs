using System.Xml.Serialization;
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

        string systemData = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string systemOp = Path.Combine(systemData, "Desktop");

        string unitName;
        string[] actualData = new string[2];
        #endregion

        FirstTime(ref systemOp, ref firstTimeBool, ref Languages); 

        OverridingLanguages(systemOp, ref Languages);

        WorkingClass.WhichLanguage(systemOp, ref Languages, firstTimeBool, ref actualData);

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
                WorkingClass.AddingWord(actualLanguage, unitName, systemOp, actualData[1]);
            }
        }
        firstTimeBool = false;

        WorkingClass.ChoosingUnit(systemOp, actualData[0], out unitName);

        do
        {
            Console.Clear();
            Console.WriteLine($"Choose on of the options\n\n\n1.Add word\n2.Multi add word\n3.Review\n4.Check list\n5.Change language\n6.Close\n\n\nLanguage: {actualData[0]}\nUnit: {unitName}"); ///menu

            ConsoleKeyInfo result = new ConsoleKeyInfo();
            result = Console.ReadKey();


            switch (result.KeyChar)
            {
                case '1':
                    WorkingClass.AddingWord(actualLanguage, unitName, systemOp, actualData[1]);
                    break;

                case '2':
                    Console.Clear();
                    Console.WriteLine("Write number of words which do you want to add. (Max 20 words)");
                    Console.Write("\nNumber: ");
                    bool correctNumber = int.TryParse(Console.ReadLine(), out int numberOfWords);
                    if (correctNumber)
                    {
                        if (numberOfWords == 0 || numberOfWords < 0 || numberOfWords > 20)
                        {
                            break;
                        }
                        else
                        {
                            for (int i = 0; i < numberOfWords; i++)
                            {
                                WorkingClass.AddingWord(actualLanguage, unitName, systemOp, actualData[1]);
                            }
                        }
                        break;
                    }
                    else
                        continue;

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
                    WorkingClass.WhichLanguage(systemOp, ref Languages, firstTimeBool,ref actualData);
                    WorkingClass.ChoosingUnit(systemOp, actualData[0], out unitName);
                    break;

                case '6':
                    end = false;
                    break;


            }
        } while (end); 

    }
    public static void FirstTime(ref string systemOp, ref bool firstTimeBool, ref List<Language> languages)
    {

        if (!Directory.Exists(Path.Combine(systemOp, "LanguageApp"))) 
        {
            systemOp = Path.Combine(systemOp, "LanguageApp");
            Directory.CreateDirectory(systemOp);

            Language l1 = new Language(LanguageName.English, CharLanguage.E);
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

            firstTimeBool = true;
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