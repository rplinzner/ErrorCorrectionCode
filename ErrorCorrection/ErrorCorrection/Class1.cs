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
           
<<<<<<< HEAD
<<<<<<< HEAD
            var binStr = string.Join(" ", bytes.Select(b => Convert.ToString(b, 2).PadLeft(8,'0')));
            var charArray = binStr.ToCharArray();
            return charArray;

=======
=======
>>>>>>> e1efedeeda8f1e13a40b91fb9d5e2a43c9675925
            var binStr = string.Join("", bytes.Select(b => Convert.ToString(b, 2).PadLeft(8,'0')));
            return binStr;
            
>>>>>>> e1efedeeda8f1e13a40b91fb9d5e2a43c9675925
        }


    }
}
