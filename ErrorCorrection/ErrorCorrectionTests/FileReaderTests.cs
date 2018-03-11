using Microsoft.VisualStudio.TestTools.UnitTesting;
using ErrorCorrection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorCorrection.Tests
{
    [TestClass()]
    public class FileReaderTests
    {
        [TestMethod()]
        public void BinConvertTest()
        {
            
           FileReader fr = new FileReader();

            var temp = "óćż asadasdasd";
            var zmienna = fr.BinConvert(temp);
            var temp2 = fr.Make8ElementPerRow(zmienna);


            char[,] tab_hash = new char[8, 16]
            {
                {'1', '0', '1', '1', '0', '1', '0', '0', '1', '0', '0', '0', '0', '0', '0', '0'},
                {'1', '0', '0', '1', '1', '1', '0', '0', '0', '1', '0', '0', '0', '0', '0', '0'},
                {'1', '1', '1', '0', '1', '0', '1', '0', '0', '0', '1', '0', '0', '0', '0', '0'},
                {'1', '1', '0', '0', '0', '1', '1', '0', '0', '0', '0', '1', '0', '0', '0', '0'},
                {'1', '1', '1', '0', '1', '0', '0', '1', '0', '0', '0', '0', '1', '0', '0', '0'},
                {'1', '0', '0', '1', '0', '1', '0', '1', '0', '0', '0', '0', '0', '1', '0', '0'},
                {'0', '0', '1', '1', '1', '1', '1', '1', '0', '0', '0', '0', '0', '0', '1', '0'},
                {'1', '1', '1', '0', '0', '1', '1', '1', '0', '0', '0', '0', '0', '0', '0', '1'}

            };
                var cl = new MathOperations();
            var  suma_k = cl.suma_kontrolna(temp2, tab_hash);


   /*         int numOfBytes = zmienna.Length / 8;
            byte[] bytes = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; ++i)
            {
                bytes[i] = Convert.ToByte(zmienna.Substring(8 * i, 8), 2);
            }

            string back = System.Text.Encoding.UTF8.GetString(bytes);

            Console.WriteLine(back);*/
        



            string s = new string(zmienna);
            System.Console.WriteLine(s);
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            string result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            System.Console.WriteLine(result);
            
        }
    }
   }
    