using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_FileIO_NTier.Data;
using Demo_FileIO_NTier.Models;
using Newtonsoft;
using Newtonsoft.Json;

namespace Demo_FileIO_NTier.DataAccessLayer
{
    class JsonDataService : IDataService
    {
        private string _dataFilePath;

        public JsonDataService()
        {
            _dataFilePath = DataSettings.dataFilePath;
        }

        public JsonDataService(string dataFilePath)
        {
            _dataFilePath = dataFilePath;
        }

        /// <summary>
        /// Read the JSON file and load a list of character objects
        /// </summary>
        /// <returns>list of characters</returns>
        public IEnumerable<Character> ReadAll()
        {
            List<Character> characters = new List<Character>();
            //JsonSerializer serializer = new JsonSerializer();

            try
            {
                StreamReader sr = new StreamReader(_dataFilePath);
                using (sr)
                {
                    string jsonString = sr.ReadToEnd();
                    Characters characterList = JsonConvert.DeserializeObject<RootObject>(jsonString).Characters;

                    characters = characterList.Character;
                    //characters = (List<Character>)serializer.Deserialize(reader);
                }

            }
            catch (Exception)
            {
                throw; // all exceptions are handled in the ListForm class
            }

            return characters;
        }

        /// <summary>
        /// write the current list of characters to the JSON data file
        /// </summary>
        /// <param name="characters">list of characters</param>
        public void WriteAll(IEnumerable<Character> characters)
        {

            RootObject rootObject = new RootObject();
            rootObject.Characters = new Characters();
            rootObject.Characters.Character = characters as List<Character>;

            string jsonString = JsonConvert.SerializeObject(rootObject);

            try
            {
                StreamWriter writer = new StreamWriter(_dataFilePath);
                using (writer)
                {
                    writer.WriteLine(jsonString);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
