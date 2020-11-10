using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VideogameShopLibrary
{
    public class CsvToJsonConverter
    {
        public string ProcessCsvFile(StringBuilder sb, string nameOfOutputFile)
        {
            string sbString = sb.ToString();
            String[] arr = sbString.Split("TABTAB");
            TextWriter File = new StreamWriter(nameOfOutputFile);
            foreach (var line in arr)
            {
                File.WriteLine(line);
            }
            File.Close();
            
            return nameOfOutputFile;
        }
    }
}
