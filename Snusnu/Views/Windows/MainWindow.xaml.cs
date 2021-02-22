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
            navigationDrawer.ItemClicked += (s, e) => SelectNavigationItem(e.Item);
            SelectNavigationItem((NavigationItem)navigationDrawer.SelectedItem);
        }

        private void SelectNavigationItem(NavigationItem item)
        {
            if (item == dashboard)
            {
                dashboardGrid.Visibility = Visibility.Visible;
                walletsGrid.Visibility = Visibility.Hidden;
                marketsGrid.Visibility = Visibility.Hidden;
            }
            else if (item == wallets)
            {
                dashboardGrid.Visibility = Visibility.Hidden;
                walletsGrid.Visibility = Visibility.Visible;
                marketsGrid.Visibility = Visibility.Hidden;
            }
            else if (item == markets)
            {
                dashboardGrid.Visibility = Visibility.Hidden;
                walletsGrid.Visibility = Visibility.Hidden;
                marketsGrid.Visibility = Visibility.Visible;
            }
        }

        private void Settings_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
