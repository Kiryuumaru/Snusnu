using Syncfusion.SfSkinManager;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDark = false;
        public MainWindow()
        {
            InitializeComponent();
            AddTheme("MaterialDark");
            AddTheme("MaterialLight");
        }

        private void AddTheme(string style)
        {
            SkinHelper styleInstance = null;
            var skinHelpterStr = "Syncfusion.Themes." + style + ".WPF." + style + "SkinHelper, Syncfusion.Themes." + style + ".WPF";
            Type skinHelpterType = Type.GetType(skinHelpterStr);
            if (skinHelpterType != null)
                styleInstance = Activator.CreateInstance(skinHelpterType) as SkinHelper;
            if (styleInstance != null)
            {
                SfSkinManager.RegisterTheme(style, styleInstance);
            }
            SfSkinManager.SetTheme(this, new Theme(style));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (isDark)
            {
                isDark = false;
                SfSkinManager.SetTheme(this, new Theme("MaterialLight"));
            }
            else
            {
                isDark = true;
                SfSkinManager.SetTheme(this, new Theme("MaterialDark"));
            }
        }
    }
}
