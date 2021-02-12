using Microsoft.Win32;
using Snusnu.Services;
using Syncfusion.Windows.Shared;
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

namespace Snusnu.Views.Windows
{
    /// <summary>
    /// Interaction logic for ProfileStarterWindow.xaml
    /// </summary>
    public partial class ProfileStarterWindow : ChromelessWindow
    {
        public ProfileStarterWindow()
        {
            InitializeComponent();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            string[] args = Environment.GetCommandLineArgs();
            if (args != null)
            {
                string absolutePath = args.FirstOrDefault(i => Path.GetExtension(i).Equals("." + Defaults.FileExtension));
                if (!string.IsNullOrEmpty(absolutePath))
                {
                    AbsolutePath.Text = absolutePath;
                    StartSession();
                }
            }
        }

        private async void StartSession()
        {
            var session = await Session.Start(AbsolutePath.Text.Trim());
            if (session == null)
            {
                AbsolutePathInputLayout.ErrorText = "Invalid snu file";
                AbsolutePathInputLayout.HasError = true;
            }
            else
            {
                new APIInputWindow(session).Show();
                Close();
            }
        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            AbsolutePathInputLayout.HasError = false;
            try
            {
                var dlg = new SaveFileDialog
                {
                    Filter = "Snu files|*.snu;*.SNU"
                };
                var result = dlg.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    File.WriteAllText(dlg.FileName, "");
                    AbsolutePath.Text = dlg.FileName;
                    StartSession();
                }
            }
            catch (Exception ex)
            {
                AbsolutePathInputLayout.ErrorText = ex.Message;
                AbsolutePathInputLayout.HasError = true;
            }
        }

        private void Button_Click_Browse(object sender, RoutedEventArgs e)
        {
            AbsolutePathInputLayout.HasError = false;
            try
            {
                var dlg = new OpenFileDialog
                {
                    Filter = "Snu files|*.snu;*.SNU"
                };
                var result = dlg.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    AbsolutePath.Text = dlg.FileName;
                }
            }
            catch (Exception ex)
            {
                AbsolutePathInputLayout.ErrorText = ex.Message;
                AbsolutePathInputLayout.HasError = true;
            }
        }

        private void Button_Click_Open(object sender, RoutedEventArgs e)
        {
            AbsolutePathInputLayout.HasError = false;
            if (!File.Exists(AbsolutePath.Text))
            {
                AbsolutePathInputLayout.ErrorText = "File does not exist";
                AbsolutePathInputLayout.HasError = true;
            }
            else
            {
                StartSession();
            }
        }

        private void AbsolutePath_TextChanged(object sender, TextChangedEventArgs e)
        {
            AbsolutePathInputLayout.HasError = false;
        }
    }
}
