using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorCorrection
{
   public class MathOperations

    {

        public char[,] get_hash_table()
        {
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
            return tab_hash;
        }

        public char[,] suma_kontrolna(char[,] bin_tab, char[,] tab_hash)
        {
            char[,] full_m = new char[bin_tab.Length / 8,16];

            for (int i = 0; i < bin_tab.Length / 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    full_m[i, j] = bin_tab[i, j];
                }

            }

            for (int i = 0; i < bin_tab.Length / 8; i++)
            {
                bool czy_parzyste = false;
                for(int j=0; j<8; j++)
                {    for (int k = 8; k < 16; k++)
                    {
                    
                        if (full_m[i, k - 8] == '1' && full_m[i, k - 8] == tab_hash[j, k - 8])
                        {
                            czy_parzyste = !czy_parzyste;
                        }

                        if (!czy_parzyste)
                        {
                            full_m[i, k] = '0';
                        }
                        else
                        {
                            full_m[i, k] = '1';
                        }
                    }

                }

                
            }
            return full_m;

            /* for (int k = 0; k < bin_tab.Length / 8; k++)
             {
                 char[] suma_k = new char[8];

                 for (int i = 0; i < 8; i++)
                 {
                     bool czy_parzyste = false;

                     for (int j = 0; j < 8; j++)
                     {

                         if (bin_tab[k, j] == '1' && bin_tab[k, j] == tab_hash[i, j])
                         {
                             czy_parzyste = !czy_parzyste;
                         }

                         if (!czy_parzyste)
                         {
                             suma_k[i] = '0';
                         }
                         else
                         {
                             suma_k[i] = '1';
                         }

                     }
                 }

                 full_m[k]

             }*/

        }


        public char[] mnozenie_macierzy(char[] wiersz, char[,] tab_hash)
        {
            char[] wynik = new char[8];

            for (int i = 0; i < 8; i++)
            {
                var czyParzyste = false;

                for (int j = 0; j < 16; j++)
                {
                    if (wiersz[j] == '1' &&  tab_hash[i, j] == '1')
                    {
                        czyParzyste = !czyParzyste;
                    }

                    if (!czyParzyste)
                    {
                        wynik[i] = '0';
                    }
                    else
                    {
                        wynik[i] = '1';
                    }

                }
            }

            return wynik;
        }




    };
}
