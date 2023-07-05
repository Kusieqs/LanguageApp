﻿using System.Xml.Serialization;
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
	public class WordDescription 
	{
		public Language Language { get; set; }
		public string Word { get; set; }
		public string WordInYourLanguage { get; set; }
        public CategoryName Category { get; set; }
		public int Mistakes { get; set; }
		public string Unit { get; set; }
		public WordDescription(string word, string wordInYourLanguage, CategoryName category,Language language,string unit)
		{
			this.Word = word;
			this.WordInYourLanguage = wordInYourLanguage;
			this.Category = category;
			Mistakes = 0;
			this.Language = language;
			this.Unit = unit;

		}
		public WordDescription()
		{ }

	}
}

