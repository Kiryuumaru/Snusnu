using Snusnu.Models;
using Syncfusion.SfSkinManager;
using Syncfusion.Themes.MaterialDark.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                _ => Defaults.DefaultTheme
            };
        }
    }

    public class Appearance
    {
        private Session session;
        private readonly List<DependencyObject> dependencyObjects = new List<DependencyObject>();

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
                foreach (var obj in new List<DependencyObject>(dependencyObjects))
                {
                    if (obj.Dispatcher != null) SfSkinManager.SetTheme(obj, new Syncfusion.SfSkinManager.Theme(value.Code));
                    else dependencyObjects.Remove(obj);
                }
                Task.Run(delegate
                {
                    session.Datastore.SetValue("theme", value.Code);
                });
                session.Logger.AddLog(new Log(DateTime.Now, "Appearance", "Changed to " + value.Code, LogType.Info));
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

        public void RegisterDependency(DependencyObject obj)
        {
            dependencyObjects.Add(obj);
            SfSkinManager.SetTheme(obj, new Syncfusion.SfSkinManager.Theme(Theme.Code));
        }
    }
}
