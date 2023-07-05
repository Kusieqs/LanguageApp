using System;
namespace LanguageApp
{
    public static class AddingWordMethods
    {
        public static void WordWriting(out string word, out string wordInYourLanguage) /// Entering vocabulary and vocabulary data in your language
        {
            Console.Clear();
            Console.Write($"Write a word in another language:    ");
            do
            {
                word = Console.ReadLine();

                if(word.Length == 0)
                    Console.WriteLine("Input can't be empty.");

            } while (word.Length == 0);
            Console.Clear();

            Console.Write($"Write a word in another language:    ");
            do
            {
                wordInYourLanguage = Console.ReadLine();

                if (wordInYourLanguage.Length == 0)
                    Console.WriteLine("Input can't be empty.");

            } while (wordInYourLanguage.Length == 0);


        }
        public static string category(out CategoryName categoryName) // Choosing word category
        {
            bool correctChoosing = false;
            do
            {

                Console.Clear();
                Console.WriteLine("\n\nChoose a category of your word: Word,Expression,Diffrent (W/E/D)");
                ConsoleKeyInfo choose = new ConsoleKeyInfo(); 
                choose = Console.ReadKey();

                switch (choose.Key.ToString().ToUpper())
                {
                    case "W":
                        categoryName = CategoryName.Word;
                        return "W";
                    case "E":
                        categoryName = CategoryName.Expression;
                        return "E";
                    case "D":
                        categoryName = CategoryName.Diffrent;
                        return "D";
                    default:
                        break;
                }
            } while (correctChoosing == false);

            throw new FormatException("Error with choosing category");
        }

    }
}

