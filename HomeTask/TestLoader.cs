using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HomeTask
{
    class TestLoader
    {
        public TestLoader(string path)
        {
            _path = path;
        }

        public void ParseNewQuestion(int questionNumber, TextBlock question, RadioButton answer1, RadioButton answer2, RadioButton answer3, Image image, MediaElement video, out int correctAnswer, out bool endOfFile)
        {
            List<char> questionString;
            List<char> answer1String;
            List<char> answer2String;
            List<char> answer3String;

            ParseText(questionNumber, out questionString, out answer1String, out answer2String, out answer3String,
                out string imgPath, out string videoPath, out correctAnswer, out endOfFile);

            question.Text = String.Concat(questionString);
            answer1.Content = String.Concat(answer1String);
            answer2.Content = String.Concat(answer2String);
            answer3.Content = String.Concat(answer3String);

            video.LoadedBehavior = MediaState.Manual;

            if (imgPath != "")
            {
                image.Visibility = System.Windows.Visibility.Visible;
                var fullImagePath = System.IO.Path.GetDirectoryName(_path) + "\\" + imgPath;
                var uri = new Uri(fullImagePath);
                var bitmap = new BitmapImage(uri);
                image.Source = bitmap;
            }
            else
                image.Visibility = System.Windows.Visibility.Hidden;

            if (videoPath != "")
            {
                video.Visibility = System.Windows.Visibility.Visible;
                var fullVideoPath = System.IO.Path.GetDirectoryName(_path) + "\\" + videoPath;
                video.Source = new Uri(fullVideoPath);
                video.Play();
            }
            else
            {
                video.Visibility = System.Windows.Visibility.Hidden;
                video.Stop();
            }
        }

        private string _path;

        private void ParseText(int questionNumber, out List<char> questionString, out List<char> answer1String, out List<char> answer2String, out List<char> answer3String,
            out string imgPath, out string videoPath, out int correctAnswer, out bool endOfFile)
        {
            string[] lines = System.IO.File.ReadAllLines(_path);

            imgPath = "";
            videoPath = "";

            endOfFile = false;

            bool isReading = false;

            correctAnswer = 1;

            questionString = new List<char>();
            answer1String = new List<char>();
            answer2String = new List<char>();
            answer3String = new List<char>();

            if (lines.Length == 0)
            {
                endOfFile = true;
                return;
            }

            foreach (var line in lines)
            {
                if (!isReading)
                {
                    if (line.Contains("[START:"))
                    {
                        var questionNumberString = String.Concat(line.Where(c => char.IsDigit(c)));
                        int qustionNumberInt = int.Parse(questionNumberString);

                        if (qustionNumberInt == questionNumber)
                        {
                            isReading = true;
                            continue;
                        }
                        else continue;
                    }
                    else continue;
                 }
                if (line == "[END]") return;

                if (line.Contains('@'))
                    imgPath = String.Concat(line.Where(c => c != '@'));

                if (line.Contains('#'))
                    videoPath = String.Concat(line.Where(c => c != '#'));

                if (line.Contains('~'))
                    questionString = line.Where(c => c != '~').ToList();

                if (line.Contains('"') && !line.Contains('~'))
                {
                    if (answer1String.Count == 0)
                        answer1String = line.Where(c => c != '"').ToList();
                    else if (answer2String.Count == 0)
                        answer2String = line.Where(c => c != '"').ToList();
                    else if (answer3String.Count == 0)
                        answer3String = line.Where(c => c != '"').ToList();
                }

                if (line.Contains("RIGHT:"))
                {
                    char correct = line.FirstOrDefault(c => char.IsDigit(c));
                    correctAnswer = (int)Char.GetNumericValue(correct);;
                }
            }

            if (answer1String.Count == 0)
                endOfFile = true;
        }
    }
}
