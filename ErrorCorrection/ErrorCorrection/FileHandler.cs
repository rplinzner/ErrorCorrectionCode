using System;
using System.Collections.Generic;
using System.IO;
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
    }
}
