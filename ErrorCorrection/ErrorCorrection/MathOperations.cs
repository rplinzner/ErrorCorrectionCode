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
            char[,] full_m = new char[bin_tab.Length / 8, 16];

            for (int i = 0; i < bin_tab.Length / 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    full_m[i, j] = bin_tab[i, j];
                }

            }
            char[,] ctrl_sums = new char[bin_tab.Length / 8,8];

            for (int i = 0; i < bin_tab.Length / 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    bool czy_parzyste = false;
                    for (int k = 0; k < 8; k++)
                    {
                        if ((bin_tab[i, k].Equals('1')) && (bin_tab[i, k].Equals(tab_hash[j, k])))
                        {
                            czy_parzyste = !czy_parzyste;
                        }
                    }

                    if (czy_parzyste) ctrl_sums[i,j] = '1';
                    else ctrl_sums[i, j] = '0';

                }
            }

            for (int i = 0; i < bin_tab.Length / 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    full_m[i, j + 8] = ctrl_sums[i, j];
                }
            }

            return full_m;
          
        }

        public int CheckErrors(char[,] bin_tab, char[,] tab_hash)
        {
            int error_index = 17;
            char[,] errors = new char[bin_tab.Length/16,8];

            for (int i = 0; i < bin_tab.Length / 16; i++)
            {         
                for (int j = 0; j < 8; j++)
                {
                    bool czy_parzyste = false;

                    for (int k = 0; k < 16; k++)
                    {
                        if (bin_tab[i, k].Equals('1') && tab_hash[j, k].Equals('1'))
                        {
                            czy_parzyste = !czy_parzyste;
                        }

                        if (!czy_parzyste)
                        {
                            errors[i, j] = '0';
                        }
                        else
                        {
                            errors[i, j] = '1';
                        }
                    }          
                }
            }


            for (int k = 0; k < bin_tab.Length / 16; k++)
            {
                var czy_blad = true;            

                for(int i=0;i<16;i++)
                {
                    czy_blad = true;

                    for (int j = 0; j < 8; j++)
                    {
                        if (tab_hash[j,i] != (errors[k, j]))
                        {
                            czy_blad = false;
                            break;
                        }
                    }

                    if (czy_blad.Equals(true))
                    {
                        if (bin_tab[k, i].Equals('1'))
                        {
                            bin_tab[k, i] = '0';
                        }
                        else
                        {
                            bin_tab[k, i] = '1';
                        }
                       
                        error_index = i;
                        i = 17;
                    }                   
                }                          
            }

            return error_index;
        }
        
    };
}
