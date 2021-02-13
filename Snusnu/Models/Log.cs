using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snusnu.Models
{
    public enum LogType
    {
        Info, Warning, Error
    }

    public class Log : ObservableObject
    {
        #region UIElements

        private string strDateTime;
        public string StrDateTime
        {
            get => strDateTime;
            set => SetProperty(ref strDateTime, value);
        }

        private string strCategory;
        public string StrCategory
        {
            get => strCategory;
            set => SetProperty(ref strCategory, value);
        }

        private string strMessage;
        public string StrMessage
        {
            get => strMessage;
            set => SetProperty(ref strMessage, value);
        }

        private string strLogType;
        public string StrLogType
        {
            get => strLogType;
            set => SetProperty(ref strLogType, value);
        }

        #endregion

        public DateTime DateTime { get; private set; }
        public string Category { get; private set; }
        public string Message { get; private set; }
        public LogType LogType { get; private set; }

        public Log(DateTime dateTime, string category, string message, LogType logType)
        {
            DateTime = dateTime;
            Category = category;
            Message = message;
            LogType = logType;
            NotifyUpdates();
        }

        public void NotifyUpdates()
        {
            StrDateTime = DateTime.ToString();
            StrCategory = Category;
            StrMessage = Message;
            StrLogType = LogType switch
            {
                LogType.Info => "Info",
                LogType.Warning => "Warning",
                LogType.Error => "Error",
                _ => "Error"
            };
        }
    }
}
