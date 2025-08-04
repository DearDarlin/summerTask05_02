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
    }
}


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter text to translate to morse code:");
        string input = Console.ReadLine();
        string morseCode = MorseTranslator.TranslateMorse(input);
        Console.WriteLine("Morse Code: " + morseCode);

    }
}