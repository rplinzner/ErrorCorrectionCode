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

            var binStr = string.Join("", bytes.Select(b => Convert.ToString(b, 2).PadLeft(8,'0')));
            var charArray = binStr.ToCharArray();
            return charArray;
        }


        public char[,] Make8ElementPerRow(char[] table)
        {
            char[,] temp = new char[table.Length / 8, 8];

            for (int j = 0; j < table.Length / 8; j++)
            {
                for (int k = 0; k < 8; k++)
                {
                    temp[j, k] = table[(j * 8) + k];
                }
            }

            return temp;
        }

    }
}
