using Project.Service;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly string fileNamePrograms = $"{Environment.CurrentDirectory}\\Service\\programDataArray.xml";

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Watcher watcher = new Watcher
            (
                XDocument.Load(fileNamePrograms)
                .Root
                .Elements("program")
                .Select(el => el.Value)
            );

            MainWindowViewModel viewModel = new MainWindowViewModel(Dispatcher, watcher);
            new MainWindow()
            {
                DataContext = viewModel
            }
            .Show();
        }
    }
}
