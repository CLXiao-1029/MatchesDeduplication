using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ChineseMatches
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void _Btn_QuChong_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_Text_Box.Text) || string.IsNullOrWhiteSpace(_Text_Box.Text))
            {
                MessageBox.Show("当前去重文本是空的。");
                return;
            }
            if (string.IsNullOrEmpty(_Text_Pattern.Text) || string.IsNullOrWhiteSpace(_Text_Pattern.Text))
            {
                MessageBox.Show("匹配的正则表达式不能为空。");
                return;
            }


            string str = _Text_Box.Text;// " Hello 你好 World 世界 Hello 你好 World 世界Hello 你好 World 世界 ，。123*/？{}《》：“‘";
            string pattern = _Text_Pattern.Text; // 匹配中文的正则表达式
            var matches = Regex.Matches(str, pattern);
            var distinctMatches = new HashSet<string>(); // 用HashSet来去重
            foreach (Match match in matches)
            {
                distinctMatches.Add(match.Value); // 将匹配到的中文字符添加到HashSet中
            }
            var result = string.Join("", distinctMatches); // 将中文字符拼接成字符串
            Console.WriteLine( result);
            _Text_Box_Copy.Text = result;
        }

        private void _Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            // Set the file name filter
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            // Set the initial directory (optional)
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Show the dialog box and get the result
            bool? result = saveFileDialog.ShowDialog();

            // Process the result
            if (result == true)
            {
                // Get the file name
                string fileName = saveFileDialog.FileName;

                // Save the file
                File.WriteAllText(fileName, _Text_Box_Copy.Text);
            }
        }
    }
}
