using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// ch12.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ch12 : Window
    {
        private ObservableCollection<Person> list;

        public ch12()
        {
            InitializeComponent();
            list = new ObservableCollection<Person>
            {
                new Person{IsChecked=true, Name="홍길동", Age=100},
                new Person{IsChecked=false, Name="임꺽정", Age=90},
                new Person{IsChecked=false, Name="타요", Age=5},
                new Person{IsChecked=true, Name="뽀로로", Age=7}
            };
            lst.ItemsSource = list;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            list.Add(new Person { IsChecked = true, Name = "뽀로로2", Age = 3 });
        }
    }

    public class Person
    {
        public bool IsChecked { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

    }
}
