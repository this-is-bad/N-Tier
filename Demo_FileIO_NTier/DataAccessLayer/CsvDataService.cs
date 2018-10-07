using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_FileIO_NTier.Models;

namespace Demo_FileIO_NTier.DataAccessLayer
{  
    /// <summary>
    /// Manage reading and writing to CSV file
    /// </summary>
    public class CsvDataService : IDataService
    {
        private string _dataFilePath;

        public CsvDataService()
        {
            _dataFilePath = DataSettings.dataFilePath;
        }

        /// <summary>
        /// Read Character data from a CSV file
        /// </summary>
        /// <returns>IEnumerable of Character</returns>
        public IEnumerable<Character> ReadAll()
        {
            List<string> characterStrings = new List<string>();
            List<Character> characters = new List<Character>();

            try
            {
                StreamReader sr = new StreamReader(_dataFilePath);
                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        characterStrings.Add(sr.ReadLine());
                    }
                }
                foreach (string characterString in characterStrings)
                {
                    characters.Add(CharacterObjectBuilder(characterString));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return characters;
        }

        /// <summary>
        /// Write Character data to a CSV file
        /// </summary>
        /// <param name="characters"></param>
        public void WriteAll (IEnumerable<Character> characters)
        {
            try
            {
                StreamWriter sw = new StreamWriter(_dataFilePath);
                using (sw)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Clear();
                    foreach (Character character in characters)
                    {
                        sb.AppendLine(CharacterStringBuilder(character));
                    }
                    sw.Write(sb.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Convert a string into a Character Object
        /// </summary>
        /// <param name="characterString"></param>
        /// <returns>Character</returns>
        private Character CharacterObjectBuilder(string characterString)
        {
            const char DELINEATOR = ',';
            string[] properties = characterString.Split(DELINEATOR);

            Character character = new Character()
            {
                Id = Convert.ToInt32(properties[0]),
                LastName = properties[1],
                FirstName = properties[2],
                Address = properties[3],
                City = properties[4],
                State = properties[5],
                Zip = properties[6],
                Age = Convert.ToInt32(properties[7]),
                Gender = (Character.GenderType)Enum.Parse(typeof(Character.GenderType), properties[8])
            };

            return character;
        }

        /// <summary>
        /// Concatenate Character object data into a string
        /// </summary>
        /// <param name="characterObject"></param>
        /// <returns>string</returns>
        private string CharacterStringBuilder(Character characterObject)
        {
            const string DELINEATOR = ",";
            string characterString;

            characterString =
                characterObject.Id + DELINEATOR +
                characterObject.LastName + DELINEATOR +
                characterObject.FirstName + DELINEATOR +
                characterObject.Address + DELINEATOR +
                characterObject.City + DELINEATOR +
                characterObject.State + DELINEATOR +
                characterObject.Zip + DELINEATOR +
                characterObject.Gender;

            return characterString;
        }
        
    }
}
