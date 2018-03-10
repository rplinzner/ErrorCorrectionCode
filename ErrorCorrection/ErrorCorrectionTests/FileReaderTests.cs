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
           var temp = "ć ";
            var zmienna = fr.BinConvert(temp);

            
            /*foreach (char c in "ć ") 
                Console.WriteLine(Convert.ToString(c, 2).PadLeft(8, '0'));


            System.Console.WriteLine("-------------");*/
            System.Console.WriteLine(zmienna);

            
            
            Console.WriteLine(lol);

        }
    }
}