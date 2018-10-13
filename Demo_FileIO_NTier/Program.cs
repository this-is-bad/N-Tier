﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_FileIO_NTier.BusinessLogicLayer;
using Demo_FileIO_NTier.DataAccessLayer;
using Demo_FileIO_NTier.Models;
using Demo_FileIO_NTier.PresentationLayer;

namespace Demo_FileIO_NTier
{
    class Program
    {
        static void Main(string[] args)
        {
            //IDataService dataService = new CsvDataService();
            //IDataService dataService = new XmlDataService(DataSettings.dataFilePath);
            IDataService dataService = new JsonDataService(DataSettings.dataFilePath);
            CharacterBLL characterBLL = new CharacterBLL(dataService);
            Presenter presenter = new Presenter(characterBLL);

        }
    }
}
