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

            } while (word.Length == 0);

            Console.Write($"\n\nWrite a word in your language:    ");
            do
            {
                wordInYourLanguage = Console.ReadLine();

            } while (wordInYourLanguage.Length == 0);


        }// Entering data of word
        public static string category(out CategoryType categoryName) // Choosing word category
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
                        categoryName = CategoryType.Word;
                        return "W";
                    case "E":
                        categoryName = CategoryType.Expression;
                        return "E";
                    case "D":
                        categoryName = CategoryType.Diffrent;
                        return "D";
                    default:
                        break;
                }
            } while (correctChoosing == false);

            throw new FormatException("Error with choosing category");
        }

    }
}

