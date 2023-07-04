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
	public class Word_Description // W nazwie klasy nie uzywamy podlogi. Powinno byc WordDescription.
	{ 
		private Language language; //prywatne pola oznaczamy w ten sposob: _language
		private string word;
		private string wordInYourLanguage; //_wordInYourLanguage i z innymi to samo.
        private CategoryName category;
		private int mistakes;
		private string unit;
		public Word_Description(string word, string wordInYourLanguage, CategoryName category,Language language,string unit)
		{
			this.word = word;
			this.wordInYourLanguage = wordInYourLanguage;
			this.category = category;
			mistakes = 0;
			this.language = language;
			this.unit = unit;

		}
		public Word_Description()
		{ }
		public string Word //Nie musisz uzywac rozbudowanych wlasciwosci jesli nie masz w nich zaimplementowanej logiki. Możesz zrobić tak: public string Word { get; set; }
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

	}
}

