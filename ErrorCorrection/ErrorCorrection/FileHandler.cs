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
                if (Path.HasExtension(ofd.FileName) == true)
                {
                    if (Path.GetExtension(ofd.FileName).Equals(".txt"))
                    {
                        return File.ReadAllText(ofd.FileName);
                    }
                    if (Path.GetExtension(ofd.FileName).Equals(".bin"))
                    {
                        byte[] fileBytes = File.ReadAllBytes(ofd.FileName);
                        StringBuilder sb = new StringBuilder();
                        foreach (byte b in fileBytes)
                        {
                            sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
                        }

                       var str = sb.ToString();
                        str = AddNewLineMarkerEveryNChar(str, 16);
                       return str;
                    }
                }
            }

            return "";
        }


        public static string RemoveNewLineMarkers(string text)
        {
            string outString = string.Empty;
            foreach (char c in text)
            {
                if (c != '\r' && c != '\n')
                {
                    outString += c;
                }
            }

            return outString;
        }

        public static string AddNewLineMarkerEveryNChar(string text, int n)
        {
            int lines = text.Length / n;
            string output = string.Empty;
            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < n+1; j++)
                {
                    if (j.Equals(n))
                    {
                        output += '\r';
                        output += '\n';
                    }
                    else
                    {
                        output += text[(i * n) + j];
                    }
                }
            }

            return output;
        }

        public static bool SaveFile(string text, string mode) //metoda sluzaca do zapisu do pliku
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (mode.Contains("TXT")) sfd.Filter = "Plik Tekstowy (*.txt)|*.txt"; //opcja zapisu do pliku txt
            else if (mode.Contains("BIN")) sfd.Filter = "Plik Binarny (*.bin)|*.bin"; //opcja zapisu do pliku bin
            sfd.FileName = "Wiadomosc";
            
            if (sfd.ShowDialog() == true) 
            {

                if (Path.GetExtension(sfd.FileName).Equals(".txt"))
                {
                    File.WriteAllText(sfd.FileName, text);
                    return true;
                }

                if (Path.GetExtension(sfd.FileName).Equals(".bin"))
                {
                    string textNoNewLine = RemoveNewLineMarkers(text);
                    int numOfBytes = textNoNewLine.Length / 8;
                    byte[] bytes = new byte[numOfBytes];
                    for (int i = 0; i < numOfBytes; ++i)
                    {
                        bytes[i] = Convert.ToByte(textNoNewLine.Substring(8 * i, 8), 2);
                    }
                    File.WriteAllBytes(sfd.FileName,bytes);
                    return true;
                }
                
                
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

    





