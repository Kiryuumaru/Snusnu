using Snusnu.Services;
using Syncfusion.Windows.Shared;
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

namespace Snusnu.Views.Windows
{
    /// <summary>
    /// Interaction logic for APIInputWindow.xaml
    /// </summary>
    public partial class APIInputWindow : ChromelessWindow
    {
        private readonly Session session;

        public APIInputWindow(Session session)
        {
            InitializeComponent();
            this.session = session;
            this.session.Appearance.RegisterDependency(this);
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            if (session.BinanceWrapper.IsCredentialsReady) Start();
        }

        private void Start()
        {
            if (!session.BinanceWrapper.IsCredentialsReady)
            {
                if (!session.BinanceWrapper.TrySetApi(APIKey.Text.Trim(), APISecret.Text.Trim())) return;
            }
            new DashboardWindow(session).Show();
            Close();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            APIKeyInputLayout.HasError = false;
            APISecretInputLayout.HasError = false;
            if (string.IsNullOrEmpty(APIKey.Text))
            {
                APIKeyInputLayout.ErrorText = "This field is required";
                APIKeyInputLayout.HasError = true;
            }
            if (string.IsNullOrEmpty(APISecret.Text))
            {
                APISecretInputLayout.ErrorText = "This field is required";
                APISecretInputLayout.HasError = true;
            }
            if (!string.IsNullOrEmpty(APIKey.Text) && !string.IsNullOrEmpty(APISecret.Text)) Start();
        }

        private void APIKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            APIKeyInputLayout.HasError = false;
        }

        private void APISecret_TextChanged(object sender, TextChangedEventArgs e)
        {
            APISecretInputLayout.HasError = false;
        }
    }
}
