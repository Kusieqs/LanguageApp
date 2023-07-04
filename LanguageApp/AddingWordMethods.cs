using System;
namespace LanguageApp
{
    public static class AddingWordMethods
    {
        public static string word_writing() //W C# konwecja nazewnictwa metod jest taka: WordWriting Czyli wszystkie słowa z dużej, bez spacji.
        {
        Backup: //Nie rób czegoś takiego. Używanie GOTO w C# nie jest dobrą praktyką. Zamiast tego użyj pętli np. do while.
            Console.Clear();
            Console.Write($"Write a word in another language:    ");
            string word = Console.ReadLine();
            if (word.Length == 0)
                goto Backup;
            return word;
        }

        public static string WordWritingExample()
        {
            Console.Clear();
            Console.Write($"Write a word in another language:    ");
            
            string word;
           
            do
            {
                word = Console.ReadLine();
                
                if (word.Length == 0)
                    Console.WriteLine("Input can't be empty.");

            } while (word.Length == 0);

            return word;
        }

        public static string word_in_your_language() //Tu też zmien nazewnictwo
        {
        Backup: //zmien goto na inna petle
            Console.Write("\n\nWrite a word in your language:    ");
            string word = Console.ReadLine();
            if (word.Length == 0)
                goto Backup;
            else
                return word;
        }
        //W zasadzie metody WordWriting jak i WordInYourLanguage da się skoncentrować do jednej metody, ponieważ robią praktycznie to samo. Spróbuj to wykminić samemu.

        public static string category(out CategoryName categoryName) //Nazwy funkcji powinny także pełnić rolę informacyjną co dana funkcja robi. Samo Category nie informuje programisty w zaden sposob, od czego jest ta funkcja.
        {
            Console.WriteLine("\n\nChoose a category of your word: Word,Expression,Diffrent (W/E/D)");
            ConsoleKeyInfo Choose = new ConsoleKeyInfo(); // nazwy zmiennych z małej a jak jest więcej niż jedno słowo to wtedy camelCase (np. chooseKey).
        backup://nie uzywaj goto
            Choose = Console.ReadKey();
            if (Choose.KeyChar == 'W' || Choose.KeyChar == 'w')//sugerowałbym użycia switcha, jest bardziej przejrzysty i wygodniejszy.
            {
                categoryName = CategoryName.Word;
                return "W";
            }
            else if (Choose.KeyChar == 'E' || Choose.KeyChar == 'e')
            {
                categoryName = CategoryName.Expression;
                return "E";
            }
            else if (Choose.KeyChar == 'D' || Choose.KeyChar == 'd')//zamiast tworzyć dwa warunki możesz po prostu robic tak: Choose.KeyChar.ToString().ToLower() == "d" 
            {
                categoryName = CategoryName.Diffrent;
                return "D";
            }
            else
                goto backup;
        }



    }
}

