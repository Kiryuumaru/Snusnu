using Snusnu.Services.SessionObjects;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace Snusnu.Services
{
    public class Session
    {
        #region Properties

        public string AbsolutePath { get; private set; }
        public DependencyObject DependencyObject { get; private set; }
        public Datastore Datastore { get; private set; }
        public Appearance Appearance { get; private set; }
        public BinanceWrapper BinanceWrapper { get; private set; }

        #endregion

        #region Initializers

        private Session() { }
        public static async Task<Session> Start(DependencyObject dependencyObject, string absolutePath)
        {
            try
            {
                using var write = File.OpenWrite(absolutePath);
                write.Close();
            }
            catch
            {
                return null;
            }
            var session = new Session()
            {
                DependencyObject = dependencyObject,
                AbsolutePath = absolutePath
            };
            session.Datastore = await Datastore.Initialize(session);
            session.Appearance = await Appearance.Initialize(session);
            session.BinanceWrapper = await BinanceWrapper.Initialize(session);
            return session;
        }

        #endregion

        #region Methods



        #endregion
    }
}
