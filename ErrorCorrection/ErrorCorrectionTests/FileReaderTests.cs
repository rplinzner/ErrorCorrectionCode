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
           var temp = "óćż a";
            var zmienna = fr.BinConvert(temp);

            
            /*foreach (char c in "ć ") 
                Console.WriteLine(Convert.ToString(c, 2).PadLeft(8, '0'));


            System.Console.WriteLine("-------------");*/
            System.Console.WriteLine(zmienna);

            int numOfBytes = zmienna.Length / 8;
            byte[] bytes = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; ++i)
            {
                bytes[i] = Convert.ToByte(zmienna.Substring(8 * i, 8), 2);
            }

            string back = System.Text.Encoding.UTF8.GetString(bytes);

            Console.WriteLine(back);





        }
    }
}