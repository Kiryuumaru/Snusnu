using Snusnu.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snusnu.Services.SessionObjects
{
    public class Logger
    {
        private Session session;

        public readonly ObservableCollection<Log> Logs = new ObservableCollection<Log>();

        public event Action<Log> OnLog;

        private Logger() { }
        public static async Task<Logger> Initialize(Session session)
        {
            return await Task.Run(delegate
            {
                return new Logger()
                {
                    session = session
                };
            });
        }

        public void AddLog(Log log)
        {
            Logs.Add(log);
            OnLog?.Invoke(log);
            Console.WriteLine(log.DateTime.ToString() + " " + log.Category + ": " + log.Message);
        }
    }
}
