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

        public static bool SaveFile(string text, string mode) //metoda sluzaca do zapisu do pliku
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (mode.Contains("TXT")) sfd.Filter = "Plik Tekstowy (*.txt)|*.txt"; //opcja zapisu do pliku txt
            else if (mode.Contains("BIN")) sfd.Filter = "Plik Binarny (*.bin)|*.bin"; //opcja zapisu do pliku bin
            else sfd.Filter = "Plik Dowolny (*.*)|*.*";
            sfd.FileName = "Wiadomosc";
            if (sfd.ShowDialog() == true) 
            {
                File.WriteAllText(sfd.FileName, text);
                return true;
            }

            return false;
        }

        public static string print_array_n_row(char[,] table, int rows) //metoda sluzaca do wypisania macierzy w oknie dialogowym
        {
            String temp = String.Empty; //deklaracja pustego stringa

            for (int i = 0; i < table.Length / rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    temp += table[i, j]; //dopisanie znaku z danej komorki macierzy do stringa
                }
                temp += "\r\n"; //dodanie znaku konca wiersza
            }

            return temp; //zwrocenie macierzy w postaci stworzonego stringa
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

        public static string Extract_8_bit(char[,] table) //metoda sluzaca do wydzielenia pierwszych 8 bitow z kazdego wiersza macierzy 16 bitowej
        {
            string temp = String.Empty; //deklaracja pustego stringa
            int size = table.Length;   //pobranie rozmiaru macierzy podanej w parametrze metody (ilosc wszystkich elementow)
            size = size / 16; //liczba wierszy macierzy 

            for (int i = 0; i < size; i++)  
            {
                for (int j = 0; j < 8; j++)
                {
                    temp += table[i, j]; //wpisanie danego bitu do stringa
                }
            }
            return temp; //zwrocenie utworzonego stringa
        }


        public static string Decode(string text) 
        {
            int numOfBytes = text.Length / 8;
            byte[] bytes = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; ++i)
            {
                bytes[i] = Convert.ToByte(text.Substring(8 * i, 8), 2);
            }

            string back = System.Text.Encoding.UTF8.GetString(bytes);
            return back;
        }
    }
}

    





