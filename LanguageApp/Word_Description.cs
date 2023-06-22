using System;
namespace LanguageApp
{
	public class Language
	{
		public LanguageName language;
		public Language(LanguageName language)
		{
			this.language = language;
		}
	}
	public class Word_Description : Language
	{
		private string word;
		private string wordInYourLanguage;
		private CategoryName category;
		private int mistakes;
		private string unit = "";
		public Word_Description()
		{

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
		public string WordInYOurLanguage
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

