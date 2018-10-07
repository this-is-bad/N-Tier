using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_FileIO_NTier.DataAccessLayer;
using Demo_FileIO_NTier.Models;

namespace Demo_FileIO_NTier.BusinessLogicLayer
{
    class CharacterBLL
    {
        IDataService _dataService;
        List<Character> _characters;

        public CharacterBLL(IDataService dataService)
        {
            _dataService = dataService;
        }
        /// <summary>
        /// Reads the data source to get a list of Character objects
        /// </summary>
        /// <param name="success">bool that indicates success</param>
        /// <param name="message">string that holds error messages</param>
        /// <returns>IEnumerable of Character</returns>
        public IEnumerable<Character> GetCharacters(out bool success, out string message)
        {
            _characters = null;
            success = false;
            message = "";
            try
            {
                _characters = _dataService.ReadAll() as List<Character>;
                _characters.OrderBy(c => c.Id);

                if (_characters.Count > 0)
                {
                    success = true;
                }
                else
                {
                    message = "It appears that there is no data in the file.";
                }
            }
            catch(FileNotFoundException)
            {
                message = "Unable to locate the data file.";
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return _characters;
        }
    }
}
