using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ErrorCorrection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Save_Button_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.SaveModeSelect == null) return;
            if (this.FileContent.Text == "")
            {
                MessageBox.Show("Nie ma nic do zapisania", "Ostrzeżenie", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return;
            }
            if (this.SaveModeSelect.SelectedItem == null)
            {
                MessageBox.Show("Najpierw wybierz tryb zapisu!", "Ostrzeżenie", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return;
            }
           
           var type = (string) this.SaveModeSelect.SelectionBoxItem;
           
            if (FileHandler.SaveFile(this.FileContent.Text, type) == true)
                MessageBox.Show("Zapis do pliku powiodl sie!", "Informacja", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            else MessageBox.Show("Zapis do pliku NIE powiodl sie!", "Ostrzeżenie", MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        private void OpenButton_OnClick(object sender, RoutedEventArgs e)
        {
            var text = FileHandler.OpenFile();
            this.FileContent.Text = text;
            
        }

        private void KonvTxtBinButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.FileContent.Text == "")
            {
                MessageBox.Show("Nie wczytano pliku lub nie wpisano znaków!", "Ostrzeżenie", MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }
            FileReader fr = new FileReader();
            var temp1 = fr.BinConvert(this.FileContent.Text);
            var temp2 = fr.Make8ElementPerRow(temp1);
            var mo = new MathOperations();
            var temp3 = mo.suma_kontrolna(temp2, mo.get_hash_table());

            var aaa = FileHandler.print_array_n_row(temp3,16).ToCharArray();

            this.FileContent.Text = FileHandler.print_array_n_row(temp3,16);
        }

        private void KonvBinTxt_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.FileContent.Text == "")
            {
                MessageBox.Show("Nie wczytano pliku lub nie wpisano znakow!", "Ostrzeżenie", MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            string temp1 = FileContent.Text;
            var errorCounter = Regex.Matches(temp1, @"[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ2-9]").Count;

            if (errorCounter>0 || temp1.Contains(" "))
            {
                MessageBox.Show($"Podany tekst nie jest binarny! Zawiera {errorCounter} niepoprawnych znaków", "Ostrzeżenie", MessageBoxButton.OK,
                    MessageBoxImage.Hand);
                return;
            }

            if (temp1.Length < 16)
            {
                MessageBox.Show("Błędny format - zbyt mało znaków!", "Ostrzeżenie", MessageBoxButton.OK,
                    MessageBoxImage.Hand);
                return;
            }
            var temp2 = FileHandler.ReadFile(temp1);
            var temp3 = FileHandler.Extract_8_bit(temp2);
            var temp4 = FileHandler.Decode(temp3);
            FileContent.Text = temp4;
        }

        private void CheckErrorsButton_OnClick(object sender, RoutedEventArgs e)
        {

            if (this.FileContent.Text == "")
            {
                MessageBox.Show("Nie wczytano pliku lub nie wpisano znakow!", "Ostrzeżenie", MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            string temp1 = FileContent.Text;
            var errorCounter = Regex.Matches(temp1, @"[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ2-9]").Count;

            if (errorCounter > 0 || temp1.Contains(" "))
            {
                MessageBox.Show($"Podany tekst nie jest binarny! Zawiera {errorCounter} niepoprawnych znaków", "Ostrzeżenie", MessageBoxButton.OK,
                    MessageBoxImage.Hand);
                return;
            }

            if (temp1.Length < 16)
            {
                MessageBox.Show("Błędny format - zbyt mało znaków!", "Ostrzeżenie", MessageBoxButton.OK,
                    MessageBoxImage.Hand);
                return;
            }

            //Część programu po sprawdzeniu czy w okienku nie ma błędów

            var temp2 = FileHandler.ReadFile(temp1);
            var mo = new MathOperations();

            //Sprawdzanie błędów
            var errors = mo.CreateErrorArray(temp2);
            var errorList = mo.FindAllErrors(temp2, errors, mo.get_hash_table());



            //var tempStr = FileHandler.print_array_n_row(errors, 8); //wartość string służąca do sprawdzenia czy istnieją błędy

            //Sprawdzanie w których wierszach wystąpiły błędy
            //var rowsErr = mo.FindRowErrors(temp2, errors);
            //string RowsErrStr = String.Empty;

            //wypisywanie błędów
            /* if (tempStr.Contains('1'))
             {
            for (int i = 0; i < rowsErr.Length; i++)
            {
                RowsErrStr += (rowsErr[i] + 1).ToString();
                if (i < rowsErr.Length - 1) RowsErrStr += ", ";
                else RowsErrStr += "\r\n";
            }*/




            //MessageBox.Show($"Zawiera błędy w następujących wierszach: {}");

            FileContent.Text = FileHandler.print_array_n_row(errorList,16);
                

            //}
            /*else
            {
                MessageBox.Show("nie zawiera");
            }*/
            



            //var macierz_bledy = mo.FindErrors(temp2, mo.get_hash_table());
            /* FileContent.Text = FileHandler.print_array_n_row(mo.FindErrors(temp2, mo.get_hash_table()), 16);
             //FileContent.Text = string.Join("", mo.FindErrors(temp2,mo.get_hash_table()));
             FileContent.Text = macierz_bledy.ToString();*/
        }
    }
}
