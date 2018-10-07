using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_FileIO_NTier.BusinessLogicLayer;
using Demo_FileIO_NTier.Models;

namespace Demo_FileIO_NTier.PresentationLayer
{
    class Presenter
    {
        static CharacterBLL _charactersBLL;

        public Presenter(CharacterBLL characterBLL)
        {
            _charactersBLL = characterBLL;
            ManageApplicationLoop();
        }

        private void ManageApplicationLoop()
        {
            DisplayWelcomeScreen();
            DisplayListOfCharacters();
            DisplayClosingScreen();
        }

        /// <summary>
        /// Display the header
        /// </summary>
        /// <param name="headerText"></param>
        static void DisplayHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"\t\t{headerText}");
            Console.ReadKey();
        }

        /// <summary>
        /// Display the continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// Display the welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tWelcome");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Display the closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tGood Bye");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Display the tabulated list of Character objects
        /// </summary>
        /// <param name="characters"></param>
        private void DisplayCharacterTable(List<Character> characters)
        {
            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("Id".PadRight(8));
            columnHeader.Append("Full Name".PadRight(25));
            Console.WriteLine(columnHeader.ToString());

            characters = characters.OrderBy(c => c.Id).ToList();

            foreach (Character character in characters)
            {
                StringBuilder characterInfo = new StringBuilder();

                characterInfo.Append(character.Id.ToString().PadRight(8));
                characterInfo.Append(character.FullName().PadRight(25));

                Console.WriteLine(characterInfo.ToString());
            }
        }

        /// <summary>
        /// Display a list of characters
        /// </summary>
        private void DisplayListOfCharacters()
        {
            bool success;
            string message;

            List<Character> characters = _charactersBLL.GetCharacters(out success, out message) as List<Character>;
           

            DisplayHeader("List of Characters");

            if (success)
            {
                characters = characters.OrderBy(c => c.Id).ToList();

                DisplayCharacterTable(characters);
            }
            else
            {
                Console.WriteLine(message);
            }

            DisplayContinuePrompt();
        }
    }
}
