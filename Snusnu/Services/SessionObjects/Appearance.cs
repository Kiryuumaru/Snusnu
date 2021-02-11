using Syncfusion.SfSkinManager;
using Syncfusion.Themes.MaterialDark.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snusnu.Services.SessionObjects
{
    public struct Theme
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        private Theme(string name, string code) { Name = name; Code = code; }

        public static readonly Theme MaterialLight = new Theme("Light", "MaterialLight");
        public static readonly Theme MaterialDark = new Theme("Dark", "MaterialDark");

        public static IEnumerable<Theme> GetThemes()
        {
            return new Theme[]
            {
                MaterialLight,
                MaterialDark
            };
        }
        public static Theme FromCode(string code)
        {
            return code switch
            {
                "MaterialLight" => MaterialLight,
                "MaterialDark" => MaterialDark,
                _ => MaterialLight
            };
        }
    }

    public class Appearance
    {
        private Session session;

        private Theme? theme;
        public Theme Theme
        {
            get
            {
                if (theme == null)
                {
                    string data = session.Datastore.GetValue("theme");
                    theme = Theme.FromCode(data);
                }
                return theme.Value;
            }
            set
            {
                theme = value;
                SfSkinManager.SetTheme(session.DependencyObject, new Syncfusion.SfSkinManager.Theme(value.Code));
                Task.Run(delegate
                {
                    session.Datastore.SetValue("theme", value.Code.ToString());
                });
            }
        }

        private Appearance() { }
        public static async Task<Appearance> Initialize(Session session)
        {
            return await Task.Run(delegate
            {
                var appearance = new Appearance
                {
                    session = session
                };
                appearance.Theme = appearance.Theme;
                return appearance;
            });
        }
    }
}
