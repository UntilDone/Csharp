using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml;

namespace ProjectSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        InstallerConfig _config;

        public MainWindow()
        {
            InitializeComponent();
            LoadConfig();
            InstallPathTextBox.Text = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                _config.InstallFolderName
            );
        }

        private void LoadConfig()
        {
            var config = new InstallerConfig();
            XmlDocument doc = new XmlDocument();
            doc.Load("installer-config.xml");

            config.AppName = doc.SelectSingleNode("//AppName")?.InnerText;
            config.InstallFolderName = doc.SelectSingleNode("//InstallFolderName")?.InnerText;

            config.FilesToInstall = new List<string>();
            var fileNodes = doc.SelectNodes("//FilesToInstall/File");
            foreach (XmlNode node in fileNodes)
            {
                config.FilesToInstall.Add(node.InnerText);
            }

            config.CreateDesktopShortcut = bool.Parse(doc.SelectSingleNode("//CreateDesktopShortcut")?.InnerText ?? "false");
            config.CreateStartMenuShortcut = bool.Parse(doc.SelectSingleNode("//CreateStartMenuShortcut")?.InnerText ?? "false");

            _config = config;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                InstallPathTextBox.Text = dialog.SelectedPath;
            }
        }

        private void InstallButton_Click(object sender, RoutedEventArgs e)
        {
            string targetDir = InstallPathTextBox.Text;
            Directory.CreateDirectory(targetDir);

            ProgressBar.Value = 0;
            int fileCount = _config.FilesToInstall.Count;
            int currentFile = 0;

            foreach (var file in _config.FilesToInstall)
            {
                string sourcePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), file);
                string destPath = Path.Combine(targetDir, Path.GetFileName(file));
                File.Copy(sourcePath, destPath, overwrite: true);

                currentFile++;
                ProgressBar.Value = (double)currentFile / fileCount * 100;
            }

            if (_config.CreateDesktopShortcut)
                CreateShortcut(targetDir, Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

            if (_config.CreateStartMenuShortcut)
                CreateShortcut(targetDir, Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.StartMenu),
                    "Programs"
                ));

            RegisterUninstaller(targetDir);

            MessageBox.Show("설치가 완료되었습니다!", "설치 완료", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        private void CreateShortcut(string targetDir, string shortcutLocation)
        {
            string targetExe = Path.Combine(targetDir, _config.FilesToInstall[0]);
            string shortcutPath = Path.Combine(shortcutLocation, $"{_config.AppName}.url");

            using (StreamWriter writer = new StreamWriter(shortcutPath))
            {
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine($"URL=file:///{targetExe.Replace('\\', '/')}");
                writer.WriteLine($"IconFile={targetExe.Replace('\\', '/')}");
                writer.WriteLine("IconIndex=0");
            }
        }

        private void RegisterUninstaller(string installDir)
        {
            string uninstallKeyPath = $@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{_config.AppName}";
            using (RegistryKey uninstallKey = Registry.LocalMachine.CreateSubKey(uninstallKeyPath))
            {
                uninstallKey.SetValue("DisplayName", _config.AppName);
                uninstallKey.SetValue("InstallLocation", installDir);
                uninstallKey.SetValue("Publisher", "Your Company");
                uninstallKey.SetValue("UninstallString", Path.Combine(installDir, "Uninstall.exe"));
            }
        }
    }

    public class InstallerConfig
    {
        public string AppName { get; set; }
        public string InstallFolderName { get; set; }
        public List<string> FilesToInstall { get; set; }
        public bool CreateDesktopShortcut { get; set; }
        public bool CreateStartMenuShortcut { get; set; }
    }
}