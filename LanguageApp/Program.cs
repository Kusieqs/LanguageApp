using System.Xml.Serialization;
namespace LanguageApp;
internal class Program
{

    private static void Main(string[] args)
    {
        List<Word_Description> ListOfWords = new List<Word_Description>();
        List<Language> Languages = new List<Language>();

        #region Data Reading
        bool FirstTimeBool = false, end = true;
        string UserName = System.Environment.UserName;
        bool WindowsOrMac = System.Environment.OSVersion.Platform == PlatformID.Win32NT;
        string SystemOp, Slash;
        string UnitName;
        string[] ActualData = new string[2];
        if (WindowsOrMac == true)
        {
            SystemOp = $@"C:\Users\{UserName}\Desktop\"; ///Windows
            Slash = @"\";
        }
        else
        {
            SystemOp = $@"/Users/{UserName}/Desktop/"; ///MacIOS
            Slash = @"/";
        }
        #endregion

        FirstTime(ref SystemOp, Slash, ref FirstTimeBool,ref Languages, ListOfWords);
        OverridingLanguages(SystemOp, ref Languages);

        WorkingClass.which_language(SystemOp, ref Languages, FirstTimeBool, Slash, ref ActualData);

        Language ActualLAN = new Language(ActualData[0], ActualData[1]);
        Word_Description Words = null;

        if(FirstTimeBool == true)
        {
            Console.Clear();
            Console.WriteLine("Hi! I would like you to enter the first 5 words to learn in the future!");
            Thread.Sleep(1600);
            UnitName = "Unit1";
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Words:\n\n");
                WorkingClass.AddingWord(Words, ActualLAN, UnitName, SystemOp, Slash, ActualData[1]);
            }
        }  
        FirstTimeBool = false;
        WorkingClass.ChoosingUnit(SystemOp, ActualData[0], Slash,out UnitName, ListOfWords);

        do
        {
        menu:
            Console.Clear();
            Console.WriteLine($"Choose on of the options\n\n\n1.Add word\n2.Multi add word\n3.Review\n4.Check list\n5.Change language\n6.Close\n\n\nLanguage: {ActualData[0]}\nUnit: {UnitName}"); ///menu

            ConsoleKeyInfo result = new ConsoleKeyInfo();
            result = Console.ReadKey();

            if (result.KeyChar == '1')
            {
                WorkingClass.AddingWord(Words, ActualLAN, UnitName, SystemOp, Slash, ActualData[1]);
            }
            else if(result.KeyChar == '2')
            {
                Console.Clear();
                Console.WriteLine("Write number of words which do you want to add. (Max 20 words)");
                Console.Write("\nNumber: ");
                int numberOfWords = int.Parse(Console.ReadLine());
                if(numberOfWords == 0 || numberOfWords < 0 || numberOfWords >20)
                {
                    continue;
                }
                else 
                {
                    for (int i = 0; i < numberOfWords; i++)
                    {
                        WorkingClass.AddingWord(Words, ActualLAN, UnitName, SystemOp, Slash, ActualData[1]);
                    }
                }    
                
            }
            else if (result.KeyChar == '3')
            {
                Review.MainReveiw(ActualData[1], SystemOp, UnitName, Slash);
            }
            else if (result.KeyChar == '4')
            {
                WorkingClass.CheckList(ActualData[1], SystemOp, UnitName, Slash);
            }
            else if (result.KeyChar == '5')
            {
                Console.Clear();
                Languages.Clear();
                OverridingLanguages(SystemOp, ref Languages);
                WorkingClass.which_language(SystemOp,ref Languages, FirstTimeBool,Slash,ref ActualData);
                WorkingClass.ChoosingUnit(SystemOp,ActualData[0], Slash, out UnitName, ListOfWords);

            }
            else if (result.KeyChar == '6')
                end = false;
            else
                goto menu;

        } while (end != false);

    }
    public static void FirstTime(ref string SystemOp, string Slash,ref bool FirstTimeBool,ref List<Language> languages, List<Word_Description> xmlList)
    {
        if (!Directory.Exists($"{SystemOp}LanguageApp"))
        {
            XmlSerializer xmlMainWord = new XmlSerializer(typeof(List<Word_Description>));
            XmlSerializer xml = new XmlSerializer(typeof(List<Language>));

            Directory.CreateDirectory($"{SystemOp}LanguageApp");
            SystemOp += @$"LanguageApp{Slash}";

            Language l1 = new Language(LanguageName.English,CharLanguage.E);
            languages.Add(l1);
            StreamWriter strw = new StreamWriter(@$"{SystemOp}Languages.xml");
            xml.Serialize(strw, languages);
            strw.Dispose();


            Directory.CreateDirectory($"{SystemOp}E");
            Directory.CreateDirectory($@"{SystemOp}E{Slash}Unit1");
            StreamWriter Write = new StreamWriter($@"{SystemOp}E{Slash}Unit1{Slash}W.xml");
            StreamWriter Write1 = new StreamWriter($@"{SystemOp}E{Slash}Unit1{Slash}E.xml");
            StreamWriter Write2 = new StreamWriter($@"{SystemOp}E{Slash}Unit1{Slash}D.xml");

            xmlMainWord.Serialize(Write, xmlList);
            xmlMainWord.Serialize(Write1, xmlList);
            xmlMainWord.Serialize(Write2, xmlList);

            Write.Close();
            Write1.Close();
            Write2.Close();

            FirstTimeBool = true;
        }
        else
        {
            SystemOp += $@"LanguageApp{Slash}";
        }
    }
    public static void OverridingLanguages(string SystemOp, ref List<Language> languages)
    {
        languages.Clear();
        XmlSerializer xml = new XmlSerializer(typeof(List<Language>));
        StreamReader sr = new StreamReader($@"{SystemOp}Languages.xml");
        languages = xml.Deserialize(sr) as List<Language>;
        sr.Dispose();
    }
}