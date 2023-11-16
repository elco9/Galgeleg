using System;
using System.ComponentModel.Design;

namespace Galgeleg
{
    internal class Program
    {
        // Words that can be used
        static string[] words = { "banangul", "maskinskrivning", "objektorienteret", "toolbox" };

        // Number of lives
        static int lives = 5;
        static bool isTrue = false;

        static void Main(string[] args)
        {
            // Picks a random word from the words array
            string pickedWord = words[new Random().Next(0, words.Length - 1)];

            // Takes the number of pickedWord characters and converts to _
            string displayWord = new string('_', pickedWord.Length);

            // String to store guessed letters
            string guessedLetters = "";

            // Initial display
            DisplayCurrentState(guessedLetters, displayWord);
            DisplayHangman();

            
            // Loop repeats until the word is guessed or lives run out
            while (displayWord.Contains('_') && lives > 0)
            {
                // Get user input
                char guessedLetter = GetUserInput("Guess a letter: ");

                // Check if the guessed letter has already been guessed
                if (guessedLetters.Contains(guessedLetter))
                {
                    Console.WriteLine("You already guessed the letter '" + guessedLetter + "'. Try again.");
                    continue; // Skip the rest of the loop and restart
                }

                guessedLetters += guessedLetter; // Add the guessed letter to the list

                // Check if the guessed letter is in the word
                UpdateDisplayWord(guessedLetter, pickedWord, ref displayWord);

                // Display the current state
                DisplayCurrentState(guessedLetters, displayWord);

                // Manage lives and display feedback
                LoseLife(displayWord);
                WinOrLose(displayWord);
                DisplayHangman();
            }

        }

        // Function to get user input as a char
        static char GetUserInput(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            while (input.Length != 1 || !char.IsLetter(input[0]))
            {
                Console.WriteLine("Invalid input. Please enter a single letter.");
                Console.Write(prompt);
                input = Console.ReadLine();
            }

            return char.ToLower(input[0]);
        }

        // Checks if the guess is right or wrong and updates the display word
        static void UpdateDisplayWord(char guessedLetter, string pickedWord, ref string displayWord)
        {
            for (int i = 0; i < pickedWord.Length; i++)
            {
                if (pickedWord[i] == guessedLetter)
                {
                    // Update the display word with the correct letter
                    displayWord = displayWord.Substring(0, i) + guessedLetter + displayWord.Substring(i + 1);
                    isTrue = true;
                }
            }
        }

        // Manages lives and displays feedback
        static void LoseLife(string displayWord)
        {
            if (isTrue)
            {
                Console.WriteLine("Correct!");
                isTrue = false;
            }
            else
            {
                Console.WriteLine("Wrong!");
                lives--;
            }

            Console.WriteLine("Lives Left: " + lives);
        }

        // Display the current state
        static void DisplayCurrentState(string guessedLetters, string displayWord)
        {
            Console.Clear();
            Console.WriteLine("Guessed Letters: " + guessedLetters);
            Console.WriteLine("Word to guess: " + displayWord);
        }

        // Message if you win or lose
        static void WinOrLose(string displayWord)
        {
            // Checks if you have still have empty letters and have lost all lives
            if (displayWord.Contains('_') && lives == 0)
            {
                Console.WriteLine("You Lose!");
            }
            // Check if you have no empty letters and lives left
            else if (!displayWord.Contains('_') && lives > 0)
            {
                Console.WriteLine("You win!");
            } 
        }
        static void DisplayHangman()
        {
            switch (lives)
            {
                case 5:
                    Console.WriteLine("  ________");
                    Console.WriteLine("  |       ");
                    Console.WriteLine("  |       ");
                    Console.WriteLine("  |");
                    Console.WriteLine("  |");
                    Console.WriteLine("  |");
                    break;
                case 4:
                    Console.WriteLine("  ________");
                    Console.WriteLine("  |      |");
                    Console.WriteLine("  |      O");
                    Console.WriteLine("  |");
                    Console.WriteLine("  |");
                    Console.WriteLine("  |");
                    break;
                case 3:
                    Console.WriteLine("  ________");
                    Console.WriteLine("  |      |");
                    Console.WriteLine("  |      O");
                    Console.WriteLine("  |      |");
                    Console.WriteLine("  |");
                    Console.WriteLine("  |");
                    break;
                case 2:
                    Console.WriteLine("  ________");
                    Console.WriteLine("  |      |");
                    Console.WriteLine("  |      O");
                    Console.WriteLine("  |     /|\\");
                    Console.WriteLine("  |");
                    Console.WriteLine("  |");
                    break;
                case 1:
                    Console.WriteLine("  ________");
                    Console.WriteLine("  |      |");
                    Console.WriteLine("  |      O");
                    Console.WriteLine("  |     /|\\");
                    Console.WriteLine("  |     / ");
                    Console.WriteLine("  |");
                    break;
                case 0:
                    Console.WriteLine("  ________");
                    Console.WriteLine("  |      |");
                    Console.WriteLine("  |      O");
                    Console.WriteLine("  |     /|\\");
                    Console.WriteLine("  |     / \\");
                    Console.WriteLine("  |");
                    break;
                default:
                    break;
            }
        }
    }
}