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
    }
}
