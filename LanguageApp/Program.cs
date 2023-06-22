using System.Xml.Serialization;
namespace LanguageApp;
internal class Program
{
    private static void Main(string[] args)
    {
        #region Data Reading
        bool FirstTimeBool = false;
        string UserName = System.Environment.UserName;
        bool WindowsOrMac = System.Environment.OSVersion.Platform == PlatformID.Win32NT;
        string SystemOp, Slash;
        List<Language> Languages = new List<Language>();
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
        FirstTime(ref SystemOp, Slash, ref FirstTimeBool,ref Languages);
        OverridingLanguages(SystemOp, ref Languages);
        foreach (var item in Languages)
        {
            Console.WriteLine(item.language + " " + item.charLanguage);
        }

    }
    public static void FirstTime(ref string SystemOp, string Slash,ref bool FirstTimeBool,ref List<Language> languages)
    {
        if (!Directory.Exists($"{SystemOp}LanguageApp"))
        {
            Directory.CreateDirectory($"{SystemOp}LanguageApp");
            SystemOp += @$"LanguageApp{Slash}";

            Language l1 = new Language(LanguageName.English,CharLanguage.E);
            languages.Add(l1);
            XmlSerializer xml = new XmlSerializer(typeof(List<Language>));
            StreamWriter strw = new StreamWriter(@$"{SystemOp}Languages.xml");
            xml.Serialize(strw, languages);
            strw.Dispose();


            Directory.CreateDirectory($"{SystemOp}E");
            Directory.CreateDirectory($@"{SystemOp}E{Slash}Unit1");
            StreamWriter Write = new StreamWriter($@"{SystemOp}E{Slash}Unit1{Slash}W");
            StreamWriter Write1 = new StreamWriter($@"{SystemOp}E{Slash}Unit1{Slash}E");
            Write.Close();
            Write1.Close();

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