using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
    /// ch7.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ch7 : Window
    {
        public ch7()
        {
            InitializeComponent();
        }

        private void hyper_MouseEnter(object sender, MouseEventArgs e)
        {
            hyper.TextDecorations = TextDecorations.Underline;
        }

        private void hyper_MouseLeave(object sender, MouseEventArgs e)
        {
            hyper.TextDecorations = null;
        }

        private void hyper_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            /*
            System.Diagnostics.ProcessStartInfo uri = new System.Diagnostics.ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            };
            
            System.Diagnostics.Process.Start(uri);
            */
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }
    }
}
