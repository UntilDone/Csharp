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
    /// ch9.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ch9 : Window
    {
        public ch9()
        {
            InitializeComponent();
        }

        private void chb3_Checked(object sender, RoutedEventArgs e)
        {
            txt.Background = Brushes.Salmon;
        }

        private void chb3_Unchecked(object sender, RoutedEventArgs e)
        {
            txt.Background = Brushes.White;
        }

        private void chbAll_Checked(object sender, RoutedEventArgs e)
        {
            chb1.IsChecked = chbAll.IsChecked;
            chb2.IsChecked = chbAll.IsChecked;
            chb3.IsChecked = chbAll.IsChecked;
            chb4.IsChecked = chbAll.IsChecked;
        }

        private void chbAll_Unchecked(object sender, RoutedEventArgs e)
        {
            chb1.IsChecked = chbAll.IsChecked;
            chb2.IsChecked = chbAll.IsChecked;
            chb3.IsChecked = chbAll.IsChecked;
            chb4.IsChecked = chbAll.IsChecked;
        }
    }
}
