using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorCorrection
{
    public class MathOperations
    {
        public char[,] get_hash_table() // metoda zwracajaca macierz haszujaca
        {
            char[,] tab_hash = new char[8, 16] //deklaracja macierzy haszującej (pierwsze 8 kolumn ,a drugie 8 kolumn to macierz jednostkowa
            {
                {'0', '1', '1', '1', '0', '0', '1', '0', '1', '0', '0', '0', '0', '0', '0', '0'},
                {'0', '1', '1', '0', '0', '1', '1', '0', '0', '1', '0', '0', '0', '0', '0', '0'},
                {'1', '0', '1', '1', '0', '1', '0', '1', '0', '0', '1', '0', '0', '0', '0', '0'},
                {'1', '0', '1', '0', '0', '0', '1', '1', '0', '0', '0', '1', '0', '0', '0', '0'},
                {'1', '0', '1', '1', '1', '1', '0', '0', '0', '0', '0', '0', '1', '0', '0', '0'},
                {'0', '1', '1', '0', '1', '0', '1', '0', '0', '0', '0', '0', '0', '1', '0', '0'},
                {'0', '1', '0', '1', '1', '1', '1', '1', '0', '0', '0', '0', '0', '0', '1', '0'},
                {'1', '0', '1', '1', '1', '0', '1', '1', '0', '0', '0', '0', '0', '0', '0', '1'}

            };
            return tab_hash;
        }

        public char[,] suma_kontrolna(char[,] bin_tab, char[,] tab_hash) //metoda sluzaca do generowania sumy kontrolnej dla kazdego slowa bitowego (8 bitow)
        {
            char[,] full_m = new char[bin_tab.Length / 8, 16]; //deklaracja macierzy do ktorej wpisane zostana slowa bitowe i sumy kontrolne dla kazdego ze slow

            for (int i = 0; i < bin_tab.Length / 8; i++)
            {
                for (int j = 0; j < 8; j++)  
                {
                    full_m[i, j] = bin_tab[i, j]; //wpisanie do pierwszych osmiu komorek wiersza bitow kazdego ze znakow
                }

            }

            char[,] ctrl_sums = new char[bin_tab.Length / 8, 8]; //dekalracja tablicy zawierajacej sumy kontrolne

            for (int i = 0; i < bin_tab.Length / 8; i++) 
            {
                for (int j = 0; j < 8; j++)
                {
                    bool czy_parzyste = false; //deklaracja 
                    for (int k = 0; k < 8; k++)
                    {
                        if ((bin_tab[i, k].Equals('1')) && (bin_tab[i, k].Equals(tab_hash[j, k])))
                        {
                            czy_parzyste = !czy_parzyste;
                        }
                    }

                    if (czy_parzyste) ctrl_sums[i, j] = '1';
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

            return full_m; //zwrocenie pelnej macierzy

        }

        public char[,] CheckErrors(char[,] bin_tab, char[,] tab_hash) //metoda sluzaca do wykrywania zaklocen
        {
            char[,] error_index = new char[bin_tab.Length / 16, 16];
            char[,] errors = new char[bin_tab.Length / 16, 8];

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

                for (int i = 0; i < 16; i++)
                {
                    czy_blad = true; //ustawienie flagi bledu (domyslnie true, czyli wystapienie zaklocenia

                    for (int j = 0; j < 8; j++)
                    {
                        if (tab_hash[j, i] != (errors[k, j])) 
                        {
                            czy_blad = false; //ustawienie flagi bledu na false (czyli brak zaklocenia)
                            break;
                        }
                    }

                    if (czy_blad) 
                    {
                        error_index[k, i] = '1'; //jesli w danym miejscu wystapilo zaklocenie, wpisywana jest 1
                    }
                    else
                    {
                        error_index[k, i] = '0'; //0 oznacza brak wystapienia bledu
                    }
                }
            }

            return error_index; //zwrocenie macierzy bitow w ktorej 1 oznacza miejsce wystapienia bledu
        }

        public char[,] CorrectErrors(char[,] error_index, char[,] bin_tab) //metoda sluzaca do poprawiania zaklocen bitowych
        {
            char[,] repaired_matrix = new char[bin_tab.Length / 16, 16];  //deklaracja macierzy do ktorej wpisane zostana poprawne bity

            for (int i = 0; i < bin_tab.Length / 16; i++) 
            {
                for (int j = 0; j < 16; j++)
                {
                    if (error_index[i, j].Equals(('1')))
                    {
                        if (bin_tab[i, j].Equals('1'))
                        {
                            bin_tab[i, j] = '0';   //
                        }
                        else
                        {
                            bin_tab[i, j] = '1';
                        }
                    }
                }
            }

            return repaired_matrix; //zwrocenie naprawionej macierzy 
        }
    };
}