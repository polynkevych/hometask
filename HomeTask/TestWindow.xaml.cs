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
using System.Windows.Shapes;

namespace HomeTask
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow(string fullPath)
        {
            InitializeComponent();

            _testLoader = new TestLoader(fullPath);
            LoadNextQuestion();
        }

        private TestLoader _testLoader;
        private int _currentQuestion = 0;
        private int _correctAnswer;
        private int _numberOfCorrectAnswers;
        private void LoadNextQuestion()
        {
            _currentQuestion++;
            _testLoader.ParseNewQuestion(_currentQuestion, question, radioButton1, radioButton2, radioButton3, image, out _correctAnswer, out bool endOfFile);
            openImage.Visibility = image.Visibility;
            if (endOfFile)
            {
             

                var messageBox = MessageBox.Show(MainWindow.surname + "Please show this result to teacher" +"You answered " + _numberOfCorrectAnswers + " out of " + (_currentQuestion - 1) + " answers correctly!", "Hooray!");
                if (messageBox == MessageBoxResult.OK)
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }
            }
        }

        private void nextQuestion_Click(object sender, RoutedEventArgs e)
        {
            int answer = 0;
            if (radioButton1.IsChecked == true)
                answer = 1;
            else if (radioButton2.IsChecked == true)
                answer = 2;
            else if (radioButton3.IsChecked == true)
                answer = 3;
            else return;

            if (answer == _correctAnswer)
            {
                _numberOfCorrectAnswers++;
            }

            radioButton1.IsChecked = false;
            radioButton2.IsChecked = false;
            radioButton3.IsChecked = false;

            LoadNextQuestion();
        }

        private void openImage_Click(object sender, RoutedEventArgs e)
        {
            var imageWindow = new Window1(image.Source);
            imageWindow.Show();
        }
    }
}
