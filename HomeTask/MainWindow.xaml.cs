using Microsoft.Win32;
using System;
using System.IO;
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

namespace HomeTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string surname;

        public MainWindow()
        {
            InitializeComponent();
        
        }

        private void selectTestFile_Click(object sender, RoutedEventArgs e)
        {
           
            surname = textBox.Text;
           
            {
                var dlg = new OpenFileDialog
                {
                    FileName = "Test",
                    DefaultExt = ".txt",
                    Filter = "Text documents (.txt)|*.txt"
                };

                bool? result = dlg.ShowDialog();

                if (result == true)
                {
                    string fileName = dlg.FileName;

                    var testWindow = new TestWindow(fileName);
                    testWindow.Show();
                    Close();
                }
            }

        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            
        }
    }
}
