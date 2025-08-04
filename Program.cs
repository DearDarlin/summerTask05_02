using System;
using System.Collections.Generic;
using System.Text;
using TextMorse;

namespace TextMorse
{
    public static class MorseTranslator
    {
        private static readonly Dictionary<char, string> MorseCode = new Dictionary<char, string>()
        {
            {'A', ".-"},    {'B', "-..."},  {'C', "-.-."},  {'D', "-.."},
            {'E', "."},     {'F', "..-."},  {'G', "--."},   {'H', "...."},
            {'I', ".."},    {'J', ".---"},  {'K', "-.-"},   {'L', ".-.."},
            {'M', "--"},    {'N', "-."},    {'O', "---"},   {'P', ".--."},
            {'Q', "--.-"},  {'R', ".-."},   {'S', "..."},   {'T', "-"},
            {'U', "..-"},   {'V', "...-"},  {'W', ".--"},   {'X', "-..-"},
            {'Y', "-.--"},  {'Z', "--.."},

            {'0', "-----"}, {'1', ".----"}, {'2', "..---"}, {'3', "...--"},
            {'4', "....-"}, {'5', "....."}, {'6', "-...."}, {'7', "--..."},
            {'8', "---.."}, {'9', "----."},

            {' ', "/"}, {'.', ".-.-.-"}, {',', "--..--"}, {'?', "..--.."}
        };

        private static readonly Dictionary<string, char> ReverseMorse;
        static MorseTranslator()
        {
            ReverseMorse = new Dictionary<string, char>();
            foreach(var pair in MorseCode)
            {
                ReverseMorse[pair.Value] = pair.Key;
            }
        }

        public static string TranslateMorse(string text)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in text.ToUpper())
            {
                if (MorseCode.ContainsKey(c))
                {
                    result.Append(MorseCode[c] + " ");
                }
                else
                {
                    result.Append("? ");
                }
            }

            return result.ToString().Trim();
        }


        public static string TranslateFromMorse(string morseText)
        {
            StringBuilder result = new StringBuilder();
            string[] words = morseText.Trim().Split('/'); 

            foreach (string word in words)
            {
                string[] symbols = word.Trim().Split(' ');
                foreach (string symbol in symbols)
                {
                    if (ReverseMorse.ContainsKey(symbol))
                        result.Append(ReverseMorse[symbol]);
                    else
                        result.Append('?'); 
                }
                result.Append(' ');
            }

            return result.ToString().TrimEnd();
        }



    }
}


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("MENU");
        Console.WriteLine("1. translate from text to morse code");
        Console.WriteLine("2. translate from morse code to text");
        Console.Write("Chose an option: ");
        string option = Console.ReadLine();

        if ( option =="1")
        {
            Console.WriteLine("Enter text to translate: ");
            string input = Console.ReadLine();
            string morse = MorseTranslator.TranslateMorse(input);
            Console.WriteLine("\nMorse Code:");
            Console.WriteLine(morse);
        }
        else if (option=="2")
        {
            Console.WriteLine("Enter Morse Code:");
            string input = Console.ReadLine();
            string text = MorseTranslator.TranslateFromMorse(input);
            Console.WriteLine("\nEnglish text:");
            Console.WriteLine(text);
        }
        else
        {
            Console.WriteLine("Invalid option selected. Please choose 1 or 2.");
        }

    }
}