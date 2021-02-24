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
    public class MarketsWindowViewModel : BaseViewModel
    {
        private Session session;

        public ObservableCollection<Market> Markets => session.BinanceWrapper.Markets;

        public MarketsWindowViewModel(Session session)
        {
            this.session = session;
        }
    }
}
