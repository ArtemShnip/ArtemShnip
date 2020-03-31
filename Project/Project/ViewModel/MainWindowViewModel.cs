using Project.Models;
using Project.Service;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace Project.ViewModel
{
    public class MainWindowViewModel
    {
        public ObservableCollection<ProgramModels> Programs { get; } = new ObservableCollection<ProgramModels>();
        private FileOpenAndSave _fileOpenAndSave;

        private readonly Watcher Watcher;
        public Dispatcher Dispatcher { get; }

        public MainWindowViewModel()
        {
            /// Конструктор по умолчанию 
            /// Используется для задания
            /// Контекста Данных Времени Разработки
        }

        public MainWindowViewModel(Dispatcher dispatcher, Watcher watcher)
        {
            Watcher = watcher;
            Dispatcher = dispatcher;

            //Thread thread = new Thread(Watcher.StartWorkAsync);
            //thread.Start();
            Watcher.NotifyStart += AddNew;
            Watcher.NotifyStop += AppendInNew;
            Watcher.StartWorkAsync();
            LoadData();

        }

        /// Этот метод тоже надо вынести в App
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
                //Programs.Clear();
            }
        }

        public void AddNew(string id)
        {
            var proc = Process.GetProcessById(int.Parse(id));
            DateTime date = DateTime.Now;
            var procStart = proc.StartTime;

            // Работа с коллекцией в UI потоке

            Dispatcher?.BeginInvoke((Action)(() =>
            {
                lock (Programs) // локируем Programs от изменений в других потоках
                    Programs.Add(
                     new ProgramModels()
                     {
                         Id = id,
                         Date = date.ToShortDateString(),
                         Name = proc.ProcessName,
                         TimeStart = procStart
                     });
            }));

            //_fileOpenAndSave.SaveDate(_programs);
        }

        public void AppendInNew(string id)
        {
            lock(Programs) // Локируем на время проверки
                for (int index = Programs.Count-1; index > 0; index--)
                {
                    if (Programs[index].Id == id)
                    {
                        DateTime time = DateTime.Now;
                        Programs[index].TimeStop = time;
                        Programs[index].LongTime =
                        time.ToLocalTime().Subtract(Programs[index].TimeStart);

                        break;
                    }
                }

            //_fileOpenAndSave.SaveDate(_programs);
        }
    }
}
