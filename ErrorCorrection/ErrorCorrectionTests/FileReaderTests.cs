﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ErrorCorrection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;


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


            char[,] tab_hash = new char[8, 16]
            {
                {'1', '0', '1', '1', '0', '1', '0', '0', '1', '0', '0', '0', '0', '0', '0', '0'},
                {'1', '0', '0', '1', '1', '1', '0', '0', '0', '1', '0', '0', '0', '0', '0', '0'},
                {'1', '1', '1', '0', '1', '0', '1', '0', '0', '0', '1', '0', '0', '0', '0', '0'},
                {'1', '1', '0', '0', '0', '1', '1', '0', '0', '0', '0', '1', '0', '0', '0', '0'},
                {'1', '1', '1', '0', '1', '0', '0', '1', '0', '0', '0', '0', '1', '0', '0', '0'},
                {'1', '0', '0', '1', '0', '1', '0', '1', '0', '0', '0', '0', '0', '1', '0', '0'},
                {'0', '0', '1', '1', '1', '1', '1', '1', '0', '0', '0', '0', '0', '0', '1', '0'},
                {'1', '1', '1', '0', '0', '1', '1', '1', '0', '0', '0', '0', '0', '0', '0', '1'}};
        
            System.Console.WriteLine(zmienna[5]);

            int size=0;

            for (int i = 0; i < zmienna.Length; i++)
            {
                if (zmienna[i] != ' ')
                {
                    size += 1;
                }
            }

            char[] bin_tab = new char[size];

            int j = 0;

            for(int i=0;i<zmienna.Length;i++)
            {
                if (zmienna[i] != ' ')
                {
                    bin_tab[j] = zmienna[i];
                    j += 1;
                }
            }


        
            string s = new string(zmienna);
            System.Console.WriteLine(s);
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            string result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            System.Console.WriteLine(result);


        }
    }
    }
    