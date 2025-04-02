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

namespace WpfApp1
{
    /// <summary>
    /// ch11.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ch11 : Window
    {
        public ch11()
        {
            InitializeComponent();
        }

        private void txtCurrent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                slider1.Value = Int32.Parse(txtCurrent.Text);
                txtCurrent.Text = slider1.Value.ToString();
                MessageBox.Show("값이 변경되었습니다.");
            }
        }
    }
}
