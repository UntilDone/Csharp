using InstallerWPF;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace ProjectSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int currentPageIndex = 0;
        private UserControl[] pages;

        public MainWindow()
        {
            InitializeComponent();

            // 페이지 초기화
            pages = new UserControl[]
            {
                 new InstallPathPage(),
                new WelcomePage()

                // 다음 단계에서 추가할 페이지들
            };

            PageHost.Content = pages[currentPageIndex];
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageIndex < pages.Length - 1)
            {
                currentPageIndex++;
                PageHost.Content = pages[currentPageIndex];
            }
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageIndex > 0)
            {
                currentPageIndex--;
                PageHost.Content = pages[currentPageIndex];
            }
        }
    }
}