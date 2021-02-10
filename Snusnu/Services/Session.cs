using Snusnu.Services.SessionObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snusnu.Services
{
    public static class Session
    {
        #region Properties

        public static BinanceWrapper BinanceWrapper { get; private set; }

        #endregion

        #region Initializers

        public static async Task Initialize(string filename)
        {
            BinanceWrapper = await BinanceWrapper.Initialize(filename);
        }

        #endregion

        #region Methods



        #endregion
    }
}
