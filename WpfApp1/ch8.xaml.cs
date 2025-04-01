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
    /// ch8.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ch8 : Window
    {
        public ch8()
        {
            InitializeComponent();
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MessageBox.Show(txt.Text);
            }

        }
    }
}
