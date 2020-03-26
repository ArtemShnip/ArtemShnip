using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Project.Models
{
    public class ProgramModels : INotifyPropertyChanged, INotifyCollectionChanged
    {
        private string _id;
        private string _name;
        private string _date;
        private DateTime _timeStart;
        private string _shortTimeStart;
        private DateTime _timeStop;
        private TimeSpan _longTime;
        private string _client;
        private int _maxIndex;

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
        public string ShortTimeStart
        {
            get => _shortTimeStart;
            set
            {
                _shortTimeStart = value;
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

        public int MaxIndex
        {
            get => _maxIndex;
            set
            {
                _maxIndex = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}