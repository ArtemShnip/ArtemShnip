using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Project.Service
{
    public class Watcher
    {
        // Модель не может знать какое приложение её использует.
        // Поэтому она не знает что такое "Environment" приложения
        // private readonly string _path = $"{Environment.CurrentDirectory}\\Service\\programDataArray.xml";

        /// <summary>Список отслеживаемых программ</summary>
        public readonly HashSet<string> WatchPrograms;
        public readonly Dictionary<string, string> runnedProgramms = new Dictionary<string, string>();
        public delegate void EventWatch(string id);
        public event EventWatch NotifyStart, NotifyStop;

        public Watcher(IEnumerable<string> namePrograms)
        {
            WatchPrograms = namePrograms?.ToHashSet();
        }

        public async Task StartWorkAsync()
        {
            //LoadArray(_path);
            await Task.Run(() =>
             {
                 ManagementEventWatcher startProgramm = new ManagementEventWatcher(
                     new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
                 startProgramm.EventArrived += StartProcesses;
                 startProgramm.Start();
                 ManagementEventWatcher stopProgramm = new ManagementEventWatcher(
                     new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace"));
                 stopProgramm.EventArrived += StopProcesses;
                 stopProgramm.Start();

                //partially solved the problem
                Thread.Sleep(2147483647);

                 startProgramm.Stop();
                 stopProgramm.Stop();
             });
        }

        void StartProcesses(object programm, EventArrivedEventArgs e)
        {
            string name = e.NewEvent.Properties["ProcessName"].Value.ToString();
            //if ((WatchPrograms == null || WatchPrograms.Contains(name)) && !runnedProgramms.ContainsValue(name))
            {
                string id = e.NewEvent.Properties["ProcessId"].Value.ToString();
                runnedProgramms.Add(id, name);
                NotifyStart?.Invoke(id);
            }
        }

        void StopProcesses(object programm, EventArrivedEventArgs e)
        {
            string id = e.NewEvent.Properties["ProcessId"].Value.ToString();
            if (runnedProgramms.ContainsKey(id))
            {
                runnedProgramms.Remove(id);
                NotifyStop?.Invoke(id);
            }
        }

        //public void LoadArray(string path)
        //{
        //    Type type = typeof(string[]);
        //    string[] retVal;
        //    XmlSerializer formatter = new XmlSerializer(type);
        //    using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
        //    {
        //        retVal = (string[])formatter.Deserialize(stream);
        //    }
        //    WatchPrograms = retVal;
        //}
    }
}
