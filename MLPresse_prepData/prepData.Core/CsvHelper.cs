using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Core
{
    public class CsvHelper<S>
    where S : ClassMap
    {
        public string Delimiter { get; set; } = ";";
        public List<T> Reader<T>(StreamReader streamReader)
        {
            using (var reader = new CsvReader(streamReader, CultureInfo.InstalledUICulture))
            {
                reader.Configuration.Delimiter = this.Delimiter;
                reader.Configuration.RegisterClassMap<S>();
                List<T> l_elements = reader.GetRecords<T>().ToList();

                return l_elements;
            }
        }

        public void Writer<T>(StreamWriter streamWriter, List<T> result)
        {
            using (var writer = new CsvWriter(streamWriter, CultureInfo.InstalledUICulture))
            {
                writer.Configuration.HasHeaderRecord = false;
                writer.Configuration.Delimiter = this.Delimiter;
                writer.WriteRecords(result);
            }
        }
    }
}
