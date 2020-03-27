using Project.Models;
using Project.Service;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;

namespace Project.ViewModel
{
    public class MainWindowViewModel
    {
        public ObservableCollection<ProgramModels> Programs { get; } = new ObservableCollection<ProgramModels>();
        private FileOpenAndSave _fileOpenAndSave;

        public MainWindowViewModel()
        {
            Watcher _programWatcher = new Watcher();
            Thread thread = new Thread(_programWatcher.Wather);
            thread.Start();
            LoadData();
            _programWatcher.NotifyStart += AddNew;
            _programWatcher.NotifyStop += AddInSave;
            
        }

        public void LoadData()
        {
            string _path = $"{Environment.CurrentDirectory}\\Service\\programDataList.json";
            _fileOpenAndSave = new FileOpenAndSave(_path);
            try
            {
                foreach (var pr in _fileOpenAndSave.LoadDate())
                    Programs.Add(pr);
            }
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
                MessageBox.Show($"Поврежден файл {_path}.");
                Programs.Clear();
            }
        }

        public void AddNew(string id)
        {
            Programs[0].MaxIndex +=1;

            var proc = Process.GetProcessById(int.Parse(id));
            DateTime date = DateTime.Now;
            var procStart = proc.StartTime;

            Programs.Add(
             new ProgramModels()
             {
                 Id = id,
                 Date = date.ToShortDateString(),
                 Name = proc.ProcessName,
                 TimeStart = procStart,
                 ShortTimeStart = procStart.ToString()
             });

            //_fileOpenAndSave.SaveDate(_programs);
        }

        public void AddInSave(string id)
        {
            var element = Programs.First(f => f.Id == id);
            var index = Programs.IndexOf(element);
            DateTime time = DateTime.Now;
            Programs[index].TimeStop = time;
            Programs[index].LongTime =
            time.ToLocalTime().Subtract(Programs[index].
            TimeStart);

            //_fileOpenAndSave.SaveDate(_programs);
        }
    }
}
