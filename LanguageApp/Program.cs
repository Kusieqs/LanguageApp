namespace LanguageApp;
internal class Program
{
    private static void Main(string[] args)
    {
        #region Data Reading
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

    }
    public static void FirstTime(ref string SystemOp, string Slash,bool FirstTime)
    {
        if (!Directory.Exists($"{SystemOp}LanguageCheck"))
        {
            Directory.CreateDirectory($"{SystemOp}LanguageCheck");
            SystemOp += @$"LanguageApp{Slash}";

            Directory.CreateDirectory($"{SystemOp}E");
            Language l1 = new Language(LanguageName.English);
            Directory.CreateDirectory($@"{SystemOp}E{Slash}Unit1");

            StreamWriter Write = new StreamWriter($@"{SystemOp}E{Slash}Unit1{Slash}W");
            StreamWriter Write1 = new StreamWriter($@"{SystemOp}E{Slash}Unit1{Slash}E");
            Write.Close();
            Write1.Close();
        }
        else
        {
            SystemOp += $@"LanguageApp{Slash}";
        }
    }
}