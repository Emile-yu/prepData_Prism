﻿using CsvHelper;
using prepData.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using prepData.Support.Models;

namespace prepData.Support.Business
{
    public class SupportManager : DataProvider<Supports>, IExport<Supports>
    {
        #region static membres

        private static CsvHelper<SupportMapClass> s_csvHelper = new CsvHelper<SupportMapClass>();

        #endregion

        #region constructor
        public SupportManager(string filePath) : base(filePath)
        {
        }

        #endregion

        protected override List<Supports> GetAllLines(StreamReader reader, int posStart, int posEnd)
        {
            List<Supports> l_elements;
            l_elements = s_csvHelper.Reader<Supports>(reader);
            return l_elements;
        }

        public void Export(string fileName, List<Supports> supps)
        {
            List<SupportExport> result = supps.Select(o => new SupportExport(Tools.ParseIdOfIndividual(o.MedNum), o.IdTitre , o.Parution, o.Jour)).ToList();
            using (var writer = new StreamWriter(fileName))
            {
                s_csvHelper.Writer<SupportExport>(writer, result);
            }
        }
    }
}
