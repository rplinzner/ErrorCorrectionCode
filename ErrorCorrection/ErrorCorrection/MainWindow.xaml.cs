using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if (this.SaveModeSelect.SelectedItem == null)
            {
                MessageBox.Show("Najpierw wybierz tryb zapisu!", "Ostrzezenie", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return;
            }
           
           var type = (string) this.SaveModeSelect.SelectionBoxItem;
           
            if (FileHandler.SaveFile(this.FileContent.Text, type) == true)
                MessageBox.Show("Zapis do pliku powiodl sie!", "Informacja", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            else MessageBox.Show("Zapis do pliku NIE powiodl sie!", "Ostrzezenie", MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }

        private void OpenButton_OnClick(object sender, RoutedEventArgs e)
        {
            var text = FileHandler.OpenFile();
            this.FileContent.Text = text;
        }

        private void SaveModeSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = (ComboBox) sender;
            var value = (ComboBoxItem) combo.SelectedValue;
            string str = (string) value.Content;
            if (str.Contains("BIN"))
            {
                if (this.FileContent.Text == "")
                {
                    MessageBox.Show("Nie wczytano Pliku lub nie wpisano znakow!", "Ostrzezenie", MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    
                    return;
                }
                FileReader fr = new FileReader();
                var temp1 = fr.BinConvert(this.FileContent.Text);
                var temp2 = fr.Make8ElementPerRow(temp1);
                var mo = new MathOperations();
                var temp3 = mo.suma_kontrolna(temp2, mo.get_hash_table());

                this.FileContent.Text = FileHandler.print_array_16_row(temp3);
            }
        }
    }
}
