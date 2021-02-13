using Snusnu.Services.SessionObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snusnu
{
    public static class Defaults
    {
        public const string FileExtension = "snu";

        public static readonly Theme DefaultTheme = Theme.MaterialLight;
        public static readonly string GeneralCurrency = "USD";
        public static readonly TimeSpan RefreshSpan = TimeSpan.FromMinutes(5);
        public static readonly TimeSpan RetrySpan = TimeSpan.FromSeconds(2);

    }
}
