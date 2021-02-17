using Snusnu.Services;
using Snusnu.ViewModels.Windows;
using Syncfusion.SfSkinManager;
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
    /// Interaction logic for MainWIndow.xaml
    /// </summary>
    public partial class MainWindow : ChromelessWindow
    {
        private readonly Session session;
        private readonly MainWindowViewModel viewModel;

        public MainWindow(Session session)
        {
            InitializeComponent();
            this.session = session;
            this.session.Appearance.RegisterDependency(this);
            viewModel = new MainWindowViewModel(session);
            DataContext = viewModel;
        }

        private void NavigationDrawer_ItemClicked(object sender, Syncfusion.UI.Xaml.NavigationDrawer.NavigationItemClickedEventArgs e)
        {

        }
    }
}
