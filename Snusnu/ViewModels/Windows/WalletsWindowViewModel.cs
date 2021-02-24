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
    public class WalletsWindowViewModel : BaseViewModel
    {
        private Session session;

        public ObservableCollection<Wallet> Wallets => session.BinanceWrapper.Wallets;

        public WalletsWindowViewModel(Session session)
        {
            this.session = session;
        }
    }
}
