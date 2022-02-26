using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Core
{
    public abstract class DataProvider<T> : IDataProvider<T> where T : ILineParser
    {
        public string _filePath { get; protected set; }

        public int _posStart { get; set; }
        
        public int _posEnd { get; set; }

        public DataProvider(string filePath, int posStart = 0, int posEnd = 0)
        {
            _filePath = filePath;
            _posStart = posStart;
            _posEnd = posEnd;
        }

        public List<T> Provider()
        {
            List<T> l_lines;

            string l_filePath;
            l_filePath = Path.GetFullPath(_filePath);
            try
            {
                using (var reader = new StreamReader(l_filePath))
                {
                    l_lines = GetAllLines(reader, _posStart, _posEnd);
                }
                return l_lines;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        protected virtual List<T> GetAllLines(StreamReader reader, int posStart, int posEnd)
        {
            List<T> l_lines = new List<T>();
            string l_line;
            int l_error = 0;
            while ((l_line = reader.ReadLine()) != null)
            {
                T ind = GetInstanceGeneric();
                if (ind.Parse(l_line, posStart, posEnd)) l_lines.Add(ind);
                else l_error++;
            }
            if (l_error > 0)
                Console.WriteLine($"Avertissement : {l_error} lignes n'ont pas pu être récupérées.");

            return l_lines;
        }

        private T GetInstanceGeneric()
        {
            Type type = typeof(T);

            return (T)Activator.CreateInstance<T>();
        }
    }
}
