
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ErrorCorrection
{
    public class MathOperations
    {
        public char[,] get_hash_table()  // metoda zwracajaca macierz haszujaca
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

        public char[] XorHashColumns(int a, int b) //
        {
            char[] temp = new char[8];
            var hasz = get_hash_table();

            for (int i = 0; i < 8; i++)
            {
                if (hasz[i, a] != hasz[i, b])
                {
                    temp[i] = '1';
                }
                else
                {
                    temp[i] = '0';
                }
            }

            return temp;
        }

        public char[,] suma_kontrolna(char[,] bin_tab, char[,] tab_hash)  //metoda sluzaca do generowania sumy kontrolnej dla kazdego slowa bitowego (8 bitow)
        { 
            char[,] full_m = new char[bin_tab.Length / 8, 16];  //deklaracja macierzy do ktorej wpisane zostana slowa bitowe i sumy kontrolne dla kazdego ze slow

            for (int i = 0; i < bin_tab.Length / 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    full_m[i, j] = bin_tab[i, j];  //wpisanie do pierwszych osmiu komorek wiersza bitow kazdego ze znakow
                }

            }

            char[,] ctrl_sums = new char[bin_tab.Length / 8, 8];  //dekalracja tablicy zawierajacej sumy kontrolne

            for (int i = 0; i < bin_tab.Length / 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    bool czy_parzyste = false; //deklaracja flagi parzystosci
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
                    full_m[i, j + 8] = ctrl_sums[i, j]; //dopisanie bitow sumy kontrolnej do bitow slowa
                }
            }

            return full_m; //zwrocenie pelnej macierzy

        }

        public char[,] CreateErrorArray(char[,] bin_tab) //metoda sluzaca do tworzenia macierzy bledow
        {
            char[,] errors = new char[bin_tab.Length / 16, 8];
            var tab_hash = get_hash_table();

            for (int i = 0; i < bin_tab.Length / 16; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    bool czy_parzyste = false;

                    for (int k = 0; k < 16; k++) //nastepuje mnozenie macierzy
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

            return errors; //zwrocenie macierzy bledow 

        }


        public int[] FindRowErrors(char[,] bin_tab, char[,] errors) //metoda zwracajaca indeksy wierszy w ktorych wystepuja bledy
        {
            List<int> rowsWithErrors = new List<int>(); //deklaracja listy 

            for (int i = 0; i < bin_tab.Length / 16; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (errors[i, j].Equals('1')) 
                    {
                        rowsWithErrors.Add(i); //jesli w danym wierszu wystepuje blad, umieszczamy jego indeks w liscie
                        break;
                    }
                }
            }

            var errorRows = new int[rowsWithErrors.Count]; 
            rowsWithErrors.CopyTo(errorRows);
            return errorRows; //zwrocenie wektora z indeksami wierszy
        }

        public bool CompareTwoArrays(char[,] arr1, int index, char[] arr2) //metoda porownujaca dwa wektory
        {
            bool result = true; //deklaracja flagi zgodnosci wektorow

            for (int i = 0; i < 8; i++)
            {
                if (arr1[index,i] != arr2[i])
                {
                    result = false; //jesli wystapi niezgodnosc, flaga jest odpowiednio ustawiana i nastepuje wyjscie z petli
                    break;
                }
            }

            return result; //zwrocenie wartosci flagi
        }

        public char[,] FindAllErrors(char[,] bin_tab, char[,] errors, char[,] hasz_tab)
        {
            char[,] errorPlacement = new char[bin_tab.Length/16,16];
            for (int i = 0; i < bin_tab.Length / 16; i++)   //zapełniamy tablice zerami
            {
                for (int j = 0; j < 16; j++)
                {
                    errorPlacement[i, j] = '0';
                }
            }

            for (int i = 0; i<errors.Length / 8; i++)
            {
                bool FurtherAction = true;

                for (int j = 0; j < 16; j++)
                {
                    bool IsErrorSingle = true;

                    for (int k = 0; k < 8; k++)
                    {
                        if (hasz_tab[k, j] != errors[i, k])
                        {
                            IsErrorSingle = false;
                            break;
                        }
                    }

                    if (IsErrorSingle)
                    {
                        errorPlacement[i, j] = '1';
                        FurtherAction = false;
                        break;
                    }
                }
                //Jeśli jest więcej niż 1 error
                if (FurtherAction)
                {
                    char[] col_sum = new char[8];

                    bool done = false;
                    for (int j = 0; j < 16; j++)
                    {
                        for (int k = (j + 1); k < 16; k++)
                        {
                            col_sum = XorHashColumns(j, k);
                            if (CompareTwoArrays(errors, i, col_sum))
                            {
                                errorPlacement[i, j] = '1';
                                errorPlacement[i, k] = '1';
                                done = true;
                            }

                            if (done) break;
                        }

                        if (done) break;
                    }
                }

            }

            return errorPlacement;

        }

        public char[,] CorrectErrors(char[,] error_index, char[,] bin_tab) //metoda sluzaca do poprawiania bledow 
        {
            char[,] repaired_matrix = new char[bin_tab.Length / 16, 16]; //deklaracja macierzy do ktorej zostana wpisane poprawne bity

            for (int i = 0; i < bin_tab.Length / 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (error_index[i, j].Equals(('1')))
                    {
                        if (bin_tab[i, j].Equals('1'))
                        {
                            bin_tab[i, j] = '0';
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
