using Snusnu.Services;
using Snusnu.ViewModels.Windows;
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
    /// Interaction logic for MarketsWindow.xaml
    /// </summary>
    public partial class MarketsWindow : ChromelessWindow
    {
        private readonly Session session;
        private readonly MarketsWindowViewModel viewModel;

        public MarketsWindow(Session session)
        {
            InitializeComponent();
            this.session = session;
            this.session.Appearance.RegisterDependency(this);
            viewModel = new MarketsWindowViewModel(session);
            DataContext = viewModel;
        }
    }
}
