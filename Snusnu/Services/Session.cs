using Snusnu.Models;
using Snusnu.Services.SessionObjects;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace Snusnu.Services
{
    public class Session
    {
        #region Properties

        public string AbsolutePath { get; private set; }
        public Datastore Datastore { get; private set; }
        public Logger Logger { get; private set; }
        public Appearance Appearance { get; private set; }
        public BinanceWrapper BinanceWrapper { get; private set; }

        public string FileName => Path.GetFileNameWithoutExtension(AbsolutePath);

        #endregion

        #region Initializers

        private Session() { }
        public static async Task<Session> Start(string absolutePath)
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
                AbsolutePath = absolutePath
            };
            session.Datastore = await Datastore.Initialize(session);
            session.Logger = await Logger.Initialize(session);
            session.Appearance = await Appearance.Initialize(session);
            session.BinanceWrapper = await BinanceWrapper.Initialize(session);
            session.Logger.AddLog(new Log(DateTime.Now, "General", "Initializing . . .", LogType.Info));
            return session;
        }

        #endregion

        #region Methods



        #endregion
    }
}
