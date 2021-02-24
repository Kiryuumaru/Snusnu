using Snusnu.Services;
using Snusnu.ViewModels.Windows;
using Syncfusion.SfSkinManager;
using Syncfusion.UI.Xaml.NavigationDrawer;
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
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow : ChromelessWindow
    {
        private readonly Session session;
        private readonly DashboardWindowViewModel viewModel;

        public DashboardWindow(Session session)
        {
            InitializeComponent();
            this.session = session;
            this.session.Appearance.RegisterDependency(this);
            viewModel = new DashboardWindowViewModel(session);
            DataContext = viewModel;
        }
    }
}
