
using System;
using System.IO;

namespace PA4
{
    public static class FileLog
    {
        public static void Log(string message)
        {
            // Ensure the log message contains a timestamp and the performed action
            string logMessage = $"{DateTime.Now}: {message}";
            File.AppendAllText("activityLog.txt", logMessage + Environment.NewLine);
        }
    }

    class Program
    {
        static char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        static void Main(string[] args)
        {
            while (true)
            {
                string userChoice = DisplayMenu();
                if (userChoice == "4")
                {
                    Console.WriteLine("Exiting...");
                    FileLog.Log("Exited the application.");
                    break;
                }
                RouteEm(userChoice);
            }
        }

        static string DisplayMenu()
        {
            Console.WriteLine("1. File Encoder\n2. File Decoder\n3. Word Count\n4. Exit");
            return Console.ReadLine();
        }

        static void RouteEm(string userChoice)
        {
            switch (userChoice)
            {
                case "1":
                    FileEncoder();
                    break;
                case "2":
                    FileDecoder();
                    break;
                case "3":
                    WordCount();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    FileLog.Log("Attempted invalid menu option.");
                    break;
            }
        }

        static void FileEncoder()
        {
            Console.Write("Enter a word to encode: ");
            string input = Console.ReadLine();
            string encoded = EncodeOrDecode(input);
            Console.WriteLine($"Encoded word: {encoded}");
            FileLog.Log($"Encoded '{input}' to '{encoded}'.");
        }

        static void FileDecoder()
        {
            Console.Write("Enter a ROT-13 encoded word to decode: ");
            string input = Console.ReadLine();
            string decoded = EncodeOrDecode(input); // ROT-13 decoding is the same as encoding
            Console.WriteLine($"Decoded word: {decoded}");
            FileLog.Log($"Decoded '{input}' to '{decoded}'.");
        }

        static string EncodeOrDecode(string input)
        {
            char[] transformedArray = new char[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                char letter = input[i];
                int index = char.IsUpper(letter) ? Array.IndexOf(alphabet, letter) : Array.IndexOf(alphabet, char.ToUpper(letter));

                if (index != -1)
                {
                    transformedArray[i] = index < 13 ? char.IsUpper(letter) ? alphabet[index + 13] : char.ToLower(alphabet[index + 13]) :
                        char.IsUpper(letter) ? alphabet[index - 13] : char.ToLower(alphabet[index - 13]);
                }
                else
                {
                    transformedArray[i] = letter;
                }
            }
            return new string(transformedArray);
        }

        static void WordCount()
        {
            Console.Write("Enter text to count words: ");
            string input = Console.ReadLine();
            int wordCount = input.Split(new char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
            Console.WriteLine($"Word count: {wordCount}");
            FileLog.Log($"Counted {wordCount} words in input.");
        }
    }
}