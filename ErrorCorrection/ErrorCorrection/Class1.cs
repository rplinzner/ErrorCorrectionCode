using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorCorrection
{
    public class FileReader
    {
       public  string FileReading(string txt_file)
        {
            string text = System.IO.File.ReadAllText(txt_file);
            return text;
        }

        public char[] BinConvert(string word)
        {
            var bytes = Encoding.UTF8.GetBytes(word);
           

            var binStr = string.Join(" ", bytes.Select(b => Convert.ToString(b, 2).PadLeft(8,'0')));
            var charArray = binStr.ToCharArray();
            return charArray;



        }


    }
}
