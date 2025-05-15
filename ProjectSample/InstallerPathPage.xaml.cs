using System.Windows.Controls;
using System.Windows;
using System.Windows.Forms;
namespace InstallerWPF
{
    public partial class InstallPathPage : System.Windows.Controls.UserControl
    {
        public string InstallPath { get; private set; }

        public InstallPathPage()
        {
            InitializeComponent();

            // 기본 경로 설정
            InstallPath = System.IO.Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles),
                "MyCustomApp");

            PathTextBox.Text = InstallPath;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "설치할 폴더를 선택하세요";
                dialog.SelectedPath = InstallPath;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    InstallPath = dialog.SelectedPath;
                    PathTextBox.Text = InstallPath;
                }
            }
        }
    }
}

