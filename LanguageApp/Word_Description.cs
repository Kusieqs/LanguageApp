using System.Xml.Serialization;
using System;
namespace LanguageApp
{
    public class Language
	{
		private string language;
		private char charLanguage;
		public Language(LanguageName language,char charLanguage)
		{
			this.language =language.ToString();
			this.charLanguage = charLanguage;
		}
        public Language(string language, char charLanguage)
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
		public char CharLanguage
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
	public class WordDescription 
	{
		public Language Language { get; set; }
		public string Word { get; set; }
		public string WordInYourLanguage { get; set; }
        public CategoryType Category { get; set; }
		public int Mistakes { get; set; }
		public string Unit { get; set; }
		public WordDescription(string word, string wordInYourLanguage, CategoryType category,Language language,string unit,int mistakes)
		{
			this.Word = word;
			this.WordInYourLanguage = wordInYourLanguage;
			this.Category = category;
			this.Mistakes = mistakes;
			this.Language = language;
			this.Unit = unit;

		}
		public WordDescription(WordDescription wordDescription)
		{
			this.Word = wordDescription.Word;
			this.WordInYourLanguage = wordDescription.WordInYourLanguage;
			this.Category = wordDescription.Category;
			this.Mistakes = wordDescription.Mistakes;
			this.Language = wordDescription.Language;
			this.Unit = wordDescription.Unit;
        }
		public WordDescription()
		{ }

	}
}

