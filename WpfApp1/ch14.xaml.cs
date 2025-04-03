using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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


namespace WpfApp1
{
    /// <summary>
    /// ch14.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ch14 : Window
    {
        public ch14()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File(*.txt)|*.txt|XML File(*.xml)|*.xm|JSON File(*.json)|*.json"; // 저장파일 창에서 필터 설정 Type File(*.Type)|*.Type 형태로 추가하면 된다.
            if (saveFileDialog.ShowDialog() == true)
            {
                using(StreamWriter sw=new StreamWriter(saveFileDialog.FileName))
                {
                    TextRange textRange = new TextRange(richTxt.Document.ContentStart, richTxt.Document.ContentEnd);
                    sw.WriteLine(textRange.Text);
                }
            }
            MessageBox.Show("파일이 저장되었습니다.");
        }
    }
}
