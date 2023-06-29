using System.Xml.Serialization;
using System;
namespace LanguageApp
{
	public class Language
	{
		private string language;
		private string charLanguage;
		public Language(LanguageName language,CharLanguage charLanguage)
		{
			this.language =language.ToString();
			this.charLanguage = charLanguage.ToString();
		}
        public Language(string language, string charLanguage)
        {
			this.language = language;
			this.charLanguage = charLanguage;
        }
        public Language()
		{ }
		public string Language1
		{
			get
			{
				return language;
			}
			set
			{
				language = value;
			}
		}
		public string CharLanguage
		{
			get
			{
				return charLanguage;
			}
			set
			{
				charLanguage = value;
			}
		}
	}
	public class Word_Description 
	{
		private Language language;
		private string word;
		private string wordInYourLanguage;
		private CategoryName category;
		private int mistakes;
		private string unit = "";
		public Word_Description(string word, string wordInYourLanguage, CategoryName category,Language language)
		{
			this.word = word;
			this.wordInYourLanguage = wordInYourLanguage;
			this.category = category;
			mistakes = 0;

		}
		public string Word
		{
			get
			{
				return word;
			}
			set
			{
				word = value;
			}
		}
		public string WordInYourLanguage
		{
			get
			{
				return wordInYourLanguage;
			}
			set
			{
				wordInYourLanguage = value;
			}
		}
		public CategoryName Category
		{
			get
			{
				return category;
			}
			set
			{
				category = value;
			}
		}
		public int Mistakes
		{
			get
			{
				return mistakes;
			}
			set
			{
				mistakes = value;
			}
		}
		public string Unit
		{
			get
			{
				return unit;
			}
			set
			{
				unit = value;
			}
		}
		public void TextWrite(string SystemOp, string unit, string slash,Language language,string category,string LanChar)
		{
			Console.ReadKey();
			
            StreamWriter wr = File.AppendText($@"{SystemOp}{slash}{LanChar}{slash}{unit}{slash}{category}");
            wr.WriteLine($"{Word}|{WordInYourLanguage}");
            wr.Close();
        }

	}
}

