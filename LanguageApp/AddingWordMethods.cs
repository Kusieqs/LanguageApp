using System;
namespace LanguageApp
{
	public static class AddingWordMethods
	{
        public static string word_writing()
        {
        Backup:
            Console.Clear();
            Console.Write($"Write a word in another Language:    ");
            string word = Console.ReadLine();
            if (word.Length == 0)
                goto Backup;
            return word;
        }
        public static string word_in_your_language()
        {
        Backup:
            Console.Write("\n\nWrite a word in your language:    ");
            string word = Console.ReadLine();
            if (word.Length == 0)
                goto Backup;
            else
                return word;

        }
        public static string category(out CategoryName categoryName)
        {
            Console.WriteLine("\n\nChoose a category of your word: Word,Expression,Diffrent (W/E/D)");
            ConsoleKeyInfo Choose = new ConsoleKeyInfo();
        backup:
            Choose = Console.ReadKey();
            if (Choose.KeyChar == 'W' || Choose.KeyChar == 'w')
            {
                categoryName = CategoryName.Word;
                return "W";
            }
            else if (Choose.KeyChar == 'E' || Choose.KeyChar == 'e')
            {
                categoryName = CategoryName.Expression;
                return "E";
            }
            else if (Choose.KeyChar == 'D' || Choose.KeyChar == 'd')
            {
                categoryName = CategoryName.Diffrent;
                return "D";
            }
            else
                goto backup;
        }



    }
}

