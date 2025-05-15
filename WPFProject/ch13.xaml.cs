using Microsoft.Win32;
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
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// ch13.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ch13 : Window
    {
        public ch13()
        {
            InitializeComponent();
        }

        private void btnFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                txtPath.Text = openFileDialog.FileName;
            }

        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(txtPath.Text))
            {
                using(StreamReader sr=new StreamReader(txtPath.Text))
                {
                    richTxt.Document.Blocks.Clear();
                    richTxt.AppendText(sr.ReadToEnd());
                }
            }
            else
            {
                MessageBox.Show("파일이 없습니다.", "에러", MessageBoxButton.OK);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            richTxt.Document.Blocks.Clear();
        }
    }
}
