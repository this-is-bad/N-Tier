using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Demo_FileIO_NTier.Models;
using Demo_FileIO_NTier.DataAccessLayer;

namespace Demo_FileIO_NTier.DataAccessLayer
{
    public class XmlDataService : IDataService
    {
        private string _dataFilePath;

        public XmlDataService()
        {
            _dataFilePath = DataSettings.dataFilePath;
        }

        /// <summary>
        /// Read the XML file and load a list of character objects
        /// </summary>
        /// <returns>list of characters</returns>
        public IEnumerable<Character> ReadAll()
        {
            IEnumerable<Character> characters = new List<Character>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Character>), new XmlRootAttribute("Characters"));

            try
            {
                StreamReader reader = new StreamReader(_dataFilePath);
                using (reader)
                {
                    characters = (List<Character>)serializer.Deserialize(reader);
                }

            }
            catch (Exception)
            {
                throw; // all exceptions are handled in the ListForm class
            }

            return characters;
        }

        /// <summary>
        /// write the current list of characters to the XML data file
        /// </summary>
        /// <param name="characters">list of characters</param>
        public void WriteAll(IEnumerable<Character> characters)
        {
        
            XmlSerializer serializer = new XmlSerializer(typeof(List<Character>), new XmlRootAttribute("Characters"));

            try
            {
                StreamWriter writer = new StreamWriter(_dataFilePath);
                using (writer)
                {
                    serializer.Serialize(writer, characters);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    //     if (character.FirstName == null & character.LastName == null)
    //        {
    //            InitializeDataFile.SeedDataFile();
    //        }
    //        else
    //        {
    //            XmlSerializer serializer = new XmlSerializer(typeof(Character), new XmlRootAttribute("Characters"));

    //StreamWriter sWriter = new StreamWriter(_dataFilePath);

    //            using (sWriter)
    //            {
    //                serializer.Serialize(sWriter, character);
    //            }
    //        }
    }
}
