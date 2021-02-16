using MvvmHelpers;
using Snusnu.Models;
using Snusnu.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snusnu.ViewModels.Windows
{
    public class DashboardWindowViewModel : BaseViewModel
    {
        private Session session;

        public string HeaderName => session.FileName;
        public ObservableCollection<Wallet> Wallets => session.BinanceWrapper.Wallets;
        public ObservableCollection<Market> Markets => session.BinanceWrapper.Markets;

        public DashboardWindowViewModel(Session session)
        {
            this.session = session;
        }
    }
}
