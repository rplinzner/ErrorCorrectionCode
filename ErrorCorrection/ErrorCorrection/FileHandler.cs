using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace ErrorCorrection
{
    public static class FileHandler
    {
        public static string OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.txt";
            ofd.Filter = "Plik Tekstowy (*.txt)|*.txt|Plik Binarny|*.bin|All Files|*.*";

            if (ofd.ShowDialog() == true)
            {
                return File.ReadAllText(ofd.FileName);
            }

            return "";
        }

        public static bool SaveFile(string text, string mode)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (mode.Contains("TXT")) sfd.Filter = "Plik Tekstowy (*.txt)|*.txt";
            else if (mode.Contains("BIN")) sfd.Filter = "Plik Binarny (*.bin)|*.bin";
            else sfd.Filter = "Plik Dowolny (*.*)|*.*";
            sfd.FileName = "Wiadomosc";
            if (sfd.ShowDialog() == true)
            {
                File.WriteAllText(sfd.FileName, text);
                return true;
            }
            return false;
        }
        public static string print_array_16_row(char[,] table)
        {
            String temp = String.Empty;

            for (int i = 0; i < table.Length / 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    temp += table[i, j];
                }

                temp += "\r\n";
            }

            return temp;
        }

        public static char[,] ReadFile(string text)
        {
            var size = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].Equals('\r'))
                {
                    size += 1;
                }
            }

            char[,] read_mat = new char[size, 16];
            int line = 0;
            int znak = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].Equals('\r'))
                {
                    ++i;
                    if (text[i].Equals('\n'))
                    {
                        ++line;
                    }
                }
                else
                {
                    if (znak == 16) znak = 0;
                    read_mat[line, znak] = text[i];
                    ++znak;
                }
            }

            return read_mat;
        }
        public static string Extract_8_bit(char[,] table)
        {
            string temp = String.Empty;
            int size = table.Length;
            size = size / 16;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    temp += table[i, j];
                }
            }
            return temp;
        }
    }



}
