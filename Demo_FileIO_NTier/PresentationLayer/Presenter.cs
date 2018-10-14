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
            //Console.ReadKey();
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

        /// <summary>
        /// prompt the user to select a data source and return the choice
        /// </summary>
        /// <returns>int</returns>
        public int DisplayGetDataTypeChoice()
        {
            Console.WriteLine("Choose a data source");

            int result = 2;
            bool usingMenu = true;

            while (usingMenu)
            {

                Console.CursorVisible = false;

                //
                // display the menu
                //
                Console.WriteLine("\n\n" + "Please type the number of your menu choice.");

                Console.Write(
                 "\t" + "1. CSV" + Environment.NewLine +
                 "\t" + "2. XML" + Environment.NewLine +
                 "\t" + "3. JSON" + Environment.NewLine +
                 "\t" + "E. Exit" + Environment.NewLine);


                //
                // get and process the user's response
                // note: ReadKey argument set to "true" disables the echoing of the key press
                //
                ConsoleKeyInfo userResponse = Console.ReadKey(true);
                switch (userResponse.KeyChar)
                {
                    case '1':
                        result = int.Parse(userResponse.ToString());
                        usingMenu = false;
                        break;
                    case '2':
                        result = int.Parse(userResponse.ToString());
                        usingMenu = false;
                        break;
                    case '3':
                        result = int.Parse(userResponse.ToString());
                        usingMenu = false;
                        break;
                    case 'E':
                    case 'e':
                        DisplayClosingScreen();
                        usingMenu = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine(
                            "It appears you have selected an incorrect choice." + Environment.NewLine +
                            "XML has been automatically selected as the data type." + Environment.NewLine +
                            "Press any key to continue.");

                        userResponse = Console.ReadKey(true);
                        if (userResponse.Key == ConsoleKey.Escape)
                        {
                            usingMenu = false;
                        }
                        break;
                }
            }
            Console.CursorVisible = true;

            return result;
        }

    }
}
