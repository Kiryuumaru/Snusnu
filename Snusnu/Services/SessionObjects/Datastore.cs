using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snusnu.Services.SessionObjects
{
    public class Datastore
    {
        private Session session;
        private string fileContent;
        private bool isWriting = false;
        private int saveRequests = 0;

        private Datastore() { }

        public static async Task<Datastore> Initialize(Session session)
        {
            return await Task.Run(delegate
            {
                var datastore = new Datastore
                {
                    session = session
                };
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(datastore.session.AbsolutePath)))
                        Directory.CreateDirectory(Path.GetDirectoryName(datastore.session.AbsolutePath));
                    if (!File.Exists(datastore.session.AbsolutePath))
                        File.WriteAllText(datastore.session.AbsolutePath, "");
                    datastore.fileContent = File.ReadAllText(datastore.session.AbsolutePath);
                }
                catch { }
                return datastore;
            });
        }

        private void Save()
        {
            saveRequests++;
            if (isWriting) return;
            isWriting = true;
            Task.Run(async delegate
            {
                while (saveRequests > 0)
                {
                    try
                    {
                        string contentCopy = fileContent;
                        File.WriteAllText(session.AbsolutePath, contentCopy);
                        await Task.Delay(500);
                    }
                    catch { }
                    saveRequests--;
                }
                isWriting = false;
            });
        }

        public void SetValue(string key, string value)
        {
            fileContent = CommonHelpers.BlobSetValue(fileContent, key, value);
            Save();
        }

        public string GetValue(string key)
        {
            return CommonHelpers.BlobGetValue(fileContent, key);
        }
    }
}
