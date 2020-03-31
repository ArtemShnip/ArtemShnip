using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Project.Models
{
    public class ProgramModels : INotifyPropertyChanged
    {
        private string _id;
        private string _name;
        private string _date;
        private DateTime _timeStart;
        private DateTime _timeStop;
        private TimeSpan _longTime;
        private string _client;

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public DateTime TimeStart
        {
            get => _timeStart;
            set
            {
                _timeStart = value;
                OnPropertyChanged();
            }
        }

        public DateTime TimeStop
        {
            get => _timeStop;
            set
            {
                _timeStop = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan LongTime
        {
            get => _longTime;
            set
            {
                _longTime = value;
                OnPropertyChanged();
            }
        }

        public string Client
        {
            get => _client;
            set
            {
                _client = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }   
    }
}